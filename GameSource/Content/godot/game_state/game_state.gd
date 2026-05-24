extends Node

const MOVEMENT_MULTIPLIER: float = 30
const MOVEMENT_DURATION: float = 1.3

var movement_phase: bool = false

func start_movement_phase():
	movement_phase = true
	$MovementPhase.start(MOVEMENT_DURATION)

func _on_movement_phase_timeout() -> void:
	movement_phase = false
	
func rescale_position(position_vector: Vector2) -> Vector2:
	# Legacy alias kept for compatibility: converts pixel coordinates to movement units
	return GameState.pixels_to_movement_units(position_vector)

# Movement scaling helpers
# MOVEMENT_MULTIPLIER is the number of pixels per "movement unit".
# Use these helpers to convert between movement units (game logic) and pixels (world coordinates).
func movement_units_to_pixels(vec: Vector2) -> Vector2:
	return vec * MOVEMENT_MULTIPLIER

func pixels_to_movement_units(vec: Vector2) -> Vector2:
	return vec / MOVEMENT_MULTIPLIER

func movement_length_to_pixels(length: float) -> float:
	return length * MOVEMENT_MULTIPLIER
