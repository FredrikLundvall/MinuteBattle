class_name Hand extends Node2D

signal card_selected(card: Card)

const CARD_HEIGHT = 40

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(_delta: float) -> void:
	pass

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	_re_position()
	for child in get_children():
		var card = (child as Card)
		_card_connect(card)

func _re_position():
	var y = 0
	for child in get_children():
		var card = (child as Card)
		card.position = Vector2(0,y)
		y += CARD_HEIGHT

func _on_child_entered_tree(node: Node) -> void:
	var card = (node as Card)
	_card_connect(card)
	_re_position()

func _on_child_exiting_tree(_node: Node) -> void:
	_re_position()

func _on_card_hovered(_card: Card) -> void:
	_highlight_last_hovered_card()

func _on_card_unhovered(card: Card) -> void:
	card.highlighted = false
	_highlight_last_hovered_card()
	
func _on_card_clicked(card: Card) -> void:
	remove_child(card)
	card_selected.emit(card)
	_re_position()
	#TODO this card has to be freed by the consumer of the signal
	card.queue_free()

func _card_connect(card: Card):
	if !card.card_hovered.is_connected(_on_card_hovered):   
		card.card_hovered.connect(_on_card_hovered)
	if !card.card_unhovered.is_connected(_on_card_unhovered):   
		card.card_unhovered.connect(_on_card_unhovered)
	if !card.card_clicked.is_connected(_on_card_clicked):   
		card.card_clicked.connect(_on_card_clicked)

func _highlight_last_hovered_card():
	var last_hovered_card: Card = null
	for child in get_children():
		var card = (child as Card)
		if card.hovered:
			card.highlighted = true
			if last_hovered_card == null:
				last_hovered_card = card
			else:
				last_hovered_card.highlighted = false
