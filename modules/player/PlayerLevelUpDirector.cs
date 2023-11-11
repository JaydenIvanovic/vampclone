using Godot;
using System;

public partial class PlayerLevelUpDirector : Node {
	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		Events.I.ExperienceGemAcquired += () => {
			var canLevelUp = Globals.GameState.ExperienceGained % 100 == 0;
			if (canLevelUp) {
				GetTree().Paused = true;
				Events.I.EmitSignal(Events.SignalName.PlayerSelectingLevelUp);
			}
		};

		Events.I.PlayerSelectedLevelUp += () => {
			GetTree().Paused = false;
		};
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
	}
}
