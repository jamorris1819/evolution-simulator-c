using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Render.Core.Textures
{
    public class Texture2DMultisample : Texture
    {
        public Texture2DMultisample() : base(TextureTarget.Texture2DMultisample) { }

        protected override void ConfigureTexture(int width, int height)
        {
            GL.TexImage2DMultisample(TextureTargetMultisample.Texture2DMultisample, 4, PixelInternalFormat.Rgb, width, height, true);

            GL.TexParameter(TextureTarget.Texture2DMultisample, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2DMultisample, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
        }
    }
}
