#version 430 core

uniform mat4 uModel;
uniform mat4 uView;
uniform mat4 uProjection;

layout (location = 0) in vec2 vPosition;
layout (location = 1) in vec3 vColour;

layout (location = 2) in vec2 vIPosition;
layout (location = 3) in vec3 vIColour;

out vec3 oColour;
 
void main(void)
{	
	gl_Position = uProjection * uView * uModel * vec4(vPosition + vIPosition, 0, 1);
	
	oColour = vIColour;
}

