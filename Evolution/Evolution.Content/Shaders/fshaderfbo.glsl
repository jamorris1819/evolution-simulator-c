#version 430 core

out vec4 FragColor;
  
in vec2 TexCoords;

uniform sampler2D screenTexture;
uniform float alpha;

void main()
{ 
    vec4 texColour = texture(screenTexture, TexCoords);

    FragColor = vec4(texColour.xyz, texColour.a * alpha);
}