using Engine.Core;
using Engine.Render.Core.Data;
using Engine.Render.Core.Shaders.Enums;
using Engine.Render.Core.VAO;
using Engine.Render.Core.VAO.Instanced;
using System.Collections.Generic;
using System.Linq;

namespace Engine.Render
{
    public class RenderComponent : IComponent
    {
        public VertexArray VertexArray { get; set; }

        public VertexArrayObject VertexArrayObject { get; set; }

        public List<ShaderType> Shaders { get; set; } = new List<ShaderType>();

        public ComponentType Type => ComponentType.COMPONENT_RENDER;

        public float MinZoom { get; set; } = 0.0f;

        public float? MaxZoom { get; set; }


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
    }
}
