using Godot;

public partial class EnemySpawner : Node {
    private Timer spawnTimer;
    private PackedScene enemyScene;

    [Export]
    private Node2D target;

    public override void _Ready() {
        enemyScene = GD.Load<PackedScene>("res://modules/enemies/basic_enemy.tscn");

        spawnTimer = new Timer {
            OneShot = false,
            Autostart = true,
            WaitTime = 1
        };
        spawnTimer.Timeout += SpawnEnemy;
        AddChild(spawnTimer);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta) {
    }

    private void SpawnEnemy() {
        var enemyInstance = enemyScene.Instantiate<IEnemy>();
        enemyInstance.Target = target;
        enemyInstance.Position = GetRandomNearbyPosition(target.Position);
        if (Constants.Logger.CAN_LOG_SPAWN_ENEMY) {

            GD.Print("Spawning enemy at: ", enemyInstance.Position);
        }
        GetParent().AddChild(enemyInstance as Node);
    }

    private Vector2 GetRandomNearbyPosition(Vector2 target) {
        // Workout the size of the viewport and spawn the enemies
        // near the edge of the screen...
        // need to halve the viewport as player is centered and off screen is a half
        var viewportSize = GetViewport().GetVisibleRect().Size;
        return (GD.Randi() % 4) switch {
            0 => target - new Vector2(viewportSize.X, 0),
            1 => target + new Vector2(viewportSize.X, 0),
            2 => target - new Vector2(0, viewportSize.Y),
            3 => target + new Vector2(0, viewportSize.Y),
            _ => target - new Vector2(viewportSize.X, 0)
        };
    }
}
