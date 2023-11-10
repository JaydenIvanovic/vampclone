using Godot;
using System;

public partial class DebugPanel : VBoxContainer {
	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
		var killsLabel = GetNode<Label>("%Kills");
		killsLabel.Text = $"Kills: {Globals.GameState.Kills}";

		var experienceLabel = GetNode<Label>("%Experience");
		experienceLabel.Text = $"Experience: {Globals.GameState.ExperienceGained}";
	}
}
