extends Node
const TOAST = preload("res://godot/toast/toast.tscn")
const NOTIFICATION_NODE_NAME = "NotificationLayer"
const MAX_FLOAT = 99999.0

# Called when the node enters the scene tree for the first time.
# Initialize utils: create a notification canvas layer for toasts
func _ready() -> void:
	var notifaction_layer = CanvasLayer.new()
	notifaction_layer.name = NOTIFICATION_NODE_NAME
	notifaction_layer.layer = 2
	add_child(notifaction_layer)

# Called every frame. 'delta' is the elapsed time since the previous frame.
# Per-frame hook for utils (unused placeholder)
func _process(_delta: float) -> void:
	pass

# Frees all child nodes of the given node
func remove_all_children(node: Node) -> void:
	for child in node.get_children():
		child.free()
		
# Hides children that expose a `visible` property
func hide_all_children(node: Node) -> void:
	for child in node.get_children():
		if "visible" in child:
			child.visible = false

# Duplicates all children from `from_node` into `to_node`
func duplicate_all_children(from_node: Node, to_node: Node) -> void:
	for child in from_node.get_children():
		to_node.add_child(child.duplicate())

# Shows a toast notification at `position` for `duration` seconds and fades it out
func show_toast(message: String, position: Vector2, duration: float, fade: float = 0) -> void:
	var notification_layer = get_node(NOTIFICATION_NODE_NAME)
	#Hide any old notifications
	hide_all_children(notification_layer)
	#Spawn the new notification
	var toastNode = TOAST.instantiate()
	notification_layer.add_child(toastNode)
	toastNode.show_toast(message, position)
	#Add the timer for the notification to be shown
	await get_tree().create_timer(duration).timeout
	#Add the tween for the notification to be faded out
	var tween := toastNode.create_tween()
	tween.tween_property(toastNode, "modulate:a", 0, fade)
	tween.play()
	await tween.finished
	tween.kill()
	toastNode.queue_free()

# Returns the point from `point_list` closest to `position`
func find_nearest_point(position: Vector2, point_list: Array[Vector2]) -> Vector2:
	var nearest_point: Vector2
	var nearest_distance: float = MAX_FLOAT
	for point: Vector2 in point_list:
		var point_distance = position.distance_to(point)
		if point_distance < nearest_distance:
			nearest_distance = point_distance
			nearest_point = point

	return nearest_point
	
# Calculates the centroid (average) of the provided points
func calc_centroid_point(point_list: Array[Vector2]) -> Vector2:
	var sum_x: float = 0.0
	var sum_y: float = 0.0
	for point: Vector2 in point_list:
		sum_x += point.x
		sum_y += point.y
	return Vector2(sum_x/point_list.size(), sum_y/point_list.size())

# Adds a small random offset to position for variety when spawning
func randomize_position(position: Vector2) -> Vector2:
	var rng = RandomNumberGenerator.new()
	return position + Vector2(rng.randf_range(-10.0, 10.0),rng.randf_range(-10.0, 10.0))
