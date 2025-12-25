class_name PlayGround extends Node2D

@onready var card_scene: PackedScene = preload("res://godot/card/card.tscn")
@onready var hand: Hand = $CanvasLayer/Hand
@onready var battle: Node2D = $CanvasLayer/Battle
@onready var playerstate = GameState.get_node("PlayerState")
@onready var deck = playerstate.get_node("Deck")

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	Utils.remove_all_children(hand)
	Utils.duplicate_all_children(deck, hand)
	if !hand.card_selected.is_connected(_on_card_selected):   
		hand.card_selected.connect(_on_card_selected)

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(_delta: float) -> void:
	pass

func _on_draw_from_deck_button_pressed() -> void:
	var drawn_card = deck.get_children().pick_random()
	hand.add_child(drawn_card.duplicate())

func _on_card_selected(card: Card) -> void:
	var spawn_unit = card.get_node("Unit").duplicate()
	spawn_unit.visible = true
	battle.spawn_unit(spawn_unit)

func _input(event):
	if event.is_action_pressed("exit_click"):
		get_tree().quit()
