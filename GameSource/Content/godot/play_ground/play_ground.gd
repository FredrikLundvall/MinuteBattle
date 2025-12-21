class_name PlayGround extends Node2D

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

func _on_draw_from_deck_button_pressed() -> void:
	var rng = RandomNumberGenerator.new()
	#TODO: Pick one from the deck
	var spawn_card = card_scene.instantiate()
	spawn_card.title = "12 pd Canon"
	spawn_card.price = rng.randi_range(2, 7)
	spawn_card.picture = preload("res://hero/artillery.png")
	spawn_card.get_node("Unit").picture = preload("res://hero/artillery.png")
	#print("artillery")
	hand.add_child(spawn_card)

func _on_card_selected(card: Card) -> void:
	var spawn_unit = card.get_node("Unit").duplicate()
	spawn_unit.visible = true
	battle.spawn_unit(spawn_unit)

func _input(event):
	if event.is_action_pressed("exit_click"):
		get_tree().quit()
