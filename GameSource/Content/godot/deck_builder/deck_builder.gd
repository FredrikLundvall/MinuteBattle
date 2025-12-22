class_name DeckBuilder extends Node2D

@onready var swedish_pool_scene: PackedScene = preload("res://godot/pool/swedish_pool.tscn")
@onready var card_scene: PackedScene = preload("res://godot/card/card.tscn")

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
	$CanvasLayer/Deck.reparent(GameState.get_node("PlayerState"))
	get_tree().change_scene_to_file("res://godot/play_ground/play_ground.tscn")

func _on_create_deck_btn_pressed() -> void:
	var rng = RandomNumberGenerator.new()
	var deck_size = rng.randi_range(5, 11)
	_generate_new_deck(deck_size)

func _generate_new_deck(deck_size: int) -> void:
	_remove_all_children($CanvasLayer/Deck)
	var swedish_pool_instance = swedish_pool_scene.instantiate()
	for t in deck_size:
		var card: Card = swedish_pool_instance.get_random_card().duplicate()
		$CanvasLayer/Deck.add_child(card)
		print(card.title)
	swedish_pool_instance.queue_free()

func _remove_all_children(node: Node) -> void:
	for child in node.get_children():
		child.free()

func _on_add_customized_card_btn_pressed() -> void:
	var rng = RandomNumberGenerator.new()
	var spawn_card = card_scene.instantiate()
	spawn_card.title = "Caroleans"
	spawn_card.price = rng.randi_range(3, 7)
	spawn_card.picture = preload("res://hero/projectile.png")
	spawn_card.get_node("Unit").picture = preload("res://hero/projectile.png")
	$CanvasLayer/Deck.add_child(spawn_card)
	print(spawn_card.title)
