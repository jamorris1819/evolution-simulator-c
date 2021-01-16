using Engine.Core;
using Engine.Render;
using Engine.Render.Data.Primitives;
using Engine.Render.VAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evolution.Life
{
    public class Plant
    {
        public static Entity Generate()
        {
            Entity entity = new Entity("plant");

            var tri = Triangle.Generate(2, 1);
            

            int count = 6;
            float step = (float)(Math.PI * 2) / count;


            /*for (int i = 1; i < count; i++)
            {
                var tri2 = new Triangle(2, 1);
                tri2.Generate();
                tri2.Rotate(i * step);

                tri.Add(tri2);
            }*/

            tri.Scale(0.1f);

            tri.SetColour(new OpenTK.Mathematics.Vector3(0, 0.6f, 0));            

            entity.AddComponent(new RenderComponent(tri) { MinZoom = 0.1f });

            return entity;
        }
    }
}
