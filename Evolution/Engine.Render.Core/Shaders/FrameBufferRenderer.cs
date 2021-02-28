using Engine.Render.Core.Data.Primitives;
using Engine.Render.Core.VAO;
using OpenTK.Graphics.ES30;
using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Render.Core.Shaders
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
            _vao.Initialise(Shaders.All);
            _vao.Load();
        }

        public void Render(FrameBufferObject fbo)
        {
            Shaders.FBORender.Bind();
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
            fbo.BindTexture();

            _vao.Bind();
            _vao.Render(Shaders.FBORender);
        }
    }
}
