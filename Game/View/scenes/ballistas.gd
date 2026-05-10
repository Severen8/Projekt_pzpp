extends Node3D

var selected

func _ready():
	update_tier()

#spawn: 0-87 (0-1.5)
#shoot: 110-139 (1.8-2.3)
#reload: 160-192 (2.65-3.23)

func play_anim(v, sound = true, s = 1.0):
	if(v == "spawn"):
		if(sound):
			$AudioStreamPlayer.stream = load("res://sounds/ballistas/spawn.mp3")
			$AudioStreamPlayer.play()
		selected.get_node("AnimationPlayer").play_section("Scene",0,1.5, -1, s)
	elif(v == "shoot"):
		if(sound):
			$AudioStreamPlayer.stream = load("res://sounds/ballistas/shoot.mp3")
			$AudioStreamPlayer.play()
		selected.get_node("AnimationPlayer").play_section("Scene",1.8,2.3, -1, s)
	elif(v == "reload"):
		if(sound):
			$AudioStreamPlayer.stream = load("res://sounds/ballistas/reload.mp3")
			$AudioStreamPlayer.play()
		selected.get_node("AnimationPlayer").play_section("Scene",2.65,3.23, -1, s)
	elif(v == "reset"):
		selected.get_node("AnimationPlayer").play("Scene", -1, 0.0001)
		selected.get_node("AnimationPlayer").stop()



func dir_rot(a):
	var num = str(self.get_meta("Tier"))
	var not_rotate_parts = [
		selected.get_node_or_null("T"+num+"PlatformBase"),
		selected.get_node_or_null("T"+num+"TowerBase"),
		selected.get_node_or_null("T"+num+"TowerFoot"),
		selected.get_node_or_null("T"+num+"Reinforcements")
		]
	var def = self.rotation_degrees
	def.y = a
	self.set_rotation_degrees(def)
	for i in not_rotate_parts:
		if i != null:
			def = i.rotation_degrees
			def.y = -a
			i.set_rotation_degrees(def)

func update_tier(t = -1, sound = true):
	if t != -1:
		self.set_meta("Tier",t)
		if(sound):
			$AudioStreamPlayer.stream = load("res://sounds/ballistas/upgrade.mp3")
			$AudioStreamPlayer.play()
	selected = get_node("T" + str(self.get_meta("Tier")))
	for i in get_children():
		if str(i).substr(0,1) == "T":
			if str(i).substr(1,1) != str(self.get_meta("Tier")):
				i.hide()
			else:
				i.show()
	play_anim("reset")
