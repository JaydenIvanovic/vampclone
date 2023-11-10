using Godot;

public partial class BasicEnemy : Area2D, IEnemy {
    public Node2D Target { get; set; }
    private Vector2 moveDirection;
    private float health = 30;
    private float moveSpeed = 30;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready() {
        AddToGroup(Constants.Groups.ENEMIES);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta) {
        moveDirection = Position.DirectionTo(Target.Position);
        Position = Position + moveDirection * moveSpeed * (float)delta;
    }

    public override void _Notification(int what) {
        if (what == NotificationPredelete) {
            if (Constants.Logger.CAN_LOG_SPAWN_GEM) {
                GD.Print("Emitting signal that enemy died");
            }
            Globals.GameState.Kills += 1;
            Events.I.EmitSignal(Events.SignalName.EnemyDied, Position);
        }
        base._Notification(what);
    }

    public void TakeDamage(float damage) {
        health -= damage;
        if (health <= 0) {
            QueueFree();
        }
    }
}
