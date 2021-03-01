using Engine.Render.Core.Buffers;
using OpenTK.Graphics.OpenGL4;

namespace Engine.Render.Core.Textures
{
    public abstract class Texture
    {
        protected int _textureId = 1;

        public bool Initialised { get; protected set; }

        public TextureTarget Target { get; private set; }

        public Texture(TextureTarget target)
        {
            Target = target;
        }

        public void Initialise(int width, int height)
        {
            if (Initialised) return;


            _textureId = GL.GenTexture();
            GL.BindTexture(Target, _textureId);

            ConfigureTexture(width, height);

            GL.BindTexture(Target, 0);

            Initialised = true;
        }

        public void Bind()
        {
            if (!Initialised) throw new RenderException("Cannot bind an un-initialised texture");
            GL.BindTexture(Target, _textureId);
        }

        public void AttachToFBO(FrameBufferObject fbo)
        {
            if (!fbo.Initialised) throw new RenderException("Cannot attach to uninitialised FBO");
            fbo.Bind();

            GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0, Target, _textureId, 0);
        }

        protected abstract void ConfigureTexture(int width, int height);
    }
}
