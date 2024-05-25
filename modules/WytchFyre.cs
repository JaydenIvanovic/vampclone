using Godot;
using System;

public partial class WytchFyre : Node2D {
	private Sprite2D sprite;
	private double time = 0.5f;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		sprite = GetNode<Sprite2D>("./Sprite2D");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
		(sprite.Material as ShaderMaterial).SetShaderParameter("delta_time", delta);
		(sprite.Material as ShaderMaterial).SetShaderParameter("relative_time", time);

		time += delta;
		if(time > 1) {
			time = 0.5;
		}
	}
}
