extends Node

var current_card_id = 0

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	get_tree().change_scene_to_file("res://scenes/main_menu.tscn")
	get_node("Battle").Visibility = false

func setCurrentCard(card_id: int):
	current_card_id = card_id
