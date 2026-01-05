class_name PlayGround extends Node2D

@onready var card_scene: PackedScene = preload("res://godot/card/card.tscn")
@onready var battle: Node2D = get_node("Battle")

@onready var my_hand: Hand = get_node("MyHand")
@onready var my_player_state = GameState.get_node("MyPlayerState")
@onready var my_resources = get_node("MyResources")
@onready var my_deck = my_player_state.get_node("MyDeck")

@onready var enemy_hand: Hand = get_node("EnemyHand")
@onready var enemy_player_state = GameState.get_node("EnemyPlayerState")
@onready var enemy_resources = get_node("EnemyResources")
@onready var enemy_deck = enemy_player_state.get_node("EnemyDeck")

const NO_RESOURCES_TXT = "Not enough resources in camp"
const SPAWN_TXT = ", report to your Commander!"

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	Utils.remove_all_children(my_hand)
	Utils.duplicate_all_children(my_deck, my_hand)
	if !my_hand.card_selected.is_connected(_on_card_selected):   
		my_hand.card_selected.connect(_on_card_selected)

	Utils.remove_all_children(enemy_hand)
	Utils.duplicate_all_children(enemy_deck, enemy_hand)
	enemy_hand.no_highlight()

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(_delta: float) -> void:
	my_resources.update_gui(my_player_state)
	enemy_resources.update_gui(enemy_player_state)

func _on_draw_from_deck_button_pressed() -> void:
	var drawn_card = my_deck.get_children().pick_random()
	my_hand.add_child(drawn_card.duplicate())

func _on_card_selected(card: Card) -> void:
	if(my_player_state.camp_resource < card.resource):
		Utils.show_toast(NO_RESOURCES_TXT, get_global_mouse_position(), 0.8)
		return
	my_hand.card_played(card)
	var spawn_unit = card.get_node("Unit").duplicate()
	#Reduce the resource count
	my_player_state.camp_resource -= card.resource
	spawn_unit.visible = true
	battle.spawn_unit(spawn_unit)
	Utils.show_toast(card.title + SPAWN_TXT, battle.to_global(spawn_unit.position), 1.8)

func _input(event):
	if event.is_action_pressed("exit_click"):
		get_tree().quit()
