using Godot;
using Godot.Collections;

public partial class BasicProjectileEmitter : Node2D {
    private IMover mover;
    private PackedScene projectileScene;
    private Array<BasicProjectile> projectiles;
    private Timer spawnTimer;
    private uint numberOfProjectiles = 1;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready() {
        mover = GetNode<IMover>("../MovementController");
        projectileScene = GD.Load<PackedScene>("res://modules/player/upgrades/basic_projectile.tscn");
        projectiles = new Array<BasicProjectile>();

        spawnTimer = new Timer {
            OneShot = false,
            Autostart = true,
            WaitTime = 1.5
        };
        spawnTimer.Timeout += SpawnProjectile;
        AddChild(spawnTimer);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta) {
        numberOfProjectiles = Globals.GameState.NumProjectileCountUpgrades;
    }

    private void SpawnProjectile() {
        // Ensures we don't spawn a projectile that
        // will remain stationary.
        if (mover.Direction == Vector2.Zero) {
            return;
        }

        if (Constants.Logger.CAN_LOG_SPAWN_PROJECTILE) {
            GD.Print("Spawning projectile...");
        }

        for (var i = 0; i < numberOfProjectiles; i++) {
            var projectile = projectileScene.Instantiate<BasicProjectile>();
            projectile.Position = mover.Position;
            projectile.Direction = mover.Direction;

            // Randomize direction for every projectile apart from the first
            if (i > 0) {
                projectile.Direction = GetRandomDirection();
            }

            projectiles.Add(projectile);

            GetTree().Root.GetNode("Main").AddChild(projectile);
        }
    }

    private Vector2 GetRandomDirection() {
        Vector2[] directions = {
            Vector2.Left,
            Vector2.Right,
            Vector2.Up,
            Vector2.Down,
            new Vector2(-1,1), // upper left
            new Vector2(1,1), // upper right
            new Vector2(-1,-1), // lower left
            new Vector2(1,-1), // lower right
        };
        return directions[GD.Randi() % directions.Length];
    }
}
