using System.Linq;
using Godot;

public partial class OrbEmitter : Node {
	private PackedScene orbScene;
	private Orb orbInstance;
	private LinearSequenceTimer linearSequenceTimer;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		orbScene = GD.Load<PackedScene>("res://modules/player/upgrades/orb.tscn");
		linearSequenceTimer = new LinearSequenceTimer(new LinearSequenceTimerRunConfig {
			CreateInterval = 5,
			DestroyInterval = 5,
			CreateFunc = () => {
				orbInstance = orbScene.Instantiate<Orb>();
				orbInstance.Scale = new Vector2(0.65f, 0.65f);
				GetParent().AddChild(orbInstance);
			},
			DestroyFunc = () => {
				GetParent().RemoveChild(orbInstance);
				orbInstance.QueueFree();
				GetChildren().ToList().ForEach(n => RemoveChild(n));
			},
			RootNode = this
		});
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
		linearSequenceTimer.Run();
	}
}
