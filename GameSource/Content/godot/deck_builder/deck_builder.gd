class_name DeckBuilder extends Node2D

@onready var swedish_pool_scene: PackedScene = preload("res://godot/pool/sweden_pool.tscn")
@onready var card_scene: PackedScene = preload("res://godot/card/card.tscn")
@onready var my_player_state = GameState.get_node("MyPlayerState")
@onready var my_resources = get_node("MyResources")
@onready var my_deck = get_node("MyDeck")
@onready var holy_roman_empire_pool_scene: PackedScene = preload("res://godot/pool/holy_roman_empire_pool.tscn")
@onready var enemy_player_state = GameState.get_node("EnemyPlayerState")
@onready var enemy_resources = get_node("EnemyResources")
@onready var enemy_deck: Deck = get_node("EnemyDeck")

const RESOURCE_STOCK = 200
var my_gold: int
var enemy_gold: int

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	_on_reroll_my_stats_btn_pressed()
	_on_reroll_enemy_stats_btn_pressed()

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(_delta: float) -> void:
	pass

func _input(event):
	if event.is_action_pressed("exit_click"):
		get_tree().quit()

func _on_create_my_deck_btn_pressed() -> void:
	var rng = RandomNumberGenerator.new()
	var deck_size = rng.randi_range(7, 11)
	my_player_state.gold_resource = my_gold
	_generate_my_new_deck(deck_size)
	my_resources.update_gui(my_player_state)

func _generate_my_new_deck(deck_size: int) -> void:
	Utils.remove_all_children(my_deck)
	var swedish_pool_instance = swedish_pool_scene.instantiate()
	for t in deck_size:
		var card: Card = swedish_pool_instance.get_random_card()
		if my_player_state.gold_resource > card.gold:
			my_player_state.gold_resource -= card.gold
			var spawn_card: Card = card.duplicate()
			my_deck.add_child(spawn_card)
			print(spawn_card.title + ": " + str(spawn_card.resource))
	swedish_pool_instance.queue_free()

func _on_add_my_customized_card_btn_pressed() -> void:
	var rng = RandomNumberGenerator.new()
	var gold = rng.randi_range(5, 9)
	if my_player_state.gold_resource > gold:
		my_player_state.gold_resource -= gold
		var spawn_card = card_scene.instantiate()
		spawn_card.title = "Caroleans"
		spawn_card.resource = rng.randi_range(3, 7)
		spawn_card.gold = gold
		spawn_card.picture = preload("res://hero/projectile.png")
		spawn_card.get_node("Unit").picture = preload("res://hero/projectile.png")
		my_deck.add_child(spawn_card)
		print(spawn_card.title)
		my_resources.update_gui(my_player_state)

func _on_create_enemy_deck_btn_pressed() -> void:
	var rng = RandomNumberGenerator.new()
	var deck_size = rng.randi_range(7, 11)
	enemy_player_state.gold_resource = enemy_gold
	_generate_enemy_new_deck(deck_size)
	enemy_resources.update_gui(enemy_player_state)

func _generate_enemy_new_deck(deck_size: int) -> void:
	Utils.remove_all_children(enemy_deck)
	var holy_roman_empire_pool_instance = holy_roman_empire_pool_scene.instantiate()
	for t in deck_size:
		var card: Card = holy_roman_empire_pool_instance.get_random_card()
		if enemy_player_state.gold_resource > card.gold:
			enemy_player_state.gold_resource -= card.gold
			var spawn_card: Card = card.duplicate()
			enemy_deck.add_child(spawn_card)
			print(spawn_card.title + ": " + str(spawn_card.resource))
	holy_roman_empire_pool_instance.queue_free()

func _on_add_enemy_customized_card_btn_pressed() -> void:
	var rng = RandomNumberGenerator.new()
	var gold = rng.randi_range(5, 9)
	if enemy_player_state.gold_resource > gold:
		enemy_player_state.gold_resource -= gold
		var spawn_card = card_scene.instantiate()
		spawn_card.title = "Arquebusier"
		spawn_card.resource = rng.randi_range(4, 6)
		spawn_card.gold = gold
		spawn_card.picture = preload("res://enemy/projectile.png")
		spawn_card.get_node("Unit").picture = preload("res://enemy/projectile.png")
		enemy_deck.add_child(spawn_card)
		print(spawn_card.title)
		enemy_resources.update_gui(enemy_player_state)

func _on_start_battle_btn_pressed() -> void:
	#Save the players deck in the PlayerState
	my_deck.reparent(my_player_state)
	#Save the enemies deck in the PlayerState
	enemy_deck.reparent(enemy_player_state)
	get_tree().change_scene_to_file("res://godot/play_ground/play_ground.tscn")

func _reroll_player_stats(stats: PlayerState):
	var rng = RandomNumberGenerator.new()
	var remaining_points = RESOURCE_STOCK
	stats.army_resource = rng.randi_range(70, 100)
	remaining_points -= stats.army_resource
	stats.reinforcement_speed = rng.randi_range(5, 30)
	remaining_points -= stats.reinforcement_speed
	stats.camp_resource = rng.randi_range(10, 40)
	remaining_points -= stats.camp_resource
	stats.gold_resource = remaining_points

func _on_reroll_my_stats_btn_pressed() -> void:
	#Set the resources for the player
	_reroll_player_stats(my_player_state)
	my_gold = my_player_state.gold_resource
	my_resources.update_gui(my_player_state)
	_on_create_my_deck_btn_pressed()

func _on_reroll_enemy_stats_btn_pressed() -> void:
	#Set the resources for the enemy
	_reroll_player_stats(enemy_player_state)
	enemy_gold = enemy_player_state.gold_resource
	enemy_resources.update_gui(enemy_player_state)
	_on_create_enemy_deck_btn_pressed()
