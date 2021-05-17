#version 430 core

uniform mat4 uModel;
uniform mat4 uView;
uniform mat4 uProjection;
uniform float alpha;

layout (location = 0) in vec3 vPosition;
layout (location = 1) in vec3 vColour;
layout (location = 2) in vec2 vIPosition;
layout (location = 3) in vec3 vIColour;
in vec2 vNormal;

out vec3 oColour;

mat2 rotationZ(in float angle)
{
	return mat2(cos(angle), -sin(angle),
			 		sin(angle), cos(angle));
}
 
void main(void)
{	
	float modifier = 0.15f;
	vec2 lightDirection = normalize(vec2(1, 1));
	vec2 offset = lightDirection * vPosition.z * modifier * 0;

	float heightScale = vPosition.z * 0.2 + 1;
	vec2 newPos = vPosition.xy * rotationZ((vIPosition.x + vIPosition.y) * 10);

	gl_Position = uProjection * uView * uModel * vec4(vIPosition + newPos * heightScale, 0, 1);
	gl_Position += uProjection * uView * vec4(offset, 0, 0);

	oColour = vec3(0, 0, 0);
}