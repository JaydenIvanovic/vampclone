using Godot;

public partial class Player : Area2D, IMover {
    private float speed = 30;
    private AnimatedSprite2D sprite;
    private RayCast2D boundaryMaskedRay;

    public Vector2 Direction { get; private set; } = Vector2.Zero;

    public override void _Ready() {
        boundaryMaskedRay = GetNode<RayCast2D>("./BoundaryMaskedRay");

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
        boundaryMaskedRay.TargetPosition = direction * 10;
        boundaryMaskedRay.ForceRaycastUpdate();
        if (boundaryMaskedRay.IsColliding()) {
            return;
        }
        Position = Position + direction * speed * (float)delta;
        Direction = direction;
    }
}
