extends Node

#The move_towards_strategic_point() should, when used alone, end up at that point not circle around it. How to do that?
#The sum of the different mult should add up to 1.0
const STRATEGIC_MOVEMENT_MULT: float = 1.0 

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

func set_unit_movements(battle: Battle) -> void:
	var unit_list = battle.get_enemy_units()
	
	for unit in unit_list:
		var new_movement: Vector2 = Vector2.ZERO
		#Move towards a good position
		new_movement += move_towards_strategic_point(unit, battle)
		unit.set_pixel_movement(new_movement)
		unit.is_movement_visible = true
		#print("Setting enemy unit movements " + unit.name + " " + str(new_movement))

func move_towards_strategic_point(unit: Unit, battle: Battle) -> Vector2:
	var highground_list = battle.get_highgrounds()
	var nearest_highground_point = Utils.find_nearest_point(unit.get_pixel_position(), highground_list)
	return unit.limit_pixel_movement_distance((nearest_highground_point - unit.get_pixel_position())) * STRATEGIC_MOVEMENT_MULT
	
