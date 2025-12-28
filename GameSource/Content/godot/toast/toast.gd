extends CanvasLayer

#@onready var background: TextureRect = $Background
#@onready var message_lbl: Label = $MessageLbl

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	#show_toast("Test", Vector2(500,200))
	pass

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(_delta: float) -> void:
	pass

func show_toast(message: String, position: Vector2) -> void:
	offset = position
	$MessageLbl.text = message
	$Background.size.x = $MessageLbl.size.x + 8
	$Background.size.y = $MessageLbl.size.y + 8
	
	
