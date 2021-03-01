using Engine.Render.Core.Buffers;
using OpenTK.Graphics.OpenGL4;
using System;

namespace Engine.Render.Core.Textures
{
    public class Texture2D : Texture
    {
        public Texture2D() : base(TextureTarget.Texture2D) { }

        protected override void ConfigureTexture(int width, int height)
        {
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb, width, height, 0, PixelFormat.Rgb, PixelType.UnsignedByte, IntPtr.Zero);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
        }
    }
}
