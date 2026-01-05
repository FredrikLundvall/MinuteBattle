class_name Resources extends Node2D

@onready var army_lbl = get_node("ArmyValue")
@onready var reinforcements_lbl = get_node("ReinforcementsValue")
@onready var camp_lbl = get_node("CampValue")
@onready var gold_lbl = get_node("GoldValue")

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	pass # Replace with function body.

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(_delta: float) -> void:
	pass

func update_gui(player_state: PlayerState):
	army_lbl.text = str(player_state.army_resource)
	reinforcements_lbl.text = "+" + str(player_state.reinforcement_speed)
	camp_lbl.text = str(player_state.camp_resource)
	gold_lbl.text = str(player_state.gold_resource)
