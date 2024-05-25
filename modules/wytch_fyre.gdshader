shader_type canvas_item;

uniform float delta_time;
uniform float relative_time;

void vertex() {
}

float scale_factor() {
	return 2.0 * relative_time;
}

void fragment() {
	vec4 color_as_rgb = COLOR * 255.0;
	if (color_as_rgb.r >= 200.0 && color_as_rgb.r < 250.0) {
		COLOR = COLOR * scale_factor();
	}
}
