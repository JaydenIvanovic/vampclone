using System;
using Godot;

public partial class Orb : Area2D {
	private double cumulativeDelta;
	private const uint OFFSET = 20;
	private const uint SPEED = 3;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		cumulativeDelta = 0;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
		cumulativeDelta += delta;
		if (cumulativeDelta > 2 * Math.PI) {
			cumulativeDelta = delta;
		}

		Position = new Vector2(
			(float)Math.Cos(cumulativeDelta * SPEED) * OFFSET,
			(float)Math.Sin(cumulativeDelta * SPEED) * OFFSET
		);
	}
}
