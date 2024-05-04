using Godot;

public partial class BasicProjectile : Area2D {
    public Vector2 Direction { get; set; } = Vector2.Zero;

    private const float SPEED = 100;
    private const double MAX_LIFETIME_IN_SECONDS = 10;
    private double timeInWorld = 0;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready() {
        AreaEntered += (Area2D area) => {
            if (area.IsInGroup(Constants.Groups.ENEMIES)) {
                (area as IEnemy).TakeDamage(30);
                QueueFree();
            }
        };
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta) {
        if (timeInWorld > MAX_LIFETIME_IN_SECONDS) {
            QueueFree();
            return;
        }
        timeInWorld += delta;
        Position = Position + Direction * SPEED * (float)delta;
    }
}
