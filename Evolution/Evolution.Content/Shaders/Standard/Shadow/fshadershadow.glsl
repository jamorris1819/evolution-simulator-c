#version 430 core

in vec3 oColour;
in float oHeight;

out vec4 color;

uniform float alpha;
 
void main(void)
{
	color = vec4(oColour, 1);
}