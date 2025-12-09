class_name Battle extends Node2D

@onready var terrain: TileMapLayer = $Background/Terrain
@onready var meleePicture: Texture2D = preload("res://hero/melee.png")
@onready var marker_scene: PackedScene = preload("res://godot/marker/marker.tscn")
@onready var marker_layer: Node2D = $Background/MarkerLayer

signal unit_selected(unit: Unit)

const SPAWN_COORDINATES = Vector2i(0,3)
const SPAWN_ATLAS_TILE = Vector2i(0,0)

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
	unit_selected.emit(unit)

func _unit_connect(unit: Unit):
	if !unit.unit_hovered.is_connected(_on_unit_hovered):   
		unit.unit_hovered.connect(_on_unit_hovered)
	if !unit.unit_unhovered.is_connected(_on_unit_unhovered):   
		unit.unit_unhovered.connect(_on_unit_unhovered)
	if !unit.unit_clicked.is_connected(_on_unit_clicked):   
		unit.unit_clicked.connect(_on_unit_clicked)

func _on_terrain_child_entered_tree(node: Node) -> void:
	_unit_connect(node as Unit)


func _on_map_area_input_event(_viewport: Node, event: InputEvent, _shape_idx: int) -> void:
	if event.is_action_pressed("mouse_click"):
		print("Mouse Click/Unclick at: ", event.position)
		print("Global position at: ", get_global_mouse_position())
		print("Local position at: ", get_local_mouse_position())
		print("Viewport Resolution is: ", _viewport.get_visible_rect().size)
		var marker = marker_scene.instantiate()
		marker.position = get_local_mouse_position() *2
		marker.get_node("Animation").play()
		marker_layer.add_child(marker)
