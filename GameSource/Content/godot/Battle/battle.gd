class_name Battle extends Node2D

@onready var terrain: TileMapLayer = $Terrain
@onready var meleePicture: Texture2D = preload("res://hero/melee.png")
@onready var map_title_lbl = get_node("MapTitle")
@onready var selected_unit: Unit = null

signal unit_selected(unit: Unit)

const BATTLE_TITLE_TXT = "North of Breitenfeldt"
const FLAG_MARKER_TXT = "Ready the troops Commander!\nMove towards that flag."

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	map_title_lbl.text = BATTLE_TITLE_TXT
	
func spawn_unit(unit: Unit):
	unit.position = terrain.map_to_local(terrain.SPAWN_COORDINATES)
	var rng = RandomNumberGenerator.new()
	unit.position += Vector2(rng.randf_range(-10.0, 10.0),rng.randf_range(-10.0, 10.0))
	terrain.add_child(unit)
	
func spawn_enemy_unit(unit: Unit):
	unit.position = terrain.map_to_local(terrain.SPAWN_ENEMY_COORDINATES)
	var rng = RandomNumberGenerator.new()
	unit.position += Vector2(rng.randf_range(-10.0, 10.0),rng.randf_range(-10.0, 10.0))
	terrain.add_child(unit)
	
func _on_unit_hovered(unit: Unit) -> void:
	unit.is_highlighted = true

func _on_unit_unhovered(unit: Unit) -> void:
	unit.is_highlighted = false
	
func _on_unit_clicked(unit: Unit) -> void:
	print(unit.name + " clicked")
	if selected_unit != null:
		selected_unit.is_selected = false
	unit.is_selected = true
	unit.movement_spr.rotation  = PI / 2
	unit.is_movement_visible = true
	selected_unit = unit
	unit_selected.emit(unit)

func _on_map_area_input_event(_viewport: Node, event: InputEvent, _shape_idx: int) -> void:
	if selected_unit != null:
		if event.is_action_pressed("mouse_click") and not _is_any_unit_hovered():
			selected_unit.is_selected = false
			selected_unit = null
		else:
			selected_unit.movement_spr.look_at(selected_unit.movement_spr.get_global_mouse_position())
			selected_unit.movement_spr.rotation += PI / 2

func _is_unit_and_selected(node: Node) -> bool:
	if node is Unit: 
		return node.is_selected
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
			unit.is_selected = false

func _is_any_unit_hovered() -> bool:
	for child in terrain.get_children():
		if child is Unit:
			var unit = (child as Unit)
			if unit.is_hovered:
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
	var spawned_unit: Unit = node as Unit
	if spawned_unit != null and !spawned_unit.is_enemy:
		_unit_connect(node as Unit)
