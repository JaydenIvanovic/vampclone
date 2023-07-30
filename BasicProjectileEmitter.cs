using Godot;
using Godot.Collections;

public partial class BasicProjectileEmitter : Node2D {
    private IMover mover;
    private PackedScene projectileScene;
    private Array<BasicProjectile> projectiles;
    private Timer spawnTimer;

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

        var projectile = projectileScene.Instantiate<BasicProjectile>();
        projectile.Position = mover.Position;
        projectile.Direction = mover.Direction;

        projectiles.Add(projectile);

        GetTree().Root.GetNode("Main").AddChild(projectile);
    }
}
