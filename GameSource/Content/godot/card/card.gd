@tool
class_name Card extends Node2D

@export var title: String = "Title"
@export var resource: int = 5
@export var gold: int = 3
@export var picture: Texture2D = preload("res://hero/melee.png")
@export var use_highlight: bool = true
var is_hovered: bool = false
var is_highlighted: bool = false
@onready var title_lbl: Label = $Background/Title
@onready var resource_lbl: Label = $Background/Resource
@onready var gold_lbl: Label = $Background/Gold
@onready var background_spr: Sprite2D = $Background
@onready var picture_spr: Sprite2D = $Background/Picture
@onready var unit_uni: Unit = $Unit

signal card_hovered(card: Card)
signal card_unhovered(card: Card)
signal card_clicked(card: Card)

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	title_lbl.text = title
	resource_lbl.text = str(resource)
	gold_lbl.text = str(gold)
	picture_spr.texture = picture
	unit_uni.picture = picture
	if Engine.is_editor_hint():
		unit_uni.visible = true
	else:
		unit_uni.visible = false

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(_delta: float) -> void:
	if is_highlighted and use_highlight:
		highlight()
	else:
		unhighlight()

func highlight():
	if background_spr == null:
		return
	background_spr.set_modulate(Color(0.8,0.7,0.6, 1))
	
func unhighlight():
	if background_spr == null:
		return
	background_spr.set_modulate(Color(1,1,1, 1))

func _on_area_mouse_entered() -> void:
	is_hovered = true
	card_hovered.emit(self)

func _on_area_mouse_exited() -> void:
	is_hovered = false
	card_unhovered.emit(self)

func _on_area_input_event(_viewport: Node, event: InputEvent, _shape_idx: int) -> void:
	if(event.is_action_pressed("mouse_click") and is_highlighted):
		card_clicked.emit(self)
