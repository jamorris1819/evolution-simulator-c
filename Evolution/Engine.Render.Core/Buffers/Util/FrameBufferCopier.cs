using OpenTK.Graphics.OpenGL4;

namespace Engine.Render.Core.Buffers.Util
{
    public static class FrameBufferCopier
    {
        public static void Copy(FrameBufferObject fbo1, FrameBufferObject fbo2)
        {
            GL.BindFramebuffer(FramebufferTarget.ReadFramebuffer, fbo1.Id);
            GL.BindFramebuffer(FramebufferTarget.DrawFramebuffer, fbo2.Id);
            GL.BlitFramebuffer(0, 0, 1920, 1080, 0, 0, 1920, 1080, ClearBufferMask.ColorBufferBit, BlitFramebufferFilter.Nearest);
        }
    }
}
