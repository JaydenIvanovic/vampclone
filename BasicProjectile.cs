using Godot;
using System;

public partial class BasicProjectile : Area2D, IMover {
    public Vector2 Direction { get; set; } = Vector2.Zero;
    private float speed = 200;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready() {
        AreaEntered += HandleCollision;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta) {
        Position = Position + Direction * speed * (float)delta;

        // TODO: Check how long the projectile has been in the game world
        // and free it if it's been too long (no point keeping it around)
    }

    private void HandleCollision(Area2D area) {
        if (!area.GetGroups().Contains(Constants.Groups.ENEMIES)) {
            return;
        }
        area.QueueFree();
        QueueFree();
    }

}
