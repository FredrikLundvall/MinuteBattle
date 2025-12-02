extends Node2D

@onready var card_scene: PackedScene = preload("res://godot/card/card.tscn")
@onready var hand: Hand = $CanvasLayer/Hand
@onready var battle: Node2D = $CanvasLayer/Battle

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	if !hand.card_selected.is_connected(_on_card_selected):   
		hand.card_selected.connect(_on_card_selected)

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(_delta: float) -> void:
	pass

func _on_button_pressed() -> void:
	var spawn_card = card_scene.instantiate()
	spawn_card.title = "Test 1"
	spawn_card.price = 1
	spawn_card.picture = preload("res://hero/artillery.png")
	hand.add_child(spawn_card)


func _on_button_2_pressed() -> void:
	var spawn_card = card_scene.instantiate()
	spawn_card.title = "Test 2"
	spawn_card.price = 2
	spawn_card.picture = preload("res://hero/projectile.png")
	hand.add_child(spawn_card)

func _on_card_selected(card: Card) -> void:
	print(card.name)
	battle.spawn_unit()
	pass
