using Godot;
using System;

public partial class MovementController : Node2D, IMover {
    public Vector2 Direction { get; private set; } = Vector2.Zero;

    private float speed = 100;
    private Node2D target;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready() {
        target = GetParent<Node2D>();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta) {
        if (Input.IsActionPressed("move_left")) {
            target.Position = target.Position + Vector2.Left * speed * (float)delta;
            Direction = Vector2.Left;
        } else if (Input.IsActionPressed("move_right")) {
            target.Position = target.Position + Vector2.Right * speed * (float)delta;
            Direction = Vector2.Right;
        } else if (Input.IsActionPressed("move_down")) {
            target.Position = target.Position + Vector2.Down * speed * (float)delta;
            Direction = Vector2.Down;
        } else if (Input.IsActionPressed("move_up")) {
            target.Position = target.Position + Vector2.Up * speed * (float)delta;
            Direction = Vector2.Up;
        }

        // Have to set the Position on this concrete IMover implementation
        // BasicProjectileEmitter has a reference to the IMover, and wants to use
        // it's position to determine where the projectiles will start from...
        Position = target.Position;
    }
}
