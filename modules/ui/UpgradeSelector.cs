using Godot;

public partial class UpgradeSelector : Container {
	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		var button1 = GetNode<Button>("./Upgrades/Upgrade1/SelectUpgradeButton1");
		var button2 = GetNode<Button>("./Upgrades/Upgrade2/SelectUpgradeButton2");
		var button3 = GetNode<Button>("./Upgrades/Upgrade3/SelectUpgradeButton3");

		button1.ButtonDown += () => {
			Globals.GameState.NumProjectileCountUpgrades += 1;
			FinalizeUpgradeSelection();
		};

		button2.ButtonDown += () => {
			Globals.GameState.NumGarlicUpgrades += 1;
			FinalizeUpgradeSelection();
		};

		button3.ButtonDown += () => {
			Globals.GameState.NumOrbs += 1;
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
