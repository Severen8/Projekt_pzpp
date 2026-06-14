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
			UpdateModelCache(GetNode<Node3D>("Model"));
			this.AudioPlayer = new AudioStreamPlayer(); //todo: extract this to a dedicated class
			this.AddChild(AudioPlayer, false, InternalMode.Front);
			ScrapeAudio();
		}


		public void PlayAnimation(
			string animationKey,
			bool soundOn = true, 
			float speed = 1
		) {
			AudibleAnimation animation;
			if(!Animations.TryGetValue(animationKey, out animation)){
				GD.PushError($"Attempted to play non-existent animation {animationKey} in {GetPath().GetConcatenatedNames()}");
				return;
			}

			if(soundOn && animation.AudioStream != null) {
				AudioPlayer.Stop();
				AudioPlayer.Stream = animation.AudioStream;
				AudioPlayer.Play();
			}
			AnimationPlayer.PlaySection("Scene", animation.StartTime, animation.EndTime, -1, speed); //todo: looping
		}



		protected void ScrapeAudio() {
			string directoryPath = this.GetParent().SceneFilePath.GetBaseDir();
			foreach (string animationName in Animations.Keys) {
				var animation = Animations[animationName];
				string audioPath = directoryPath + "/Audio/" + animationName + ".mp3";
				animation.LoadAudio(audioPath);
			}
		}


		
		public void SetRotation(float radians) {
			this.Rotation = new Vector3(0, radians, 0);
		}


		protected void UpdateModelCache(Node3D newModel) {
			if(CurrentModel != null) {
				this.RemoveChild(CurrentModel);
				CurrentModel.QueueFree();
			}
			this.CurrentModel = newModel;
			this.AnimationPlayer = CurrentModel.GetNode<AnimationPlayer>("AnimationPlayer");
		}
	}
}
