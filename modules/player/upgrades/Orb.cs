using System;
using Constants;
using Godot;

public partial class Orb : Area2D {
	private double cumulativeDelta;
	private const uint OFFSET = 20;
	private const uint SPEED = 3;
	private const uint DAMAGE = 100;
	public double InitialPosition { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		cumulativeDelta = InitialPosition;

		AreaEntered += (Area2D area) => {
			if (area.IsInGroup(Groups.ENEMIES)) {
				var enemy = area as IEnemy;
				enemy.TakeDamage(DAMAGE);
			}
		};
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
