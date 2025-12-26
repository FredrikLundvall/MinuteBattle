class_name Deck extends Node2D

const CARD_WIDTH = 100

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	_re_position()

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(_delta: float) -> void:
	pass

func _re_position():
	var x = 0 - (get_child_count() * CARD_WIDTH) / 2.0
	for child in get_children():
		var card = (child as Card)
		card.position = Vector2(x,0)
		x += CARD_WIDTH

func deck_is_changed() -> void:
	_re_position()

func _on_child_entered_tree(_node: Node) -> void:
	deck_is_changed()

func _on_child_exiting_tree(_node: Node) -> void:
	deck_is_changed()

func _on_child_order_changed() -> void:
	deck_is_changed()
