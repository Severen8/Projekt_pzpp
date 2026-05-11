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
		public string AudioPath { get; set; }

		[Export]
		public double StartTime { get; set; }

		[Export]
		public double EndTime { get; set; }

		public AudioStream Stream { get; set; }

		public AudibleAnimation() {

		}
	}
}
