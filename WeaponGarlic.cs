using Constants;
using Godot;

public partial class WeaponGarlic : Area2D {
	private CollisionShape2D collisionShape2D;
	private float radius = 0f;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		collisionShape2D = GetNode<CollisionShape2D>("./CollisionShape2D");
		(collisionShape2D.Shape as CircleShape2D).Radius = radius;

		// TODO: Need to check when enemies exit the area as well.
		// That is, the logic needs to be something like...
		// On enter, set a variable that this enemy should take tick
		// damage from Garlic periodically.
		// On exit, remove this variable.
		AreaEntered += HandleCollision;

		Events.getInstance().ExperienceGemAcquired += HandleExperienceAcquired;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
	}

	public override void _Draw() {
		DrawCircle(Vector2.Zero, radius, Colors.LightGreen);
	}

	private void HandleCollision(Area2D target) {
		if (target.IsInGroup(Groups.ENEMIES)) {
			(target as IEnemy).TakeDamage(10.0f);
		}
	}

	private void HandleExperienceAcquired() {
		var garlicLevels = Globals.GameState.experienceGained / 100;
		// TODO: Make this logarithmic?
		radius = 13 * garlicLevels;
		QueueRedraw();
	}
}
