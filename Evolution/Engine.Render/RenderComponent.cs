﻿using Engine.Core;
using Engine.Render.Core.Data;
using Engine.Render.Core.Shaders.Enums;
using Engine.Render.Core.VAO;
using Engine.Render.Core.VAO.Instanced;

namespace Engine.Render
{
    public class RenderComponent : IComponent
    {
        public VertexArray VertexArray { get; set; }

        internal VertexArrayObject VertexArrayObject { get; set; }

        public ShaderType Shader { get; set; } = ShaderType.Standard;

        public ComponentType Type => ComponentType.COMPONENT_RENDER;

        public float MinZoom { get; set; } = 0.0f;


        public RenderComponent(VertexArray va)
        {
            VertexArray = va;
            VertexArrayObject = new VertexArrayObject(va);
        }

        public RenderComponent(VertexArray va, InstanceSettings settings)
        {
            VertexArray = va;

            var iVAO = new InstancedVertexArrayObject(va, settings.Instances);
            VertexArrayObject = iVAO;
        }

        public void UpdateInstanceSettings(InstanceSettings settings)
        {
            if (VertexArrayObject is InstancedVertexArrayObject iVAO)
            {
                iVAO.Update(settings.Instances);
            }
        }
    }
}
