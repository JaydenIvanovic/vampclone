using Godot;

public partial class UI : Node {
	private Control upgradeSelector;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		upgradeSelector = GetNode<Control>("./UpgradeSelector");
		BindUpgradeSelectors();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
	}

	private void BindUpgradeSelectors() {
		var button1 = upgradeSelector.GetNode<Button>("./UpgradeOption/Button");
		var button2 = upgradeSelector.GetNode<Button>("./UpgradeOption2/Button");

		button1.ButtonDown += () => {
			GD.Print("Selected 1 projectile");
		};

		button2.ButtonDown += () => {
			GD.Print("Selected garlic");
		};
	}
}
