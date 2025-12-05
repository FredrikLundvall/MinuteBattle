class_name Hand extends Node2D

signal card_selected(card: Card)

const CARD_HEIGHT = 40

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(_delta: float) -> void:
	pass
	
# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	re_position()
	for child in get_children():
		var card = (child as Card)
		card_connect(card)

func re_position():
	var y = 0
	for child in get_children():
		var card = (child as Card)
		card.position = Vector2(0,y)
		y += CARD_HEIGHT

func _on_child_entered_tree(node: Node) -> void:
	var card = (node as Card)
	card_connect(card)
	re_position()

func _on_child_exiting_tree(_node: Node) -> void:
	re_position()

func _on_card_focused(card: Card) -> void:
	card.focused = true
	highlight_last_focused_card()

func _on_card_unfocused(card: Card) -> void:
	card.focused = false
	card.highlighted = false
	highlight_last_focused_card()
	
func _on_card_clicked(card: Card) -> void:
	remove_child(card)
	card_selected.emit(card)
	re_position()
	#TODO this card has to be freed by the consumer of the signal
	card.queue_free()

func card_connect(card: Card):
	if !card.card_focused.is_connected(_on_card_focused):   
		card.card_focused.connect(_on_card_focused)
	if !card.card_unfocused.is_connected(_on_card_unfocused):   
		card.card_unfocused.connect(_on_card_unfocused)
	if !card.card_clicked.is_connected(_on_card_clicked):   
		card.card_clicked.connect(_on_card_clicked)

func highlight_last_focused_card():
	var last_focused_card: Card = null
	for child in get_children():
		var card = (child as Card)
		if card.focused:
			card.highlighted = true
			if last_focused_card == null:
				last_focused_card = card
			else:
				last_focused_card.highlighted = false


func _on_tree_exiting() -> void:
	print_orphan_nodes()
