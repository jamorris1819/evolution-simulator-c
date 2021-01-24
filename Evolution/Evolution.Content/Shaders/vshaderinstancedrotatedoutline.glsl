#version 430 core

uniform mat4 uModel;
uniform mat4 uView;
uniform mat4 uProjection;

layout(location = 0) in vec2 vPosition;
layout(location = 1) in vec3 vColour;

layout(location = 2) in vec2 vIPosition;
layout(location = 3) in vec3 vIColour;

out vec3 oColour;

mat2 rotationZ(in float angle)
{
	return mat2(cos(angle), -sin(angle),
			 		sin(angle), cos(angle));
}

void main(void)
{
	vec2 newPos = vPosition * rotationZ((vIPosition.x + vIPosition.y) * 10);

	float sizeChange = 0.001;

	// make bigger
	/*if(newPos.x < 0) newPos.x -= sizeChange;
	if(newPos.x > 0) newPos.x += sizeChange;
	if(newPos.y < 0) newPos.y -= sizeChange;
	if(newPos.y > 0) newPos.y += sizeChange;*/

	gl_Position = uProjection * uView * uModel * vec4((newPos * 1.1) + vIPosition, 0, 1);

	oColour = vIColour;
}

