using Godot;

public partial class GameOverScreen : VFlowContainer {
	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		Events.I.PlayerDied += () => {
			Show();
			GetTree().Paused = true;
		};

		var button = GetNode<Button>("./Button");
		button.ButtonDown += () => {
			GetTree().Paused = false;
			GetTree().ReloadCurrentScene();
		};

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
	}
}
