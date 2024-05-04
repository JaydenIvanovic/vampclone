using Godot;

public partial class Player : Area2D, IMover {
    private float speed = 30;
    private string boundaryHit = "";
    private AnimatedSprite2D sprite;

    public Vector2 Direction { get; private set; } = Vector2.Zero;

    public override void _Ready() {
        sprite = GetNode<AnimatedSprite2D>("./AnimatedSprite2D");
        sprite.Play();

        AreaEntered += (Area2D target) => {
            if (target.IsInGroup(Constants.Groups.ENEMIES)) {
                Globals.GameState.PlayerHealthAmount -= 10;
                if (Globals.GameState.PlayerHealthAmount <= 0) {
                    Events.I.EmitSignal(Events.SignalName.PlayerDied);
                }
            }

            if (target.IsInGroup(Constants.Groups.BOUNDARIES)) {
                boundaryHit = target.Name;
            }
        };

        AreaExited += (Area2D target) => {
            if (target.IsInGroup(Constants.Groups.BOUNDARIES)) {
                boundaryHit = "";
            }
        };
    }

    public override void _PhysicsProcess(double delta) {
        if (Input.IsActionPressed("move_left")) {
            if (boundaryHit == "Left") {
                return;
            }
            Position = Position + Vector2.Left * speed * (float)delta;
            Direction = Vector2.Left;
            sprite.FlipH = true;
        } else if (Input.IsActionPressed("move_right")) {
            if (boundaryHit == "Right") {
                return;
            }
            Position = Position + Vector2.Right * speed * (float)delta;
            Direction = Vector2.Right;
            sprite.FlipH = false;
        } else if (Input.IsActionPressed("move_down")) {
            if (boundaryHit == "Down") {
                return;
            }
            Position = Position + Vector2.Down * speed * (float)delta;
            Direction = Vector2.Down;
        } else if (Input.IsActionPressed("move_up")) {
            if (boundaryHit == "Up") {
                return;
            }
            Position = Position + Vector2.Up * speed * (float)delta;
            Direction = Vector2.Up;
        }
    }
}
