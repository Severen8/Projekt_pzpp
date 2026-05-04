extends Node3D

func _ready():
	pass

var dir = 0

func _physics_process(delta):
	#test_func($Ballistas)
	test_func($cannons)
	'''
	$Marker3D.rotate_y(0.001)
	if Input.is_action_pressed("spawn_anim"):
		$Ballistas.play_anim("spawn")
	elif Input.is_action_pressed("shoot_anim"):
		$Ballistas.play_anim("shoot", 2)
	elif Input.is_action_pressed("reload_anim"):
		$Ballistas.play_anim("reload", 0.5)
	if Input.is_action_just_pressed("rotate_left"):
		dir += 45
		if dir > 360:
			dir -= 360
			if $Ballistas.get_meta("Tier") < 4:
				$Ballistas.update_tier($Ballistas.get_meta("Tier")+1)
		$Ballistas.dir_rot(dir)
	if Input.is_action_just_pressed("rotate_right"):
		dir -= 45
		if dir < 0:
			dir += 360
			if $Ballistas.get_meta("Tier") > 1:
				$Ballistas.update_tier($Ballistas.get_meta("Tier")-1)
		$Ballistas.dir_rot(dir)
	'''

func test_func(obj):
	$Marker3D.rotate_y(0.001)
	if Input.is_action_pressed("spawn_anim"):
		obj.play_anim("spawn")
	elif Input.is_action_pressed("shoot_anim"):
		if obj == $Ballistas:
			obj.play_anim("shoot", 2)
		else:
			obj.play_anim("ready", 2)
	elif Input.is_action_pressed("reload_anim"):
		if obj == $Ballistas:
			obj.play_anim("reload", 0.5)
		else:
			obj.play_anim("shoot", 2)
	if Input.is_action_just_pressed("rotate_left"):
		dir += 45
		if dir > 360:
			dir -= 360
			if obj.get_meta("Tier") < 4:
				obj.update_tier(obj.get_meta("Tier")+1)
		obj.dir_rot(dir)
	if Input.is_action_just_pressed("rotate_right"):
		dir -= 45
		if dir < 0:
			dir += 360
			if obj.get_meta("Tier") > 1:
				obj.update_tier(obj.get_meta("Tier")-1)
		obj.dir_rot(dir)
