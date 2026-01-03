extends Node2D

var toast_pos: Vector2
@onready var background_trct: TextureRect = $Background
@onready var message_lbl: Label = $MessageLbl

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	pass

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(_delta: float) -> void:
	_set_size_and_position()

func show_toast(message: String, pos: Vector2) -> void:
	message_lbl.text = message
	toast_pos = pos

func _set_size_and_position() -> void:
	if background_trct == null:
		return
	background_trct.size.x = message_lbl.size.x + 16
	background_trct.size.y = message_lbl.size.y + 16
	position.x = toast_pos.x
	position.y = toast_pos.y
