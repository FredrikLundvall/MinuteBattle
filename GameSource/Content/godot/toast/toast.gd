extends Node2D

var toast_pos: Vector2

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	pass

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(_delta: float) -> void:
	_set_size_and_position()

func show_toast(message: String, pos: Vector2) -> void:
	$MessageLbl.text = message
	toast_pos = pos

func _set_size_and_position() -> void:
	$Background.size.x = $MessageLbl.size.x + 16
	$Background.size.y = $MessageLbl.size.y + 16
	position.x = toast_pos.x
	position.y = toast_pos.y
