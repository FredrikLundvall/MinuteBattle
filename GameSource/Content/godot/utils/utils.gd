extends Node


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
