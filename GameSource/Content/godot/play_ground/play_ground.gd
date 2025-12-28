class_name PlayGround extends Node2D

@onready var card_scene: PackedScene = preload("res://godot/card/card.tscn")
@onready var hand: Hand = $CanvasLayer/Hand
@onready var battle: Node2D = $CanvasLayer/Battle
@onready var player_state = GameState.get_node("PlayerState")
@onready var deck = player_state.get_node("Deck")
@onready var player_army_lbl = $CanvasLayer/PlayerResources/ArmyValue
@onready var player_reinforcements_lbl = $CanvasLayer/PlayerResources/ReinforcementsValue
@onready var player_camp_lbl = $CanvasLayer/PlayerResources/CampValue

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	Utils.remove_all_children(hand)
	Utils.duplicate_all_children(deck, hand)
	if !hand.card_selected.is_connected(_on_card_selected):   
		hand.card_selected.connect(_on_card_selected)

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(_delta: float) -> void:
	player_army_lbl.text = str(player_state.army_resource)
	player_reinforcements_lbl.text = str(player_state.reinforcement_speed)
	player_camp_lbl.text = str(player_state.camp_resource)

func _on_draw_from_deck_button_pressed() -> void:
	var drawn_card = deck.get_children().pick_random()
	hand.add_child(drawn_card.duplicate())

func _on_card_selected(card: Card) -> void:
	if(player_state.camp_resource < card.resource):
		Utils.show_toast("Not enough resources", card.position, 1.5)
		return
	hand.card_played(card)
	var spawn_unit = card.get_node("Unit").duplicate()
	#Reduce the resource count
	player_state.camp_resource -= card.resource
	spawn_unit.visible = true
	battle.spawn_unit(spawn_unit)
	Utils.show_toast(card.title, spawn_unit.position, 1.5)

func _input(event):
	if event.is_action_pressed("exit_click"):
		get_tree().quit()
