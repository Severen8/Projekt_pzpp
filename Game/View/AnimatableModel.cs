using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedievalTDIncremental.Game.View {
	[GlobalClass]
	public partial class AnimatableModel: Node3D {
		[Export]
		public Godot.Collections.Dictionary<string, AudibleAnimation> Animations { get; set; }
		//deprecated
		[Export]
		public IgnoredMeshList IgnoredMeshes { get; set; }

		Node3D CurrentModel { get; set; }
		AnimationPlayer AnimationPlayer { get; set; }
		AudioStreamPlayer AudioPlayer { get; set; }

		


		public override void _Ready() {
			base._Ready();
			this.CurrentModel = GetChild<Node3D>(0);
			this.AnimationPlayer = CurrentModel.GetNode<AnimationPlayer>("AnimationPlayer");

			this.AudioPlayer = new AudioStreamPlayer();

			foreach(AudibleAnimation animation in Animations.Values) {
				animation.LoadAudio();
			}

			IgnoredMeshes.ScrapeMeshes(CurrentModel);
		}


		public override void _Process(double delta) {
			base._Process(delta);
			if (Input.IsActionJustPressed("spawn_anim")) {
				this.RotateTowards(Vector2.Zero, Vector2.Left);
			}
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


		public void Reset() {
			AnimationPlayer.PlaySection("Scene", -1, 0.0001);
			AnimationPlayer.Stop();
			this.Rotation = Vector3.Zero;
		}


		public void RotateTowards(Vector2 simPos, Vector2 target) {
			Vector3 newRotation = Rotation;
			newRotation.Y = simPos.AngleToPoint(target);
			Rotation = newRotation;

			RevertStaticMeshRotation();
		}

		void RevertStaticMeshRotation() {
			foreach (MeshInstance3D mesh in IgnoredMeshes.Meshes) {
				Vector3 meshNewRotation = mesh.Rotation;
				meshNewRotation.Y = -Rotation.Y;
				mesh.Rotation = meshNewRotation;
			}
		}
	}
}
