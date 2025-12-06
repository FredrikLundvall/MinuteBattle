class_name Unit extends Node2D

@export var picture: Texture2D = preload("res://hero/melee.png")
@export var highlighted: bool = false

signal unit_clicked(card: Unit)

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	var pictureSpr: Sprite2D = $Picture
	pictureSpr.texture = picture

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(_delta: float) -> void:
	if highlighted:
		highlight()
	else:
		unhighlight()

func highlight():
	$Picture.set_modulate(Color(0.8,0.7,0.6, 1))
	
func unhighlight():
	$Picture.set_modulate(Color(1,1,1, 1))

func _on_input_event(_viewport: Node, event: InputEvent, _shape_idx: int) -> void:
	if event.is_action_pressed("mouse_click"):
		unit_clicked.emit(self)
