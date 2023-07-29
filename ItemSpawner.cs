using Godot;
using System;

public partial class ItemSpawner : Node
{
    private PackedScene experieneGemScene;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        experieneGemScene = GD.Load<PackedScene>("res://experience_gem.tscn");

        Events.getInstance().EnemyDied += SpawnExperience;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    private void SpawnExperience(Vector2 deathPosition)
    {
        if (Logger.CAN_LOG_SPAWN_GEM)
        {
            GD.Print("Got enemy died event, will spawn gem at: ", deathPosition);
        }
        var experienceGem = experieneGemScene.Instantiate<ExperienceGem>();
        experienceGem.Position = deathPosition;
        GetTree().Root.AddChild(experienceGem);
    }
}
