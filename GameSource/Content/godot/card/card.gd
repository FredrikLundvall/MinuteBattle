#@tool
class_name Card extends Node2D

@export var title: String = "Title"
@export var price: int = 5
@export var picture: Texture2D = preload("res://hero/melee.png")

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	print("Ready")	
	var titleLbl: Label = $Background/Title
	titleLbl.text = title
	var priceLbl: Label = $Background/Price
	priceLbl.text = str(price)
	var pictureSpr: Sprite2D = $Background/Picture
	pictureSpr.texture = picture

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	pass

func card_selected():
	var background: Sprite2D = $Background
	background.set_modulate(Color(1,0.5,0.1, 1))
	
func card_unselected():
	var background: Sprite2D = $Background
	background.set_modulate(Color(1,1,1, 1))


func _on_area_2d_mouse_entered() -> void:
	print("heyyy")
