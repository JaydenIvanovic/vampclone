using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Godot;

public partial class OrbEmitter : Node {
	private PackedScene orbScene;
	private List<Orb> orbInstances;
	private LinearSequenceTimer linearSequenceTimer;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		orbScene = GD.Load<PackedScene>("res://modules/player/upgrades/orb.tscn");
		linearSequenceTimer = new LinearSequenceTimer(new LinearSequenceTimerRunConfig {
			CreateInterval = 5,
			DestroyInterval = 5,
			CreateFunc = () => {
				orbInstances = new List<Orb>();
				for (var i = 0; i < Globals.GameState.NumOrbs; i++) {
					var orb = orbScene.Instantiate<Orb>();
					orb.Scale = new Vector2(0.65f, 0.65f);
					orb.InitialPosition = i * Mathf.Pi;
					orbInstances.Add(orb);
					GetParent().AddChild(orb);
				}

			},
			DestroyFunc = () => {
				orbInstances.ForEach(orb => {
					GetParent().RemoveChild(orb);
					orb.QueueFree();
				});
			},
			RootNode = this
		});
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
		linearSequenceTimer.Run();
	}
}
