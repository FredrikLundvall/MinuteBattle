extends Node


# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	pass # Replace with function body.

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(_delta: float) -> void:
	pass

func play_cards(hand: Hand, player_state: PlayerState, battle: Battle) -> void:
	for i in hand.get_child_count() * 2:
		var lowest_resource: int = hand.lowest_resource()
		if player_state.camp_resource < lowest_resource:
			break
		var card: Card = hand.get_children().pick_random()
		if player_state.camp_resource < card.resource:
			continue
		if(player_state.camp_resource < card.resource):
			print("Not enough resources to play enemy card: " + card.title)
			return
		hand.card_played(card)
		var spawn_unit = card.get_node("Unit").duplicate()
		spawn_unit.is_enemy = true
		#Reduce the resource count
		player_state.camp_resource -= card.resource
		if EngineDebugger.is_active(): 
			#This is only shown for the testing
			spawn_unit.visible = true
		battle.spawn_enemy_unit(spawn_unit)
		print("Playing enemy card: " + card.title)
