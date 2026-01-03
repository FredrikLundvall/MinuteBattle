class_name DeckBuilder extends Node2D

@onready var swedish_pool_scene: PackedScene = preload("res://godot/pool/sweden_pool.tscn")
@onready var card_scene: PackedScene = preload("res://godot/card/card.tscn")
@onready var my_player_state = GameState.get_node("MyPlayerState")
@onready var my_deck = get_node("MyDeck")
@onready var holy_roman_empire_pool_scene: PackedScene = preload("res://godot/pool/holy_roman_empire_pool.tscn")
@onready var enemy_player_state = GameState.get_node("EnemyPlayerState")
@onready var enemy_deck: Deck = get_node("EnemyDeck")

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	_on_create_my_deck_btn_pressed()
	_on_create_enemy_deck_btn_pressed()

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(_delta: float) -> void:
	pass

func _input(event):
	if event.is_action_pressed("exit_click"):
		get_tree().quit()

func _on_create_my_deck_btn_pressed() -> void:
	var rng = RandomNumberGenerator.new()
	var deck_size = rng.randi_range(5, 11)
	_generate_my_new_deck(deck_size)

func _generate_my_new_deck(deck_size: int) -> void:
	Utils.remove_all_children(my_deck)
	var swedish_pool_instance = swedish_pool_scene.instantiate()
	for t in deck_size:
		var card: Card = swedish_pool_instance.get_random_card().duplicate()
		my_deck.add_child(card)
		print(card.title + ": " + str(card.resource))
	swedish_pool_instance.queue_free()

func _on_add_my_customized_card_btn_pressed() -> void:
	var rng = RandomNumberGenerator.new()
	var spawn_card = card_scene.instantiate()
	spawn_card.title = "Caroleans"
	spawn_card.resource = rng.randi_range(3, 7)
	spawn_card.picture = preload("res://hero/projectile.png")
	spawn_card.get_node("Unit").picture = preload("res://hero/projectile.png")
	my_deck.add_child(spawn_card)
	print(spawn_card.title)

func _on_create_enemy_deck_btn_pressed() -> void:
	var rng = RandomNumberGenerator.new()
	var deck_size = rng.randi_range(3, 7)
	_generate_enemy_new_deck(deck_size)

func _generate_enemy_new_deck(deck_size: int) -> void:
	Utils.remove_all_children(enemy_deck)
	var holy_roman_empire_pool_instance = holy_roman_empire_pool_scene.instantiate()
	for t in deck_size:
		var card: Card = holy_roman_empire_pool_instance.get_random_card().duplicate()
		enemy_deck.add_child(card)
		print(card.title + ": " + str(card.resource))
	holy_roman_empire_pool_instance.queue_free()

func _on_add_enemy_customized_card_btn_pressed() -> void:
	var rng = RandomNumberGenerator.new()
	var spawn_card = card_scene.instantiate()
	spawn_card.title = "Arquebusier"
	spawn_card.resource = rng.randi_range(4, 6)
	spawn_card.picture = preload("res://enemy/projectile.png")
	spawn_card.get_node("Unit").picture = preload("res://enemy/projectile.png")
	enemy_deck.add_child(spawn_card)
	print(spawn_card.title)

func _on_start_battle_btn_pressed() -> void:
	#Save the players deck in the PlayerState
	my_deck.reparent(my_player_state)
	#Set the resources for the player
	my_player_state.army_resource = 90
	my_player_state.reinforcement_speed = 10
	my_player_state.camp_resource = 14
	#Save the enemies deck in the PlayerState
	enemy_deck.reparent(enemy_player_state)
	#Set the resources for the enemy
	enemy_player_state.army_resource = 50
	enemy_player_state.reinforcement_speed = 14
	enemy_player_state.camp_resource = 24
	get_tree().change_scene_to_file("res://godot/play_ground/play_ground.tscn")
