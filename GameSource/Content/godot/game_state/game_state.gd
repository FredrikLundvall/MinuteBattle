extends Node

const MOVEMENT_MULTIPLIER: float = 30
const MOVEMENT_DURATION: float = 1.3

var movement_phase: bool = false

# Starts the movement phase timer and sets the phase flag
func start_movement_phase():
	movement_phase = true
	$MovementPhase.start(MOVEMENT_DURATION)

# Callback when movement phase timer expires; clears the movement phase flag
func _on_movement_phase_timeout() -> void:
	movement_phase = false
	
# Converts a pixel position to movement units (legacy alias)
func rescale_position(position_vector: Vector2) -> Vector2:
	# Legacy alias kept for compatibility: converts pixel coordinates to movement units
	return GameState.pixels_to_movement_units(position_vector)

# Movement scaling helpers
# MOVEMENT_MULTIPLIER is the number of pixels per "movement unit".
# Use these helpers to convert between movement units (game logic) and pixels (world coordinates).
# Convert a vector from movement units to world pixels
func movement_units_to_pixels(vec: Vector2) -> Vector2:
	return vec * MOVEMENT_MULTIPLIER

# Convert a vector from world pixels to movement units
func pixels_to_movement_units(vec: Vector2) -> Vector2:
	return vec / MOVEMENT_MULTIPLIER

# Convert a scalar movement length (units) to pixels
func movement_length_to_pixels(length: float) -> float:
	return length * MOVEMENT_MULTIPLIER
	
# Convert a scalar movement pixels to length 
func movement_pixels_to_length(length: float) -> float:
	return length / MOVEMENT_MULTIPLIER
