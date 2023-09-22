using Godot;
using Godot.Collections;

public partial class BasicProjectileEmitter : Node2D {
    private IMover mover;
    private PackedScene projectileScene;
    private Array<BasicProjectile> projectiles;
    private Timer spawnTimer;
    private uint numberOfProjectiles = 1;
    // TODO: Do we need a global state object?
    private uint experienceGained;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready() {
        mover = GetNode<IMover>("../MovementController");
        projectileScene = GD.Load<PackedScene>("res://basic_projectile.tscn");
        projectiles = new Array<BasicProjectile>();

        spawnTimer = new Timer();
        spawnTimer.OneShot = false;
        spawnTimer.Autostart = true;
        spawnTimer.WaitTime = 1.5;
        spawnTimer.Timeout += this.SpawnProjectile;
        this.AddChild(spawnTimer);

        Events.getInstance().ExperienceGemAcquired += HandleExperienceAcquired;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta) {
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
        return directions[(GD.Randi() % directions.Length)];
    }

    private void HandleExperienceAcquired() {
        experienceGained += 10;

        numberOfProjectiles = experienceGained / 50;
        if (numberOfProjectiles < 1) {
            numberOfProjectiles = 1;
        }
    }
}
