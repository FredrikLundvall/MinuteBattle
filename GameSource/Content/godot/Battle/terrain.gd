class_name Terrain extends TileMapLayer

const SPAWN_COORDINATES = Vector2i(0,3)
const SPAWN_ENEMY_COORDINATES = Vector2i(24,9)
const SPAWN_ATLAS_TILE = Vector2i(0,0)

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	pass # Replace with function body.

func _use_tile_data_runtime_update(coords: Vector2i) -> bool:
	if coords == SPAWN_COORDINATES || coords == SPAWN_ENEMY_COORDINATES:
		return true
	return false

func _tile_data_runtime_update(coords: Vector2i, tile_data: TileData) -> void:
	if coords == SPAWN_COORDINATES:
		tile_data.modulate = Color(0,0,1.6)
	if coords == SPAWN_ENEMY_COORDINATES:
		tile_data.modulate = Color(1.8,0,0)
