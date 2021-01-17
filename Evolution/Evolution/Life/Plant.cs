using Engine.Core;
using Engine.Core.Components;
using Engine.Render;
using Engine.Render.Core.Data.Primitives;
using Engine.Render.Core.VAO.Instanced;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Evolution.Life
{
    public class Plant
    {
        public static Entity Generate(List<Vector2> points)
        {
            Entity entity = new Entity("plant");

            var tri = Triangle.Generate(2, 1);
            

            int count = 6;
            float step = (float)(Math.PI * 2) / count;


            for (int i = 1; i < count; i++)
            {
                var tri2 = Triangle.Generate(2, 1);
                tri2.Rotate(i * step);

                tri.Add(tri2);
            }

            tri.Scale(0.1f);

            tri.SetColour(new OpenTK.Mathematics.Vector3(0, 0.6f, 0));            

            entity.AddComponent(new RenderComponent(tri, new Engine.Render.Core.VAO.Instanced.InstanceSettings()
            {
                Instances = points.Select(x => new Instance()
                {
                    Position = x,
                    Colour = new Vector3(0, 0.6f, 0)
                }).ToArray()
            }) { MinZoom = 0.1f, Shader = Engine.Render.Core.Shaders.Enums.ShaderType.StandardInstanced });
            entity.AddComponent(new PositionComponent());

            return entity;
        }
    }
}
