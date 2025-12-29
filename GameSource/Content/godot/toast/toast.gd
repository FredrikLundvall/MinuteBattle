extends CanvasLayer

#@onready var background: TextureRect = $Background
#@onready var message_lbl: Label = $MessageLbl
var toast_position: Vector2

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	#show_toast("Test", Vector2(500,200))
	pass

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(_delta: float) -> void:
	_set_size_and_position()

func show_toast(message: String, position: Vector2) -> void:
	$MessageLbl.text = message
	toast_position = position

func _set_size_and_position() -> void:
	$Background.size.x = $MessageLbl.size.x + 16
	$Background.size.y = $MessageLbl.size.y + 16
	$Background.position.x = toast_position.x #- ($Background.size.x) / 2.0
	$Background.position.y = toast_position.y #- ($Background.size.y) / 2.0
	$MessageLbl.position.x = $Background.position.x + 8
	$MessageLbl.position.y = $Background.position.y + 8
