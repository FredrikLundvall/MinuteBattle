class_name DeckBuilder extends Node2D

@onready var swedish_pool_scene: PackedScene = preload("res://godot/pool/sweden_pool.tscn")
@onready var card_scene: PackedScene = preload("res://godot/card/card.tscn")
@onready var player_state = GameState.get_node("PlayerState")
@onready var my_deck = get_node("MyDeck")

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	_on_create_deck_btn_pressed()

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(_delta: float) -> void:
	pass

func _input(event):
	if event.is_action_pressed("exit_click"):
		get_tree().quit()

func _on_battle_btn_pressed() -> void:
	#Save the players deck in the PlayerState
	my_deck.reparent(player_state)
	#Set the resources for the player
	player_state.army_resource = 90
	player_state.reinforcement_speed = 10
	player_state.camp_resource = 14
	get_tree().change_scene_to_file("res://godot/play_ground/play_ground.tscn")

func _on_create_deck_btn_pressed() -> void:
	var rng = RandomNumberGenerator.new()
	var deck_size = rng.randi_range(5, 11)
	_generate_new_deck(deck_size)

func _generate_new_deck(deck_size: int) -> void:
	Utils.remove_all_children(my_deck)
	var swedish_pool_instance = swedish_pool_scene.instantiate()
	for t in deck_size:
		var card: Card = swedish_pool_instance.get_random_card().duplicate()
		my_deck.add_child(card)
		print(card.title + ": " + str(card.resource))
	swedish_pool_instance.queue_free()

func _on_add_customized_card_btn_pressed() -> void:
	var rng = RandomNumberGenerator.new()
	var spawn_card = card_scene.instantiate()
	spawn_card.title = "Caroleans"
	spawn_card.resource = rng.randi_range(3, 7)
	spawn_card.picture = preload("res://hero/projectile.png")
	spawn_card.get_node("Unit").picture = preload("res://hero/projectile.png")
	my_deck.add_child(spawn_card)
	print(spawn_card.title)
