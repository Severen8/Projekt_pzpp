using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedievalTDIncremental.Game.View {
	[GlobalClass]
	public partial class AnimatableModel: Node3D {
		[Export]
		public Godot.Collections.Dictionary<string, AudibleAnimation> Animations { get; set; }

		protected Node3D CurrentModel { get; set; }
		protected AnimationPlayer AnimationPlayer { get; set; }
		protected AudioStreamPlayer AudioPlayer { get; set; }

		public override void _Ready() {
			base._Ready();
			this.CurrentModel = GetChild<Node3D>(0);
			this.AnimationPlayer = CurrentModel.GetNode<AnimationPlayer>("AnimationPlayer");

			this.AudioPlayer = new AudioStreamPlayer(); //todo: extract this to a dedicated class
			this.AddChild(AudioPlayer);
			ScrapeAudio();
		}


		//todo: remove temp test setup
		public override void _Process(double delta) {
			base._Process(delta);
			if (Input.IsActionJustPressed("spawn_anim"))
				this.PlayAnimation("spawn");
			if(Input.IsActionJustPressed("rotate_left"))
				this.RotateTowards(Vector2.Zero, Vector2.Left);
		}



		public void PlayAnimation(
			string animationKey, 
			bool soundOn = true, 
			float speed = 1
		) {
			AudibleAnimation animation;
			if(!Animations.TryGetValue(animationKey, out animation)){
				GD.PrintErr($"Attempted to play non-existent animation {animationKey} in {GetPath().GetConcatenatedNames()}");
				return;
			}

			if(soundOn && animation.AudioStream != null) {
				AudioPlayer.Stream = animation.AudioStream;
				AudioPlayer.Play();
			}
			AnimationPlayer.PlaySection("Scene", animation.StartTime, animation.EndTime, -1, speed);
		}



		public void ResetAnimation() {
			AnimationPlayer.PlaySection("Scene", -1, 0.0001);
			AnimationPlayer.Stop();
		}



		public virtual void RotateTowards(Vector2 simPos, Vector2 target) {
			Vector3 newRotation = Rotation;
			newRotation.Y = simPos.AngleToPoint(target);
			Rotation = newRotation;
		}


		protected void ScrapeAudio() {
			int sceneNameIndex = SceneFilePath.RFind("/");
			string directoryPath = SceneFilePath.Substring(0, sceneNameIndex);
			foreach (string animationName in Animations.Keys) {
				var animation = Animations[animationName];
				string audioPath = directoryPath + "/Audio/" + animationName + ".mp3";
				animation.LoadAudio(audioPath);
			}
		}
	}
}
