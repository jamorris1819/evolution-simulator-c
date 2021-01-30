using Engine.Core;
using Engine.Core.Components;
using Engine.Render;
using Engine.Render.Core.Data;
using Engine.Render.Core.VAO.Instanced;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evolution.Environment.Food
{
    public class FoodSource
    {
        private Vector2 _position;
        private VertexArray _shape;
        private readonly List<FoodInstance> _instances;

        public Entity Entity { get; private set; }

        public Vector2 Position => _position;

        public FoodSource(Vector2 position, VertexArray shape, List<FoodInstance> instances)
        {
            _position = position;
            _shape = shape;
            _instances = instances;
        }

        public void Initialise()
        {
            Entity = new Entity("food source");

            var instanceSettings = new InstanceSettings()
            {
                Instances = _instances.Select(x => new Instance()
                {
                    Colour = x.Colour,
                    Position = x.Position
                }).ToArray()
            };

            var renderComponent = new RenderComponent(_shape, instanceSettings, true);
            renderComponent.Shaders.Add(Engine.Render.Core.Shaders.Enums.ShaderType.Outline);
            renderComponent.Shaders.Add(Engine.Render.Core.Shaders.Enums.ShaderType.InstancedRotated);
            //renderComponent.MinZoom = 0.15f;
            Entity.AddComponent(renderComponent);
            Entity.AddComponent(new PositionComponent(Position));
        }

        public void Add(int count = 1)
        {
            var vao = Entity.GetComponent<RenderComponent>().VertexArrayObject as VariableInstancedVAO;
            vao.Add(count);
        }

        public void Remove(int count = 1)
        {
            var vao = Entity.GetComponent<RenderComponent>().VertexArrayObject as VariableInstancedVAO;
            vao.Remove(count);
        }
    }
}
