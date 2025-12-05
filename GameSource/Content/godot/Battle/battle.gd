class_name Battle extends Node2D


@onready var terrain: TileMapLayer = $Background/Terrain
@onready var meleePicture: Texture2D = preload("res://hero/melee.png")

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
