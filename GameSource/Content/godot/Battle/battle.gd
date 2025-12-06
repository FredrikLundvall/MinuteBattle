class_name Battle extends Node2D

@onready var terrain: TileMapLayer = $Background/Terrain
@onready var meleePicture: Texture2D = preload("res://hero/melee.png")

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
	unit_selected.emit(unit)

func unit_connect(unit: Unit):
	if !unit.unit_hovered.is_connected(_on_unit_hovered):   
		unit.unit_hovered.connect(_on_unit_hovered)
	if !unit.unit_unhovered.is_connected(_on_unit_unhovered):   
		unit.unit_unhovered.connect(_on_unit_unhovered)
	if !unit.unit_clicked.is_connected(_on_unit_clicked):   
		unit.unit_clicked.connect(_on_unit_clicked)

func _on_terrain_child_entered_tree(node: Node) -> void:
	unit_connect(node as Unit)
