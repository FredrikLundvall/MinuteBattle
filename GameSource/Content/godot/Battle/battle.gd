class_name Battle extends Node2D

@onready var terrain: TileMapLayer = $Terrain
@onready var meleePicture: Texture2D = preload("res://hero/melee.png")
@onready var marker_scene: PackedScene = preload("res://godot/marker/marker.tscn")

signal unit_selected(unit: Unit)

const SPAWN_COORDINATES = Vector2i(0,3)
const SPAWN_ATLAS_TILE = Vector2i(0,0)
const TERRAIN_OFFSET = Vector2(10,10)

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	#Set spawn point
	terrain.set_cell(SPAWN_COORDINATES,0,SPAWN_ATLAS_TILE,0)

func spawn_unit(unit: Unit):
	unit.position = terrain.map_to_local(SPAWN_COORDINATES)
	var rng = RandomNumberGenerator.new()
	unit.position += Vector2(rng.randf_range(-10.0, 10.0),rng.randf_range(-10.0, 10.0))
	terrain.add_child(unit)
	
func _on_unit_hovered(unit: Unit) -> void:
	unit.highlighted = true

func _on_unit_unhovered(unit: Unit) -> void:
	unit.highlighted = false
	
func _on_unit_clicked(unit: Unit) -> void:
	print(unit.name + " clicked")
	unit.selected = true
	unit_selected.emit(unit)

func _on_map_area_input_event(_viewport: Node, event: InputEvent, _shape_idx: int) -> void:
	if event.is_action_pressed("mouse_click") and _has_any_unit_selected() and not _is_any_unit_hovered():
		var marker = marker_scene.instantiate()
		marker.position = get_local_mouse_position() - TERRAIN_OFFSET
		marker.get_node("Animation").play()
		terrain.add_child(marker)
		_set_marker_for_selected_units_and_remove_old(marker)
		_unselect_all_units()

func _set_marker_for_selected_units_and_remove_old(marker: Marker) -> void:
	var markers_to_remove: Array[Node] = []
	for unit in terrain.get_children().filter(func(c): return _is_unit_and_selected(c)):
		if unit.marker != null and not markers_to_remove.has(unit.marker):
			markers_to_remove.append(unit.marker)
		unit.marker = marker
	Utils.show_toast("Orders to move out, ready the troop comander! \nMove towards the flag and make haste!", marker.position, 2.5)
	_remove_nodes(markers_to_remove)

func _is_unit_and_selected(node: Node) -> bool:
	if node is Unit: 
		return node.selected
	else: 
		return false

func _remove_nodes(nodes: Array[Node]) -> void:
	for node in nodes:
		node.queue_free()

func _has_any_unit_selected() -> bool:
	for child in terrain.get_children():
		if _is_unit_and_selected(child):
			return true
	return false
	
func _unselect_all_units():
	for child in terrain.get_children():
		if child is Unit:
			var unit = (child as Unit)
			unit.selected = false

func _is_any_unit_hovered() -> bool:
	for child in terrain.get_children():
		if child is Unit:
			var unit = (child as Unit)
			if unit.hovered:
				return true
	return false

func _unit_connect(unit: Unit):
	if !unit.unit_hovered.is_connected(_on_unit_hovered):   
		unit.unit_hovered.connect(_on_unit_hovered)
	if !unit.unit_unhovered.is_connected(_on_unit_unhovered):   
		unit.unit_unhovered.connect(_on_unit_unhovered)
	if !unit.unit_clicked.is_connected(_on_unit_clicked):   
		unit.unit_clicked.connect(_on_unit_clicked)

func _on_terrain_child_entered_tree(node: Node) -> void:
	if node is Unit:
		_unit_connect(node as Unit)
