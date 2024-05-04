using Godot;

public partial class Boundaries : Node {
	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		var nodes = GetChildren();
		foreach (var n in nodes) {
			n.AddToGroup(Constants.Groups.BOUNDARIES);
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
	}
}
