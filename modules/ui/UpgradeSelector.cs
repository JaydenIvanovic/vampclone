using Godot;
using System;

public partial class UpgradeSelector : VFlowContainer {
	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		var button1 = GetNode<Button>("./UpgradeOption/Button");
		var button2 = GetNode<Button>("./UpgradeOption2/Button");

		button1.ButtonDown += () => {
			Globals.GameState.NumProjectileCountUpgrades += 1;
			FinalizeUpgradeSelection();
		};

		button2.ButtonDown += () => {
			Globals.GameState.NumGarlicUpgrades += 1;
			FinalizeUpgradeSelection();
		};

		Events.I.PlayerSelectingLevelUp += () => {
			Show();
		};
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
	}

	private void FinalizeUpgradeSelection() {
		Hide();
		Events.I.EmitSignal(Events.SignalName.PlayerSelectedLevelUp);
	}
}
