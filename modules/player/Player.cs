using Godot;

public partial class Player : Area2D {
    private AnimatedSprite2D sprite;

    // Called when the node enters the scene tree for the first time.
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
        };
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta) {
    }
}
