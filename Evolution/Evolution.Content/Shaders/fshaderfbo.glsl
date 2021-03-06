#version 430 core

out vec4 FragColor;
  
in vec2 TexCoords;

uniform sampler2D screenTexture;

void main()
{ 
    vec4 texColour = texture(screenTexture, TexCoords);

    FragColor = texColour;
}