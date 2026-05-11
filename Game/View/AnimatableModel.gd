class_name AnimatableModel
extends Node3D

@export var animations: Dictionary[String, AudibleAnimation];
var animationPlayer: AnimationPlayer;
var soundPlayer: AudioStreamPlayer;
var model: Node3D;

func _ready():
	model = get_node("Model");
	animationPlayer = model.get_node("AnimationPlayer");
	soundPlayer = get_node("AudioStreamPlayer");
	for value: AudibleAnimation in animations.values():
		if(value.AudioPath != ""):
			value.Stream = load(value.AudioPath);
	pass

func play_anim(
	animationKey: String, 
	sound: bool = true, 
	speed: float = 1.0
):
	var animation: AudibleAnimation = animations[animationKey];
	animationPlayer.play_section(
		"Scene", 
		animation.StartTime, 
		animation.EndTime,
		-1,
		speed);
	if(sound && animation.AudioPath != ""):
		soundPlayer.stream = animation.Stream;
		soundPlayer.play();
	pass
	
func reset():
	animationPlayer.play_section(
		"Scene",
		-1,
		0.0001);
	animationPlayer.stop();
