using Godot;
using System;

[Tool]
public partial class MeshChecker : MeshInstance3D
{
	public MeshChecker() {	}
	public override void _Ready() {
		base._Ready();
	}

	[ExportToolButton("Get Size")]
	public Callable GetSize => Callable.From(() => {
		GD.Print(GetAabb().Size * 100);
	});
}
