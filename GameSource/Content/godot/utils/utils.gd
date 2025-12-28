extends Node
const TOAST = preload("res://godot/toast/toast.tscn")

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	pass # Replace with function body.

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(_delta: float) -> void:
	pass

func remove_all_children(node: Node) -> void:
	for child in node.get_children():
		child.free()

func duplicate_all_children(from_node: Node, to_node: Node) -> void:
	#to_node.get_children().assign(from_node.get_children().duplicate_deep())
	for child in from_node.get_children():
		to_node.add_child(child.duplicate())

func show_toast(message: String, position: Vector2, duration: float) -> void:
	var toastNode = TOAST.instantiate()
	toastNode.show_toast(message, position + Vector2(90,180))
	add_child(toastNode)
	await get_tree().create_timer(duration).timeout
	toastNode.queue_free()
