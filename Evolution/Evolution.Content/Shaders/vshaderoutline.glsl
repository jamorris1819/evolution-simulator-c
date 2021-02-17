#version 430 core

uniform mat4 uModel;
uniform mat4 uView;
uniform mat4 uProjection;

layout (location = 0) in vec2 vPosition;
layout (location = 1) in vec3 vColour;
in vec2 vNormal;

out vec2 oNormal;
out vec2 oPosition;
out vec3 oColour;
 
void main(void)
{	
	mat4 modelMatrix = uView * uModel;
	mat3 normalMatrix = mat3(modelMatrix[0].xyz, modelMatrix[1].xyz, modelMatrix[2].xyz);

	vec2 newNormal = (normalMatrix * vec3(vNormal, 0)).xy;

	vec2 border = vNormal * vec2(0.025, 0.025);
	gl_Position = uProjection * uView * uModel * vec4(vPosition * 1.1, 0, 1);
	
	oNormal = vNormal;
	oPosition = vPosition;
	oColour = vec3(0, 0, 0);
}