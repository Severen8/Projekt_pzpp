using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedievalTDIncremental.Game.Enemies {
	public class EnemyEscapeArgs : EventArgs {
		public required int Damage { get; set; }
	}
}
