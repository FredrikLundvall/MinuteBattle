extends Node

const MOVEMENT_MULTIPLIER: float = 30
const MOVEMENT_DURATION: float = 1.3

var movement_phase: bool = false

func start_movement_phase():
	movement_phase = true
	$MovementPhase.start(MOVEMENT_DURATION)

func _on_movement_phase_timeout() -> void:
	movement_phase = false
