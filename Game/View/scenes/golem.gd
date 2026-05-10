extends Node3D

@onready var selected = $Golem

func _ready():
	#update_tier()
	pass

func play_anim(v, sound = true, s = 1.0):
	if(v == "spawn"):
		if(sound):
			$AudioStreamPlayer.stream = load("res://sounds/golems/spawn.mp3")
			$AudioStreamPlayer.play()
		selected.get_node("AnimationPlayer").play_section("Scene",0,2.51, -1, s)
	elif(v == "idle"):
		if(sound):
			$AudioStreamPlayer.stream = load("res://sounds/golems/idle.mp3")
			$AudioStreamPlayer.play()
		selected.get_node("AnimationPlayer").play_section("Scene",3.283,4.9892, -1, s)
	elif(v == "move"):
		if(sound):
			$AudioStreamPlayer.stream = load("res://sounds/golems/move.mp3")
			$AudioStreamPlayer.play()
		selected.get_node("AnimationPlayer").play_section("Scene",5.7862,7.47, -1, s)
	elif(v == "attack"):
		if(sound):
			$AudioStreamPlayer.stream = load("res://sounds/golems/attack.mp3")
			$AudioStreamPlayer.play()
		selected.get_node("AnimationPlayer").play_section("Scene",8.2853,9.9656, -1, s)
	elif(v == "death"):
		if(sound):
			$AudioStreamPlayer.stream = load("res://sounds/golems/death.mp3")
			$AudioStreamPlayer.play()
		selected.get_node("AnimationPlayer").play_section("Scene",10.8034,13.2917, -1, s)
	elif(v == "reset"):
		selected.get_node("AnimationPlayer").play("Scene", -1, 0.0001)
		selected.get_node("AnimationPlayer").stop()

#func move(distance, speed):
#	self.position += Vector3(0, 0, speed).rotated(self.rotation.normalized(),self.rotation.z)

func dir_rot(a):
	var def = self.rotation_degrees
	def.y = a
	self.set_rotation_degrees(def)
