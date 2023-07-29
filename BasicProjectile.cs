using Godot;
using System;

public partial class BasicProjectile : Area2D, IMover
{
    public Vector2 Direction { get; set; } = Vector2.Zero;
    private float speed = 200;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        AreaEntered += HandleCollision;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        Position = Position + Direction * speed * (float)delta;
    }

    private void HandleCollision(Area2D area)
    {
        area.QueueFree();
        QueueFree();
    }

}
