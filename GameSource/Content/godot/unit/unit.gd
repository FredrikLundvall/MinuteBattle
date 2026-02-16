@tool
class_name Unit extends Node2D

@export var movement_distance: int = 3
@export var picture: Texture2D = preload("res://hero/melee.png")
@export var is_movement_visible: bool = false

var is_hovered: bool = false
var is_highlighted: bool = false
var is_selected: bool = false
@export_storage var marker: Marker = null
@export_storage var is_enemy: bool = false
@onready var picture_spr: Sprite2D = $Picture
@onready var movement_spr: Sprite2D = $Movement


signal unit_hovered(unit: Unit)
signal unit_unhovered(unit: Unit)
signal unit_clicked(unit: Unit)

const MOVEMENT_MULTIPLIER: float = 30

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	picture_spr.texture = picture

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(_delta: float) -> void:
	if is_highlighted or is_selected:
		highlight()
	else:
		unhighlight()
	if is_movement_visible != null && movement_spr != null:
		movement_spr.visible = is_movement_visible
		if is_movement_visible and is_selected:
			var distance = movement_spr.position.distance_to(movement_spr.get_parent().get_local_mouse_position())
			distance = clampf(distance, 0, movement_distance * MOVEMENT_MULTIPLIER)
			movement_spr.scale.y = distance / movement_spr.texture.get_height()
			movement_spr.look_at(movement_spr.get_global_mouse_position())
			movement_spr.rotation += PI / 2

func highlight():
	if picture_spr == null:
		return
	picture_spr.set_modulate(Color(0.8,0.7,0.6, 1))
	
func unhighlight():
	if picture_spr == null:
		return
	picture_spr.set_modulate(Color(1,1,1, 1))

func _on_input_event(_viewport: Node, event: InputEvent, _shape_idx: int) -> void:
	if event.is_action_pressed("mouse_click"):
		unit_clicked.emit(self)

func _on_mouse_entered() -> void:
	is_hovered = true
	unit_hovered.emit(self)

func _on_mouse_exited() -> void:
	is_hovered = false
	unit_unhovered.emit(self)
