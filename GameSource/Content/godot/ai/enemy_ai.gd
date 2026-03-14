extends Node

#Lower values is kind of the turning radius
#How to make the impact of each rule work good together?
#Do everey function need to be in the same "pixel unit"?
#The move_towards_strategic_point() should, when used alone, end up at that point not circle around it. How to do that?

const STRATEGIC_MOVEMENT_MULT: float = 1.0 
const CENTER_MOVEMENT_MULT: float = 0.3
const TOO_NEAR_MOVEMENT_MULT: float = 0.7
const TOO_NEAR_DISTANCE_LIMIT: float = 25.0
const MATCHING_MOVEMENT_MULT: float = 0.5
const KEEP_SAME_DISTANCE_MOVEMENT_MULT: float = 1.0

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

func set_unit_movements(battle: Battle) -> void:
	var unit_list = battle.get_enemy_units()
	var own_centroid: Vector2 = Utils.calc_centroid_point(battle.get_unit_positions(unit_list))
	#The opposite_centroid is used for lining upp the enemies towards the friendly units
	#If the enemies try to have the same distance to this point, they will kind of line upp (or more make a circle around it)
	var opposite_centroid: Vector2 = Utils.calc_centroid_point(battle.get_unit_positions(battle.get_my_units()))
	
	for unit in unit_list:
		var near_unit_list = battle.find_nearest_units(unit, unit_list, 2)
		#Pseudo Boids
		var new_movement: Vector2 = Vector2.ZERO
		#Move towards a good position
		new_movement += move_towards_strategic_point(unit, battle)
		#Move towards your own fractions centroid
		new_movement += move_towards_own_center(unit, own_centroid)
		#Move away from any unit that is too close
		new_movement += move_away_from_units(unit, unit_list)
		#Match the direction and velocity of the near units
		new_movement += match_movement_of_near_units(near_unit_list)
		#Match the distance to the opposite_centroid from near units
		new_movement += keep_same_distance_to_opposite_center(unit, near_unit_list, opposite_centroid)
		unit.add_movement(new_movement)
		unit.is_movement_visible = true
		#print("Setting enemy unit movements " + unit.name + " " + str(new_movement))

func move_towards_strategic_point(unit: Unit, battle: Battle) -> Vector2:
	var highground_list = battle.get_highgrounds()
	var nearest_highground_point = Utils.find_nearest_point(unit.position, highground_list)
	return GameState.rescale_position(nearest_highground_point - unit.get_next_position()) * STRATEGIC_MOVEMENT_MULT
	
func move_towards_own_center(unit: Unit, centroid: Vector2) -> Vector2:
	return GameState.rescale_position(centroid - unit.get_next_position()) * CENTER_MOVEMENT_MULT

func move_away_from_units(unit: Unit, unit_list: Array[Unit]) -> Vector2:
	var new_movement: Vector2 = Vector2.ZERO
	for near_unit in unit_list:
		if (near_unit != unit && unit.get_next_position().distance_to(near_unit.get_next_position()) < TOO_NEAR_DISTANCE_LIMIT):
			new_movement += unit.get_next_position() - near_unit.get_next_position()
	return GameState.rescale_position(new_movement) * TOO_NEAR_MOVEMENT_MULT
	
func match_movement_of_near_units(unit_list: Array[Unit]) -> Vector2:
	var new_movement: Vector2 = Vector2.ZERO
	for near_unit in unit_list:
		new_movement += near_unit.movement_vector
	if(unit_list.size() > 0):
		return (new_movement/unit_list.size()) * MATCHING_MOVEMENT_MULT
	else:
		return Vector2.ZERO
		
func keep_same_distance_to_opposite_center(unit: Unit, unit_list: Array[Unit], centroid: Vector2) -> Vector2:
	var distance: float = unit.position.distance_to(centroid)
	var size: int = 1
	for near_unit in unit_list:
		if (near_unit != unit):
			distance += near_unit.position.distance_to(centroid)
			size += 1
	
	distance = distance/size
	var dist_diff = unit.get_next_position().distance_to(centroid) - distance
	var new_movement: Vector2 = (centroid - unit.get_next_position()).normalized() * dist_diff
	print("Calc movement next position: " + str(unit.get_next_position()) + ", center: " + str(centroid) + ", distance: " + str(distance) + ", distance diff: " + str(dist_diff) +", new movement: " + str(new_movement))
	return GameState.rescale_position(new_movement) * KEEP_SAME_DISTANCE_MOVEMENT_MULT
