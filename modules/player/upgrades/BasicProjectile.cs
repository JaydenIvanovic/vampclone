using Godot;

public partial class BasicProjectile : Area2D, IMover {
    public Vector2 Direction { get; set; } = Vector2.Zero;

    private const float SPEED = 100;
    private const double MAX_LIFETIME_IN_SECONDS = 10;
    private double timeInWorld = 0;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready() {
        AreaEntered += HandleCollision;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta) {
        if (timeInWorld > MAX_LIFETIME_IN_SECONDS) {
            QueueFree();
        }
        timeInWorld += delta;

        Position = Position + Direction * SPEED * (float)delta;
    }

    private void HandleCollision(Area2D area) {
        if (!area.GetGroups().Contains(Constants.Groups.ENEMIES)) {
            return;
        }
        (area as IEnemy).TakeDamage(30);
        QueueFree();
    }

}
