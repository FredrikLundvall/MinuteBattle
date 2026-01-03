extends Node
const TOAST = preload("res://godot/toast/toast.tscn")
const NOTIFICATION_NODE_NAME = "NotificationLayer"

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	var notifaction_layer = CanvasLayer.new()
	notifaction_layer.name = NOTIFICATION_NODE_NAME
	notifaction_layer.layer = 2
	add_child(notifaction_layer)

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(_delta: float) -> void:
	pass

func remove_all_children(node: Node) -> void:
	for child in node.get_children():
		child.free()
		
func hide_all_children(node: Node) -> void:
	for child in node.get_children():
		if "visible" in child:
			child.visible = false

func duplicate_all_children(from_node: Node, to_node: Node) -> void:
	for child in from_node.get_children():
		to_node.add_child(child.duplicate())

func show_toast(message: String, position: Vector2, duration: float) -> void:
	var notification_layer = get_node(NOTIFICATION_NODE_NAME)
	#Hide any old notifications
	hide_all_children(notification_layer)
	#Spawn the new notification
	var toastNode = TOAST.instantiate()
	notification_layer.add_child(toastNode)
	toastNode.show_toast(message, position)
	#Add the timer for the notification to disapear
	await get_tree().create_timer(duration).timeout
	toastNode.queue_free()
