extends Node3D

func _ready():
	pass

var dir = 0

func _physics_process(delta):
	$Marker3D.rotate_y(0.001)
	if Input.is_action_pressed("spawn_anim"):
		$golem.play_anim("spawn", 2)
	elif Input.is_action_pressed("shoot_anim"):
		$golem.play_anim("idle", 1.5)
	elif Input.is_action_pressed("reload_anim"):
		$golem.play_anim("move")
	elif Input.is_action_pressed("4_anim"):
		$golem.play_anim("attack")
	elif Input.is_action_pressed("5_anim"):
		$golem.play_anim("death")
	elif Input.is_action_pressed("reset_anim"):
		$golem.play_anim("reset")
	if Input.is_action_just_pressed("rotate_left"):
		dir += 45
		if dir > 360:
			dir -= 360
		$golem.dir_rot(dir)
	if Input.is_action_just_pressed("rotate_right"):
		dir -= 45
		if dir < 0:
			dir += 360
		$golem.dir_rot(dir)
