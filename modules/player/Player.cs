using Godot;

public partial class Player : Area2D, IMover {
    private float speed = 30;
    private AnimatedSprite2D sprite;
    private RayCast2D rayCast;

    public Vector2 Direction { get; private set; } = Vector2.Zero;

    public override void _Ready() {
        rayCast = GetNode<RayCast2D>("./RayCast2D");

        sprite = GetNode<AnimatedSprite2D>("./AnimatedSprite2D");
        sprite.Play();

        AreaEntered += (Area2D target) => {
            if (target.IsInGroup(Constants.Groups.ENEMIES)) {
                Globals.GameState.PlayerHealthAmount -= 10;
                if (Globals.GameState.PlayerHealthAmount <= 0) {
                    Events.I.EmitSignal(Events.SignalName.PlayerDied);
                }
            }
        };
    }

    public override void _PhysicsProcess(double delta) {
        if (Input.IsActionPressed("move_left")) {
            Move(Vector2.Left, delta);
            sprite.FlipH = true;
        } else if (Input.IsActionPressed("move_right")) {
            Move(Vector2.Right, delta);
            sprite.FlipH = false;
        } else if (Input.IsActionPressed("move_down")) {
            Move(Vector2.Down, delta);
        } else if (Input.IsActionPressed("move_up")) {
            Move(Vector2.Up, delta);
        }
    }

    private void Move(Vector2 direction, double delta) {
        rayCast.TargetPosition = direction * 10;
        rayCast.ForceRaycastUpdate();
        if (rayCast.IsColliding()) {
            return;
        }
        Position = Position + direction * speed * (float)delta;
        Direction = direction;
    }
}
