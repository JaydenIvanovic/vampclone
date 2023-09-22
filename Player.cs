using Godot;

public partial class Player : Area2D
{
    private AnimatedSprite2D sprite;
    private BasicProjectileEmitter projectileEmitter;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        sprite = GetNode<AnimatedSprite2D>("./AnimatedSprite2D");
        sprite.Play();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}
