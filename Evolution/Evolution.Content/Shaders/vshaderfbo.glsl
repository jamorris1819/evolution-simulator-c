#version 430 core
layout (location = 0) in vec3 vPosition;
layout (location = 1) in vec3 vColour;
layout (location = 4) in vec2 vNormal;

out vec2 TexCoords;

void main()
{
    gl_Position = vec4(vPosition.x, vPosition.y, 0.0, 1.0); 
    TexCoords = (vPosition.xy + vec2(1, 1)) * 0.5;
}  