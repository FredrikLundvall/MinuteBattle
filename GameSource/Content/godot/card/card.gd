#@tool
class_name Card extends Node2D

@export var title: String = "Title"
@export var price: int = 5
@export var picture: Texture2D = preload("res://hero/melee.png")
@export var focused: bool = false
@export var highlighted: bool = false
#@export var unit: Unit #= preload("res://godot/unit/unit.tscn").instantiate()

signal card_focused(card: Card)
signal card_unfocused(card: Card)
signal card_clicked(card: Card)

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	#var unitScn: PackedScene = preload("res://godot/unit/unit.tscn")
	#unit = unitScn.instantiate()
	var titleLbl: Label = $Background/Title
	titleLbl.text = title
	var priceLbl: Label = $Background/Price
	priceLbl.text = str(price)
	var pictureSpr: Sprite2D = $Background/Picture
	pictureSpr.texture = picture

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

func _on_area_2d_mouse_entered() -> void:
	card_focused.emit(self)

func _on_area_2d_mouse_exited() -> void:
	card_unfocused.emit(self)

func _on_area_2d_input_event(_viewport: Node, event: InputEvent, _shape_idx: int) -> void:
	if(event.is_action_pressed("mouse_click") and highlighted):
		card_clicked.emit(self)
