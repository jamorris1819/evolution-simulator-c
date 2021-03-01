using Engine.Render.Core.Buffers.RenderBuffers;
using Engine.Render.Core.Textures;
using OpenTK.Graphics.OpenGL4;

namespace Engine.Render.Core.Buffers
{
    public class FrameBufferObject
    {
        private readonly Texture _texture;
        private readonly RenderBufferObject _rbo;

        private int _bufferId = -1;

        public int Id => _bufferId;

        public bool Initialised { get; private set; }

        public FrameBufferObject(Texture tex, RenderBufferObject rbo)
        {
            _texture = tex;
            _rbo = rbo;
        }

        public void Initialise(int width, int height)
        {
            if (Initialised) throw new RenderException("Cannot initialise as it is already initialised");

            CreateFrameBuffer();
            Bind();

            _texture.Initialise(width, height);
            _rbo.Initialise(width, height);

            Initialised = true;

            _texture.AttachToFBO(this);
            _rbo.AttachToFBO(this);

            if (GL.CheckFramebufferStatus(FramebufferTarget.Framebuffer) != FramebufferErrorCode.FramebufferComplete)
                throw new RenderException("The framebuffer is not ready");

            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
        }

        private void CreateFrameBuffer()
        {
            _bufferId = GL.GenFramebuffer();
        }
      
        public void Clear()
        {
            Bind();
            GL.ClearColor(0.3f, 0.5f, 0.1f, 1.0f);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit | ClearBufferMask.StencilBufferBit);
        }

        public void Bind()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, _bufferId);
        }
    }
}
