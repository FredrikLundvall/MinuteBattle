class_name Battle extends Node2D

@onready var terrain: TileMapLayer = $Terrain
@onready var meleePicture: Texture2D = preload("res://hero/melee.png")
@onready var map_title_lbl = get_node("MapTitle")
@onready var selected_unit: Unit = null

signal unit_selected(unit: Unit)

const BATTLE_TITLE_TXT = "North of Breitenfeldt"
const FLAG_MARKER_TXT = "Ready the troops Commander!\nMove towards that flag."

# Called when the node enters the scene tree for the first time.
# Initialize the battle scene UI and state
func _ready() -> void:
	map_title_lbl.text = BATTLE_TITLE_TXT
	
# Spawn a friendly unit at the spawn coordinates with small random offset
func spawn_unit(unit: Unit):
	var unit_position = terrain.map_to_local(terrain.SPAWN_COORDINATES)
	unit_position = Utils.randomize_position(unit_position)
	unit.set_raw_position(unit_position)
	terrain.add_child(unit)
	
# Spawn an enemy unit at the enemy spawn coordinates with random offset
func spawn_enemy_unit(unit: Unit):
	var unit_position = terrain.map_to_local(terrain.SPAWN_ENEMY_COORDINATES)
	unit_position = Utils.randomize_position(unit_position)
	unit.set_raw_position(unit_position)
	terrain.add_child(unit)
	
# Handler when a unit is hovered: mark the unit as highlighted
func _on_unit_hovered(unit: Unit) -> void:
	unit.is_highlighted = true

# Handler when a unit is no longer hovered: clear highlight
func _on_unit_unhovered(unit: Unit) -> void:
	unit.is_highlighted = false
	
# Handler for unit click: select the unit and enable movement visuals
func _on_unit_clicked(unit: Unit) -> void:
	print(unit.name + " clicked")
	if selected_unit != null:
		selected_unit.is_selected = false
	unit.is_selected = true
	unit.movement_spr.rotation = PI / 2
	unit.is_movement_visible = true
	selected_unit = unit
	unit_selected.emit(unit)

# Input handler for the map area: deselect units when clicking empty space
func _on_map_area_input_event(_viewport: Node, event: InputEvent, _shape_idx: int) -> void:
	if selected_unit != null:
		if event.is_action_pressed("mouse_click") and not _is_any_unit_hovered():
			selected_unit.is_selected = false
			selected_unit = null
	else:
		if event.is_action_pressed("mouse_click"):
			print("position: " + str(terrain.get_local_mouse_position()))

# Returns true if the given node is a Unit and is currently selected
func _is_unit_and_selected(node: Node) -> bool:
	if node is Unit: 
		return node.is_selected
	else: 
		return false

# Queue-free a list of nodes to remove them from the scene
func _remove_nodes(nodes: Array[Node]) -> void:
	for node in nodes:
		node.queue_free()

# Returns true if any unit in the terrain children is currently selected
func _has_any_unit_selected() -> bool:
	for child in terrain.get_children():
		if _is_unit_and_selected(child):
			return true
	return false
	
# Clears selection flag on all units under terrain
func _unselect_all_units():
	for child in terrain.get_children():
		if child is Unit:
			var unit = (child as Unit)
			unit.is_selected = false

# Returns true if any unit under terrain is currently hovered
func _is_any_unit_hovered() -> bool:
	for child in terrain.get_children():
		if child is Unit:
			var unit = (child as Unit)
			if unit.is_hovered:
				return true
	return false

# Connect unit signals (hover, unhover, click) to local handlers if not already connected
func _unit_connect(unit: Unit):
	if !unit.unit_hovered.is_connected(_on_unit_hovered):   
		unit.unit_hovered.connect(_on_unit_hovered)
	if !unit.unit_unhovered.is_connected(_on_unit_unhovered):   
		unit.unit_unhovered.connect(_on_unit_unhovered)
	if !unit.unit_clicked.is_connected(_on_unit_clicked):   
		unit.unit_clicked.connect(_on_unit_clicked)

# Called when a child is added to terrain: auto-connect unit signals for friendly units
func _on_terrain_child_entered_tree(node: Node) -> void:
	var spawned_unit: Unit = node as Unit
	if spawned_unit != null and !spawned_unit.is_enemy:
		_unit_connect(node as Unit)

# Returns a list of predefined highground positions on the map
func get_highgrounds() -> Array[Vector2]:
	var highground_list: Array[Vector2]
	highground_list.append(Vector2(797.0, 290.0))
	highground_list.append(Vector2(842.0, 353.0))
	highground_list.append(Vector2(907.0, 413.0))
	highground_list.append(Vector2(1167.0, 354.0))
	highground_list.append(Vector2(1257.0, 351.0))
	highground_list.append(Vector2(1317.0, 420.0))
	return highground_list

# Convenience: get all enemy units
func get_enemy_units() -> Array[Unit]:
	return get_faction_units(true)
	
# Convenience: get all friendly units
func get_my_units() -> Array[Unit]:
	return get_faction_units(false)
	
# Extracts world positions from a list of units
func get_unit_positions(unit_list: Array[Unit]) -> Array[Vector2]:
	var position_list: Array[Vector2]
	for unit in unit_list:
		position_list.append(unit.position)
	return position_list
	
# Returns up to `size` nearest units to `own_unit` from `unit_list`
func find_nearest_units(own_unit: Unit, unit_list: Array[Unit], size: int) -> Array[Unit]:
	var nearest_list: Array[Unit]
	for check_unit in unit_list:
		if(check_unit != own_unit): #Don't add your own unit as nearest
			#Add so the nearest is first
			for i: int in nearest_list.size():
				if(i >= size):
					break
				var check_distance = own_unit.position.distance_to(check_unit.position)
				#TODO: Save the distance in the array to avoid recalculating it all the time 
				if check_distance < own_unit.position.distance_to(nearest_list[i].position):
					nearest_list.insert(i, check_unit)
					print("added a near unit")
					break
			if(nearest_list.size() == 0):
				nearest_list.insert(0, check_unit)
			if(nearest_list.size() > size):
				nearest_list.resize(size)
	return nearest_list
	
# Returns units filtered by enemy flag (true for enemies)
func get_faction_units(enemy: bool) -> Array[Unit]:
	var enemy_list: Array[Unit]
	for child in terrain.get_children():
		if child is Unit:
			var unit = (child as Unit)
			if unit.is_enemy == enemy:
				enemy_list.append(unit)
	return enemy_list
