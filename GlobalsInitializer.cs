using Godot;
using System;

public partial class GlobalsInitializer : Node {
	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		Globals.GameState.NumGarlicUpgrades = 0;
		Globals.GameState.NumProjectileCountUpgrades = 1;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
	}
}
