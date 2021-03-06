using Engine.Render.Core.Data.Primitives;
using Engine.Render.Core.VAO;
using OpenTK.Graphics.OpenGL4;

namespace Engine.Render.Core.Buffers.Util
{
    public class FrameBufferRenderer
    {
        private readonly VertexArrayObject _vao;

        public FrameBufferRenderer()
        {
            var shape = Rectangle.Generate(2, 2);
            _vao = new VertexArrayObject(shape);
        }

        public void Load()
        {
            _vao.Initialise(Shaders.Shaders.All);
            _vao.Load();
        }

        public void Render(FrameBufferObject fbo1, FrameBufferObject fbo2) => Render(fbo1, fbo2.Id);

        public void RenderToScreen(FrameBufferObject fbo) => Render(fbo, 0);

        private void Render(FrameBufferObject fbo, int fboToRenderTo)
        {
            GL.BlendFunc(BlendingFactor.One, BlendingFactor.OneMinusSrcAlpha);

            Shaders.Shaders.FBORender.Bind();
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, fboToRenderTo);
            fbo.Texture.Bind();
            fbo.RenderBuffer.Bind();

            _vao.Bind();
            _vao.Render(Shaders.Shaders.FBORender);
        }
    }
}
