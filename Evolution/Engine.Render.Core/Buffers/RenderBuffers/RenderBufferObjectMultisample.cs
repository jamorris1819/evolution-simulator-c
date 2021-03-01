using OpenTK.Graphics.OpenGL4;

namespace Engine.Render.Core.Buffers.RenderBuffers
{
    public class RenderBufferObjectMultisample : RenderBufferObject
    {
        protected override void Configure(int width, int height)
        {
            GL.RenderbufferStorageMultisample(RenderbufferTarget.Renderbuffer, 4, RenderbufferStorage.Depth24Stencil8, 1920, 1080);
        }
    }
}
