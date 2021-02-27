using Engine.Core;
using Engine.Render.Core.Data;
using Engine.Render.Core.Data.Zoom;
using Engine.Render.Core.Shaders;
using Engine.Render.Core.Shaders.Enums;
using Engine.Render.Core.VAO;
using Engine.Render.Core.VAO.Instanced;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Engine.Render
{
    public class RenderComponent : IComponent
    {
        private float _alpha = 1f;

        public VertexArray VertexArray { get; set; }

        public VertexArrayObject VertexArrayObject { get; set; }

        public List<ShaderConfiguration> Shaders { get; set; } = new List<ShaderConfiguration>();

        public bool Outlined { get; set; }

        public ShaderType OutlineShader { get; set; }

        public ComponentType Type => ComponentType.COMPONENT_RENDER;

        public ZoomProfile? ZoomProfile { get; set; }

        public int Layer { get; set; }

        public float Alpha
        {
            get => _alpha;
            set
            {
                _alpha = Math.Max(0, Math.Min(1, value));
            }
        }

        public RenderComponent(VertexArray va)
        {
            VertexArray = va;
            VertexArrayObject = new VertexArrayObject(va);
        }

        public RenderComponent(VertexArray va, InstanceSettings settings) : this(va, settings, false) { }

        public RenderComponent(VertexArray va, InstanceSettings settings, bool variableCount)
        {
            VertexArray = va;

            if (variableCount)
            {
                var iVAO = new VariableInstancedVAO(va, settings.Instances);
                VertexArrayObject = iVAO;
            }
            else
            {
                var iVAO = new InstancedVertexArrayObject(va, settings.Instances);
                VertexArrayObject = iVAO;
            }
        }

        public void UpdateInstanceSettings(InstanceSettings settings, bool reload)
        {
            if (VertexArrayObject is InstancedVertexArrayObject iVAO)
            {
                if(reload) iVAO.Update(settings.Instances.ToList());
                else iVAO.Update(settings.Instances);
            }
        }

        public void Update(VertexArray va)
        {
            VertexArray = va;
            VertexArrayObject.VBO.First(x => x.Name == "Vertex data").QueueReload();
            VertexArrayObject.VBO.First(x => x.Name == "Vertex data (index)").QueueReload();
        }
    }
}
