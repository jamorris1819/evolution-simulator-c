using Engine.Render.Shaders;
using OpenTK.Graphics.ES30;
using OpenTK.Mathematics;
using Redbus.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine.Render
{
    public abstract class Camera
    {
        private IEventBus _eventBus;
        private ShaderManager _shaderManager;
        private int _width;
        private int _height;
        private Vector2 _lastPosition;

        public int PixelsPerMetre { get; set; } = 64;

        public Vector2 Position { get; protected set; }

        public float Scale { get; protected set; } = 1.0f;

        public Matrix4 Projection { get; private set; }

        public Vector4 Viewport { get; private set; }

        public Camera(int width, int height, IEventBus eventBus, ShaderManager shaderManager)
        {
            _width = width;
            _height = height;
            _eventBus = eventBus;
            _shaderManager = shaderManager;
        }

        public virtual void Update(double deltaTime)
        {
            //if (_lastPosition != Position)
            //{
                UpdateShaders();
            //}
        }

        public void UpdateShaders()
        {
            Matrix4 position = Matrix4.CreateTranslation(new Vector3(Position.X, Position.Y, 0));
            CreateProjection();
            
            for(int i = 0; i < _shaderManager.All.Count; i++)
            {
                Shader shader = _shaderManager.All[i];
                shader.SetUniformMat4(Shaders.Enums.ShaderUniforms.View, position);
                shader.SetUniformMat4(Shaders.Enums.ShaderUniforms.Projection, Projection);
            }
        }

        protected void CreateProjection()
        {
            float zoomWidth = (float)_width / Scale;
            float zoomHeight = ((float)_height / (float)_width) * zoomWidth;

            // Create the new projection.
            float boundaryWidth = zoomWidth / PixelsPerMetre;
            float boundaryHeight = zoomHeight / PixelsPerMetre;


            Projection = Matrix4.CreateOrthographic(boundaryWidth, boundaryHeight, -1.0f, 1.0f);
            Viewport = new Vector4(
                (_width - zoomWidth) / PixelsPerMetre,
                (_height - zoomHeight) / PixelsPerMetre,
                zoomWidth / PixelsPerMetre,
                zoomHeight / PixelsPerMetre);
        }
    }
}
