class_name Hand extends Node2D

const CARD_HEIGHT = 60

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	re_position()

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	pass

func re_position():
	var y = 0
	for child in get_children():
		var card = (child as Node2D)
		card.position = Vector2(0,y)
		y += CARD_HEIGHT
	


func _on_child_entered_tree(node: Node) -> void:
	re_position()


func _on_child_exiting_tree(node: Node) -> void:
	re_position()
