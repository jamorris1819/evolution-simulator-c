#version 430 core

uniform mat4 uModel;
uniform mat4 uView;
uniform mat4 uProjection;

layout (location = 0) in vec2 vPosition;
layout (location = 1) in vec3 vColour;
layout (location = 4) in vec2 vNormal;

out vec3 oColour;
 
void main(void)
{	
	//vec4 P = uProjection * uView * uModel * vec4(vPosition, 0, 1);
	//vec4 N = transpose(inverse(uProjection * uView * uModel)) * vec4(vNormal, 0, 1);

	gl_Position = uProjection * uView * uModel * vec4(vPosition + vNormal * 0.01, 0, 1);
	oColour = vec3(0, 0, 0);
}