class_name Battle extends Node2D


@onready var terrain: TileMapLayer = $Background/Terrain
@onready var meleePicture: Texture2D = preload("res://hero/melee.png")

const SPAWN_COORDINATES = Vector2i(0,3)
const SPAWN_ATLAS_TILE = Vector2i(0,0)

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	#Set spawn point
	terrain.set_cell(SPAWN_COORDINATES,0,SPAWN_ATLAS_TILE,0)

func spawn_unit():
	var melee: Sprite2D = Sprite2D.new()
	melee.texture = meleePicture
	melee.position = terrain.map_to_local(SPAWN_COORDINATES)
	terrain.add_child(melee)
