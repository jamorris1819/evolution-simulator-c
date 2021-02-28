using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Render.Core.Shaders
{
    public class FrameBufferObject
    {
        private int _bufferId = -1;
        private int _textureId = -1;
        private int _rboId = -1;

        public void Initialise()
        {
            CreateFrameBuffer();
            Bind();

            CreateTexture();
            AttachTextureToFBO();

            CreateRBO();
            AttachRBOToFBO();

            if (GL.CheckFramebufferStatus(FramebufferTarget.Framebuffer) != FramebufferErrorCode.FramebufferComplete)
                throw new RenderException("The framebuffer is not ready");

            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
        }

        private void CreateFrameBuffer()
        {
            _bufferId = GL.GenFramebuffer();
        }

        private void CreateTexture()
        {
            _textureId = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2DMultisample, _textureId);

            //GL.TexImage2D(TextureTarget2d.Texture2D, 0, TextureComponentCount.Rgb, 1920, 1080, 0, PixelFormat.Rgb, PixelType.UnsignedByte, IntPtr.Zero);
            GL.TexImage2DMultisample(TextureTargetMultisample.Texture2DMultisample, 4, PixelInternalFormat.Rgb, 1920, 1080, true);

            GL.TexParameter(TextureTarget.Texture2DMultisample, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2DMultisample, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            GL.BindTexture(TextureTarget.Texture2DMultisample, 0);
        }

        private void CreateRBO()
        {
            _rboId = GL.GenRenderbuffer();
            GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, _rboId);
            GL.RenderbufferStorageMultisample(RenderbufferTarget.Renderbuffer, 4, RenderbufferStorage.Depth24Stencil8, 1920, 1080);
            GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, 0);
        }

        private void AttachTextureToFBO()
        {
            GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0, TextureTarget.Texture2DMultisample, _textureId, 0);
        }

        private void AttachRBOToFBO()
        {
            GL.FramebufferRenderbuffer(FramebufferTarget.Framebuffer, FramebufferAttachment.DepthStencilAttachment, RenderbufferTarget.Renderbuffer, _rboId);
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
            BindTexture();
        }

        public void BindTexture()
        {
            GL.BindTexture(TextureTarget.Texture2DMultisample, _textureId);
        }
    }
}
