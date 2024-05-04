using Godot;
using System;

public partial class HealthPickup : Node2D {
	private Area2D area;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		area = GetNode<Area2D>("Area2D");
		area.AreaEntered += (Area2D target) => {
			if (target.Name == "Player") {
				Globals.GameState.PlayerHealthAmount += 10;
				QueueFree();
			}
		};
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
	}
}
