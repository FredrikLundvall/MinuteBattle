extends Node

#The move_towards_strategic_point() should, when used alone, end up at that point not circle around it. How to do that?
#The sum of the different mult should add up to 1.0
const STRATEGIC_MOVEMENT_MULT: float = 1
const CENTER_MOVEMENT_MULT: float = 0
const TOO_NEAR_MOVEMENT_MULT: float = 0
const TOO_NEAR_PIXEL_DISTANCE_LIMIT: float = 35.0
const MATCHING_MOVEMENT_MULT: float = 0
const KEEP_SAME_DISTANCE_MOVEMENT_MULT: float = 2

# Formation / line grouping configuration
const FORMATION_MOVEMENT_MULT: float = 0.6
const MAX_UNITS_PER_LINE: int = 7
const SLOT_SPACING: float = 28.0
const LINE_SPACING: float = 36.0

# Persistent assignment configuration
const PERSIST_MAX_ROUNDS: int = 3
var _persistent_line_assignment: Dictionary = {}
var _persistent_assignment_age: Dictionary = {}

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	pass # Replace with function body.

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(_delta: float) -> void:
	pass

func play_cards(hand: Hand, player_state: PlayerState, battle: Battle) -> void:
	for i in hand.get_child_count() * 2:
		var lowest_resource: int = hand.lowest_resource()
		if player_state.camp_resource < lowest_resource:
			break
		var card: Card = hand.get_children().pick_random()
		if player_state.camp_resource < card.resource:
			continue
		if(player_state.camp_resource < card.resource):
			print("Not enough resources to play enemy card: " + card.title)
			return
		hand.card_played(card)
		var spawn_unit = card.get_node("Unit").duplicate()
		spawn_unit.is_enemy = true
		#Reduce the resource count
		player_state.camp_resource -= card.resource
		if EngineDebugger.is_active(): 
			#This is only shown for the testing
			spawn_unit.visible = true
		battle.spawn_enemy_unit(spawn_unit)
		print("Playing enemy card: " + card.title)

# set_unit_movements(battle)
# - Main entry point called each round to compute pixel movement vectors for all enemy units.
# - Builds formation lines oriented perpendicular to the player's centroid, assigns units to lines (with persistence),
#   clamps line movement based on the slowest member's movement allowance, and computes final movement by combining:
#   1) formation attraction (high priority), 2) separation from nearby units, 3) alignment with nearby units,
#   4) a reduced strategic movement toward highground, and 5) distance-matching to the opposite centroid.
func set_unit_movements(battle: Battle) -> void:
	var unit_list = battle.get_enemy_units()
	if unit_list.size() == 0:
		return
	var own_centroid: Vector2 = Utils.calc_centroid_point(battle.get_unit_positions(unit_list))
	#The opposite_centroid is used for lining upp the enemies towards the friendly units
	#If the enemies try to have the same distance to this point, they will kind of line upp (or more make a circle around it)
	var opposite_centroid: Vector2 = Utils.calc_centroid_point(battle.get_unit_positions(battle.get_my_units()))

	# Choose an anchor for the formation: prefer highground near our centroid, else use our centroid
	var highground_list = battle.get_highgrounds()
	var anchor_point: Vector2 = own_centroid
	if highground_list.size() > 0:
		anchor_point = Utils.find_nearest_point(own_centroid, highground_list)

	# Approach direction is from anchor towards the player's centroid (lines should be perpendicular to this)
	var approach_dir: Vector2 = (opposite_centroid - anchor_point)
	if approach_dir.length() == 0:
		approach_dir = Vector2.UP
	else:
		approach_dir = approach_dir.normalized()

	# Compute formation lines with desired centers and per-line slot offsets
	var lines = _compute_formation_lines(anchor_point, approach_dir, unit_list.size())

	# Assign each unit to a line center with persistence (stabilizes assignments between rounds)
	var line_assignments: Array = []
	for i in range(lines.size()):
		line_assignments.append([]) # list of units assigned to each line

	# Prune persistent assignments for units that no longer exist and age entries
	var current_units := {}
	for u in unit_list:
		current_units[u] = true
	var to_remove: Array = []
	for key in _persistent_line_assignment.keys():
		if not current_units.has(key):
			to_remove.append(key)
			continue
		_persistent_assignment_age[key] += 1
		if _persistent_assignment_age[key] > PERSIST_MAX_ROUNDS:
			to_remove.append(key)
	for r in to_remove:
		_persistent_line_assignment.erase(r)
		_persistent_assignment_age.erase(r)

	# First honor persistent assignments where possible
	for unit in unit_list:
		var honored: bool = false
		if _persistent_line_assignment.has(unit):
			var li = int(_persistent_line_assignment[unit])
			if li >= 0 and li < lines.size():
				if line_assignments[li].size() < lines[li]["units_in_line"]:
					line_assignments[li].append(unit)
					_persistent_assignment_age[unit] = 0
					honored = true
		if honored:
			continue
		# Otherwise assign to nearest available line
		var best_line: int = -1
		var best_dist: float = 1e9
		var u_pos: Vector2 = unit.get_pixel_position()
		for li in range(lines.size()):
			var line = lines[li]
			var cap = line["units_in_line"]
			if line_assignments[li].size() >= cap:
				continue
			var d = u_pos.distance_to(line["desired_center"])
			if d < best_dist:
				best_dist = d
				best_line = li
		if best_line >= 0:
			line_assignments[best_line].append(unit)
			_persistent_line_assignment[unit] = best_line
			_persistent_assignment_age[unit] = 0
		else:
			# fallback to first line
			line_assignments[0].append(unit)
			_persistent_line_assignment[unit] = 0
			_persistent_assignment_age[unit] = 0

	# For each line compute final (clamped) center based on member units and min movement_distance
	var assignments: Dictionary = {}
	for li in range(lines.size()):
		var line = lines[li]
		var members: Array = line_assignments[li]
		if members.size() == 0:
			continue
		# compute current centroid of members
		var member_positions: Array[Vector2] = []
		var min_move_pixels: float = 1e9
		for m in members:
			member_positions.append(m.get_pixel_position())
			var m_pixels = GameState.movement_length_to_pixels(m.movement_distance)
			if m_pixels < min_move_pixels:
				min_move_pixels = m_pixels
		var current_centroid: Vector2 = Utils.calc_centroid_point(member_positions)
		# desired center from line info
		var desired_center: Vector2 = line["desired_center"]
		var center_delta: Vector2 = desired_center - current_centroid
		# clamp center movement to min_move_pixels to avoid moving formation too far
		if center_delta.length() > min_move_pixels:
			center_delta = center_delta.normalized() * min_move_pixels
		var final_center: Vector2 = current_centroid + center_delta
		# assign slots inside line to nearest members greedily
		var available_slots: Array = []
		for s in line["slot_offsets"]:
			available_slots.append(final_center + s)
		for m in members:
			var best_idx: int = -1
			var best_d: float = 1e9
			var mpos: Vector2 = m.get_pixel_position()
			for si in range(available_slots.size()):
				var s_pos: Vector2 = available_slots[si]
				var d2: float = mpos.distance_to(s_pos)
				if d2 < best_d:
					best_d = d2
					best_idx = si
			if best_idx >= 0:
				assignments[m] = available_slots[best_idx]
				available_slots.remove_at(best_idx)
			else:
				assignments[m] = m.get_pixel_position()

	# Now compute movement for each unit combining formation attraction (PRIORITY) and other boids rules
	for unit in unit_list:
		var near_unit_list = battle.find_nearest_units(unit, unit_list, 2)
		var new_movement: Vector2 = Vector2.ZERO
		# Formation: primary; move toward assigned slot (loose coupling)
		var slot_pos: Vector2 = assignments.get(unit, unit.get_pixel_position())
		new_movement += (slot_pos - unit.get_pixel_position()) * FORMATION_MOVEMENT_MULT
		#Move away from any unit that is too close
		new_movement += move_away_from_units(unit, unit_list)
		#Match the direction and velocity of the near units
		new_movement += match_movement_of_near_units(unit, near_unit_list)
		# Secondary: Move towards strategic point but scaled down so formation is prioritized
		new_movement += move_towards_strategic_point(unit, battle) * 0.35
		#Match the distance to the opposite_centroid from near units
		new_movement += keep_same_distance_to_opposite_center(unit, near_unit_list, opposite_centroid)

		# Prevent overshooting the assigned slot along the slot direction
		var to_slot: Vector2 = slot_pos - unit.get_pixel_position()
		if to_slot.length() > 0:
			var dir_to_slot: Vector2 = to_slot.normalized()
			var forward_comp: float = new_movement.dot(dir_to_slot)
			var dist_to_slot: float = to_slot.length()
			if forward_comp > dist_to_slot:
				# remove only the excess forward component so unit does not pass the slot
				new_movement -= dir_to_slot * (forward_comp - dist_to_slot)

		unit.set_pixel_movement( unit.limit_pixel_movement_distance(new_movement))
		unit.is_movement_visible = true
		#print("Setting enemy unit movements " + unit.name + " " + str(new_movement))

# _compute_formation_lines(anchor_point, approach_dir, total_units)
# - Create formation 'lines' positioned around anchor_point.
# - Lines are arranged along approach_dir; individual slots are offset perpendicular to approach_dir.
# - Each returned line is a Dictionary: {"desired_center": Vector2, "slot_offsets": Array[Vector2], "units_in_line": int}
# - The function distributes total_units across multiple lines with up to MAX_UNITS_PER_LINE per line.
func _compute_formation_lines(anchor_point: Vector2, approach_dir: Vector2, total_units: int) -> Array:
	# Returns an array of line dictionaries:
	# { desired_center: Vector2, slot_offsets: Array[Vector2], units_in_line: int }
	var lines: Array = []
	if total_units <= 0:
		return lines
	var perp: Vector2 = Vector2(-approach_dir.y, approach_dir.x).normalized()
	var num_lines: int = int(ceil(float(total_units) / float(MAX_UNITS_PER_LINE)))
	var units_remaining: int = total_units
	var line_center_offset: float = (num_lines - 1) / 2.0
	for line_idx in range(num_lines):
		var units_in_line: int = min(units_remaining, MAX_UNITS_PER_LINE)
		units_remaining -= units_in_line
		var forward_offset: float = (line_idx - line_center_offset) * LINE_SPACING
		var line_center: Vector2 = anchor_point + approach_dir * forward_offset
		var slot_center_offset: float = (units_in_line - 1) / 2.0
		var slot_offsets: Array = []
		for s_idx in range(units_in_line):
			var slot_offset: float = (s_idx - slot_center_offset) * SLOT_SPACING
			slot_offsets.append(perp * slot_offset)
		lines.append({"desired_center": line_center, "slot_offsets": slot_offsets, "units_in_line": units_in_line})
	return lines

# move_towards_strategic_point(unit, battle)
# - Returns a vector pointing toward the nearest strategic highground point for the unit.
# - The returned vector is scaled by STRATEGIC_MOVEMENT_MULT so callers can weight its influence.
func move_towards_strategic_point(unit: Unit, battle: Battle) -> Vector2:
	var highground_list = battle.get_highgrounds()
	var nearest_highground_point = Utils.find_nearest_point(unit.get_pixel_position(), highground_list)
	return ((nearest_highground_point - unit.get_pixel_position())) * STRATEGIC_MOVEMENT_MULT

# move_towards_own_center(unit, centroid)
# - Returns a vector that moves the unit toward the provided centroid of friendly units.
# - This uses the unit's movement limit and CENTER_MOVEMENT_MULT to control influence.
func move_towards_own_center(unit: Unit, centroid: Vector2) -> Vector2:
	return unit.limit_pixel_movement_distance(centroid - unit.get_pixel_position()) * CENTER_MOVEMENT_MULT

# move_away_from_units(unit, unit_list)
# - Separation behavior: returns a vector pushing the unit away from any units closer than TOO_NEAR_PIXEL_DISTANCE_LIMIT.
# - Result is limited by the unit's movement allowance and scaled by TOO_NEAR_MOVEMENT_MULT.
func move_away_from_units(unit: Unit, unit_list: Array[Unit]) -> Vector2:
	var new_movement: Vector2 = Vector2.ZERO
	for near_unit in unit_list:
		if (near_unit != unit && unit.get_pixel_position().distance_to(near_unit.get_pixel_position()) < TOO_NEAR_PIXEL_DISTANCE_LIMIT):
			new_movement += unit.get_pixel_position() - near_unit.get_pixel_position()
	return unit.limit_pixel_movement_distance(new_movement) * TOO_NEAR_MOVEMENT_MULT
	
# match_movement_of_near_units(unit, unit_list)
# - Alignment behavior: computes the average pixel movement of nearby units and returns a matching vector.
# - The average is clamped by the unit's movement limit and scaled by MATCHING_MOVEMENT_MULT.
func match_movement_of_near_units(unit: Unit, unit_list: Array[Unit]) -> Vector2:
	var new_movement: Vector2 = Vector2.ZERO
	for near_unit in unit_list:
		new_movement += near_unit.get_pixel_movement()
	if(unit_list.size() > 0):
		return unit.limit_pixel_movement_distance(new_movement/unit_list.size()) * Vector2.ONE * MATCHING_MOVEMENT_MULT
	else:
		return Vector2.ZERO
		
# keep_same_distance_to_opposite_center(unit, unit_list, centroid)
# - Cohesion/spacing relative to the enemy centroid: computes how far the unit should move to match
#   the average distance of its neighbors to the provided centroid, then returns a vector toward/away
#   from the centroid scaled by KEEP_SAME_DISTANCE_MOVEMENT_MULT.
func keep_same_distance_to_opposite_center(unit: Unit, unit_list: Array[Unit], centroid: Vector2) -> Vector2:
	var distance: float = unit.position.distance_to(centroid)
	var size: int = 1
	for near_unit in unit_list:
		if (near_unit != unit):
			distance += near_unit.position.distance_to(centroid)
			size += 1

	distance = distance/size
	var dist_diff = unit.get_pixel_position().distance_to(centroid) - distance
	var new_movement: Vector2 = (centroid - unit.get_pixel_position()).normalized() * dist_diff
	print("Calc movement next position: " + str(unit.get_pixel_position()) + ", center: " + str(centroid) + ", distance: " + str(distance) + ", distance diff: " + str(dist_diff) +", new movement: " + str(new_movement))
	return new_movement * KEEP_SAME_DISTANCE_MOVEMENT_MULT
