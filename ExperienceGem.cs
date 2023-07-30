using Godot;

public partial class ExperienceGem : Area2D {

    public Node2D Target { get; set; }

    private const float PULL_RANGE = 50;
    private const float ACQUIRED_RANGE = 5;
    private const float SPEED = 200;


    // Called when the node enters the scene tree for the first time.
    public override void _Ready() {
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta) {
        var isMoving = Position.DistanceTo(Target.Position) < PULL_RANGE;
        if (isMoving) {
            Position += Position.DirectionTo(Target.Position) * SPEED * (float)delta;
        }
        if (Position.DistanceTo(Target.Position) < ACQUIRED_RANGE) {
            Events.getInstance().EmitSignal(Events.SignalName.ExperienceGemAcquired);
            QueueFree();
        }
    }

}
