extends Node3D

func _ready():
	pass

var dir = 0

func _physics_process(delta):
	$Marker3D.rotate_y(0.001)
	if Input.is_action_pressed("spawn_anim"):
		$Ballista_T1_test.play_anim("spawn")
	elif Input.is_action_pressed("shoot_anim"):
		$Ballista_T1_test.play_anim("shoot", 2)
	elif Input.is_action_pressed("reload_anim"):
		$Ballista_T1_test.play_anim("reload", 0.5)
	if Input.is_action_just_pressed("rotate_left"):
		dir += 45
		if dir > 360:
			dir -= 360
		$Ballista_T1_test.dir_rot(dir)
	if Input.is_action_just_pressed("rotate_right"):
		dir -= 45
		if dir < 0:
			dir += 360
		$Ballista_T1_test.dir_rot(dir)
