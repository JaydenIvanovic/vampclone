using Godot;

public partial class ItemSpawner : Node {
    [Export]
    public Node2D Target;

    private PackedScene experienceGemScene;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready() {
        experienceGemScene = GD.Load<PackedScene>("res://experience_gem.tscn");

        Events.getInstance().EnemyDied += SpawnExperience;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta) {
    }

    private void SpawnExperience(Vector2 deathPosition) {
        if (Constants.Logger.CAN_LOG_SPAWN_GEM) {
            GD.Print("Got enemy died event, will spawn gem at: ", deathPosition);
        }
        var experienceGem = experienceGemScene.Instantiate<ExperienceGem>();
        experienceGem.Position = deathPosition;
        experienceGem.Target = Target;
        GetTree().Root.GetNode("Main").AddChild(experienceGem);
    }
}
