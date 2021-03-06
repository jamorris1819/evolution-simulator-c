using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Render.Core.Buffers.RenderBuffers
{
    public class RenderBufferObject
    {
        private int _bufferId = 1;

        public bool Initialised { get; private set; }

        public void Initialise(int width, int height)
        {
            if (Initialised) throw new RenderException("The RBO is already initialised");

            _bufferId = GL.GenRenderbuffer();
            GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, _bufferId);

            Configure(width, height);

            GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, 0);

            Initialised = true;
        }

        public void AttachToFBO(FrameBufferObject fbo)
        {
            if (!Initialised) throw new RenderException("RBO must be initialised before attaching to FBO");
            if (!fbo.Initialised) throw new RenderException("Cannot attach to uninitialised FBO");

            fbo.Bind();
            GL.FramebufferRenderbuffer(FramebufferTarget.Framebuffer, FramebufferAttachment.DepthStencilAttachment, RenderbufferTarget.Renderbuffer, _bufferId);
        }

        public void Bind() => GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, _bufferId);

        protected virtual void Configure(int width, int height)
        {
            GL.RenderbufferStorage(RenderbufferTarget.Renderbuffer, RenderbufferStorage.Depth24Stencil8, width, height);
        }
    }
}
