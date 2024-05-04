using Godot;

public partial class DebugPanel : VBoxContainer {
	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		if (Constants.UI.CAN_SHOW_DEBUG_PANEL) {
			Show();
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
		var healthLabel = GetNode<Label>("%Health");
		healthLabel.Text = $"Health: {Globals.GameState.PlayerHealthAmount}";

		var killsLabel = GetNode<Label>("%Kills");
		killsLabel.Text = $"Kills: {Globals.GameState.Kills}";

		var experienceLabel = GetNode<Label>("%Experience");
		experienceLabel.Text = $"Experience: {Globals.GameState.ExperienceGained}";

		var fpsLabel = GetNode<Label>("%FPS");
		fpsLabel.Text = $"FPS: {Engine.GetFramesPerSecond()}";
	}
}
