using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Godot;
using Godot.Collections;

namespace MedievalTDIncremental.Game.View {
	[GlobalClass]
	public partial class AudibleAnimation: Resource {
		[Export]
		public double StartTime { get; set; }

		[Export]
		public double EndTime { get; set; }

		public AudioStream AudioStream { get; private set; }

		public AudibleAnimation() {

		}

		public void LoadAudio(string path) {
			if (FileAccess.FileExists(path)) {
				AudioStream = ResourceLoader.Load<AudioStream>(path);
			} else {
				GD.PushWarning($"Audio at {path} not found");
			}
		}
	}
}
