extends Area2D


# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	mouse_entered.connect(_on_area_2d_mouse_entered)

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	pass


func _on_area_2d_mouse_entered() -> void:
	print("what")

func _on_area_2d_input_event(viewport: Node, event: InputEvent, shape_idx: int) -> void:
	print("what!")
