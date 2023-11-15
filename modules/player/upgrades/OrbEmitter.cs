using System.Linq;
using Godot;
using Godot.Collections;

public partial class OrbEmitter : Node {

	private PackedScene orbScene;
	private Orb orbInstance;
	private Timer spawnTimer;
	private Timer destroyTimer;
	private bool canEmitOrb;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		canEmitOrb = true;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
		if (canEmitOrb) {
			SpawnOrb();
		}
	}

	private void SpawnOrb() {
		canEmitOrb = false;

		orbScene = GD.Load<PackedScene>("res://modules/player/upgrades/orb.tscn");

		spawnTimer = new Timer {
			OneShot = true,
			Autostart = true,
			WaitTime = 5
		};
		spawnTimer.Timeout += () => {
			orbInstance = orbScene.Instantiate<Orb>();
			orbInstance.Scale = new Vector2(0.7f, 0.7f);
			GetParent().AddChild(orbInstance);

			destroyTimer = new Timer {
				OneShot = true,
				Autostart = true,
				WaitTime = 5
			};
			destroyTimer.Timeout += () => {
				GetParent().RemoveChild(orbInstance);
				orbInstance.QueueFree();
				GetChildren().ToList().ForEach(n => RemoveChild(n));
				canEmitOrb = true;
			};
			AddChild(destroyTimer);
		};
		AddChild(spawnTimer);
	}
}
