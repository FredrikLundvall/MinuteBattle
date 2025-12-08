#@tool
class_name Card extends Node2D

@export var title: String = "Title"
@export var price: int = 5
@export var picture: Texture2D = preload("res://hero/melee.png")
@export var hovered: bool = false
@export var highlighted: bool = false

signal card_hovered(card: Card)
signal card_unhovered(card: Card)
signal card_clicked(card: Card)

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	$Background/Title.text = title
	$Background/Price.text = str(price)
	$Background/Picture.texture = picture
	$Members/Unit.picture = picture

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(_delta: float) -> void:
	if highlighted:
		highlight()
	else:
		unhighlight()

func highlight():
	var background: Sprite2D = $Background
	background.set_modulate(Color(0.8,0.7,0.6, 1))
	
func unhighlight():
	var background: Sprite2D = $Background
	background.set_modulate(Color(1,1,1, 1))

func _on_area_mouse_entered() -> void:
	hovered = true
	card_hovered.emit(self)

func _on_area_mouse_exited() -> void:
	hovered = false
	card_unhovered.emit(self)

func _on_area_input_event(_viewport: Node, event: InputEvent, _shape_idx: int) -> void:
	if(event.is_action_pressed("mouse_click") and highlighted):
		card_clicked.emit(self)
