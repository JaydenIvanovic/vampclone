using Godot;


public partial class WytchFyre : Node2D {
	struct ShaderGlow {
		public const double TimeUpperBound = 1f;
		public const double TimeLowerBound = 0.5f;
		public const double TimeSpeedFactor = 0.3f;
		public double Time { get; set; }
		public bool Ascending { get; set; }

		public ShaderGlow() {
			Time = 0.5f;
			Ascending = true;
		}
	}


	private AnimatedSprite2D sprite;
	private ShaderGlow shaderGlow;

	public override void _Ready() {
		sprite = GetNode<AnimatedSprite2D>("./AnimatedSprite2D");
		sprite.Play();

		shaderGlow = new ShaderGlow();
	}

	public override void _Process(double delta) {
		ProcessGlowShader(delta);
	}

	private void ProcessGlowShader(double delta) {
		// glow_factor is a number between 1.0 and 2.0: e.g. 1.0 <= glow_factor <= 2.0
		// this effectively gives us a factor that increases the COLOR shader variable between 1 - 100%.
		// e.g. when glow_factor = 1.1, we have a 10% "bump" to each color value in the vec4 array.
		(sprite.Material as ShaderMaterial).SetShaderParameter("glow_factor", 2.0 * shaderGlow.Time);

		if (shaderGlow.Ascending) {
			shaderGlow.Time += ShaderGlow.TimeSpeedFactor * delta;
		} else {
			shaderGlow.Time -= ShaderGlow.TimeSpeedFactor * delta;
		}

		if (shaderGlow.Time > ShaderGlow.TimeUpperBound) {
			shaderGlow.Time = ShaderGlow.TimeUpperBound;
			shaderGlow.Ascending = false;
		}
		if (shaderGlow.Time < ShaderGlow.TimeLowerBound) {
			shaderGlow.Time = ShaderGlow.TimeLowerBound;
			shaderGlow.Ascending = true;
		}
	}
}
