class_name Unit extends Node2D

@export var picture: Texture2D = preload("res://hero/melee.png")
@export var hovered: bool = false
@export var highlighted: bool = false
@export var selected: bool = false
@export var marker: Marker = null

signal unit_hovered(unit: Unit)
signal unit_unhovered(unit: Unit)
signal unit_clicked(unit: Unit)

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	$Picture.texture = picture

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(_delta: float) -> void:
	if highlighted or selected:
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

func _on_mouse_entered() -> void:
	hovered = true
	unit_hovered.emit(self)

func _on_mouse_exited() -> void:
	hovered = false
	unit_unhovered.emit(self)
