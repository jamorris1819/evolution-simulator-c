using Engine.Render.Core.Buffers.RenderBuffers;
using Engine.Render.Core.Textures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine.Render.Core.Buffers.Util
{
    public class FrameBufferManager
    {
        private readonly Dictionary<int, FrameBufferObject> _fbos;
        private readonly FrameBufferRenderer _fboRenderer;

        private int _width;
        private int _height;

        public FrameBufferObject IntermediateBuffer { get; private set; }

        public FrameBufferObject CombinedBuffer { get; private set; }

        public FrameBufferManager(FrameBufferRenderer fboRenderer, int width, int height)
        {
            _fbos = new Dictionary<int, FrameBufferObject>();
            _fboRenderer = fboRenderer;
            _width = width;
            _height = height;

            IntermediateBuffer = new FrameBufferObject(new Texture2D(), new RenderBufferObject());
            CombinedBuffer = new FrameBufferObject(new Texture2D(), new RenderBufferObject());

            IntermediateBuffer.Initialise(_width, _height);
            CombinedBuffer.Initialise(_width, _height);
        }

        public FrameBufferObject GetFBO(int id)
        {
            if (!_fbos.TryGetValue(id, out var fbo))
            {
                fbo = new FrameBufferObject(new Texture2DMultisample(), new RenderBufferObjectMultisample());
                fbo.Initialise(_width, _height);
                _fbos.Add(id, fbo);
            }

            return fbo;
        }

        public void CombineLayers()
        {
            CombinedBuffer.Clear();

            var keys = _fbos.Keys.Select(x => x).ToArray();

            for(int i = 0; i < keys.Length; i++)
            {
                int key = keys[i];

                // Copy the rendered FBO
                IntermediateBuffer.Clear();
                FrameBufferCopier.Copy(_fbos[key], IntermediateBuffer);

                // Draw it to the combined buffer
                _fboRenderer.Render(IntermediateBuffer, CombinedBuffer);
            }
        }
    }
}
