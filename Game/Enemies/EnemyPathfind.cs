using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedievalTDIncremental.Game.Enemies {
	public class EnemyPathfind {
		static List<Vector2> Path => Round.Singleton.Path;

		public event EventHandler ReachedEnd;

		public delegate void OnTurned(Vector2 newDirection);
		public event OnTurned Turned;


		int PathIndex { get; set; }
		Vector2 CurrentPosition { get; set; }
		Vector2 TargetVertex => Path[PathIndex + 1];
		//todo: potentially create a dedicated path class and move this there
		bool PathHasVerticesRemaining => Path.Count > PathIndex + 2;

		public EnemyPathfind() {
			this.PathIndex = 0;
			this.CurrentPosition = Path[0];
		}

		public Vector2 GetNextPos(double distanceMoved) {
			Vector2 distanceToNextPoint = TargetVertex - CurrentPosition;
			double leftoverDistance = distanceMoved - distanceToNextPoint.Length();

			CurrentPosition = CurrentPosition.MoveToward(TargetVertex, (float)distanceMoved);
			if(leftoverDistance > 0) {
				if (PathHasVerticesRemaining) {
					FindNextVertex();
					return GetNextPos(leftoverDistance);
				}
				ReachedEnd.Invoke(this, new());
			}
			return CurrentPosition;
		}


		void FindNextVertex() {
			this.PathIndex++;
			Turned.Invoke(CurrentPosition.DirectionTo(TargetVertex));
		}
	}
}
