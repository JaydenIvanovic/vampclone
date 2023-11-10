using System.Collections.Generic;
using System.Linq;
using Constants;
using Godot;

public partial class WeaponGarlic : Area2D {
	private CollisionShape2D collisionShape2D;
	private List<Area2D> enemiesInRadius;
	private Timer damageTicker;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		collisionShape2D = GetNode<CollisionShape2D>("./CollisionShape2D");
		(collisionShape2D.Shape as CircleShape2D).Radius = 0f;

		enemiesInRadius = new List<Area2D>();

		damageTicker = new Timer {
			OneShot = false,
			Autostart = true,
			WaitTime = 0.5
		};
		damageTicker.Timeout += () => {
			foreach (var enemy in enemiesInRadius) {
				(enemy as IEnemy).TakeDamage(15.0f);
			}
		};
		AddChild(damageTicker);

		AreaEntered += (Area2D target) => {
			if (target.IsInGroup(Groups.ENEMIES)) {
				enemiesInRadius.Add(target);
			}
		};
		AreaExited += (Area2D target) => {
			if (target.IsInGroup(Groups.ENEMIES)) {
				enemiesInRadius = enemiesInRadius.Where(e => e.GetInstanceId() != target.GetInstanceId()).ToList();
			}
		};

		Events.I.PlayerSelectedLevelUp += () => {
			var garlicLevels = Globals.GameState.NumGarlicUpgrades;
			// TODO: Make this logarithmic?
			(collisionShape2D.Shape as CircleShape2D).Radius = 15 * garlicLevels;
			QueueRedraw();
		};
	}

	public override void _Draw() {
		DrawCircle(Vector2.Zero, (collisionShape2D.Shape as CircleShape2D).Radius, Colors.LightGreen);
	}
}
