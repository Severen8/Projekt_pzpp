extends Node3D

#spawn: 0-87 (0-1.5)
#shoot: 110-139 (1.8-2.3)
#reload: 160-192 (2.65-3.23)

func play_anim(v, s = 1.0):
	if(v == "spawn"):
		$AnimationPlayer.play_section("Scene",0,1.5, -1, s)
	elif(v == "shoot"):
		$AnimationPlayer.play_section("Scene",1.8,2.3, -1, s)
	elif(v == "reload"):
		$AnimationPlayer.play_section("Scene",2.65,3.23, -1, s)
	elif(v == "reset"):
		$AnimationPlayer.play("Scene", -1, 0.0001)
		$AnimationPlayer.stop()

var not_rotate_parts = [$PlatformBase,$TowerBase,$TowerFoot]

func dir_rot(a):
	var def = self.rotation_degrees
	def.y = a
	self.set_rotation_degrees(def)
	for i in not_rotate_parts:
		def = i.rotation_degrees
		def.y = -a
		i.set_rotation_degrees(def)
	
