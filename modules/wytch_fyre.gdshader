shader_type canvas_item;

uniform float glow_factor;

void vertex() {
}

void fragment() {
	vec4 color_as_rgb = COLOR * 255.0;
	if (color_as_rgb.r >= 200.0 && color_as_rgb.r < 250.0) {
		COLOR = COLOR * glow_factor;
	}
}
