using Godot;
using System;

public partial class GameInitializer : Node {
	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		Globals.GameState.ExperienceGained = 0;
		Globals.GameState.Kills = 0;
		Globals.GameState.NumGarlicUpgrades = 0;
		Globals.GameState.NumProjectileCountUpgrades = 1;
		Globals.GameState.PlayerHealthAmount = 100;

		Events.Init();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
	}
}
