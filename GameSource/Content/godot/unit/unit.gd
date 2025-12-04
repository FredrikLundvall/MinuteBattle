class_name Unit extends Node2D

@export var picture: Texture2D = preload("res://hero/melee.png")


# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	var pictureSpr: Sprite2D = $Picture
	pictureSpr.texture = picture


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(_delta: float) -> void:
	pass
