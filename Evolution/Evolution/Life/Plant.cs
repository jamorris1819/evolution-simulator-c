using Engine.Core;
using Engine.Core.Components;
using Engine.Render;
using Engine.Render.Core;
using Engine.Render.Core.Data;
using Engine.Render.Core.Data.Primitives;
using Engine.Render.Core.VAO.Instanced;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Evolution.Life
{
    public class Plant
    {
        public static Entity Generate(List<Vector2> points)
        {
            Entity entity = new Entity("plant");

            var tri = layer(0);
            tri = VertexHelper.Combine(tri, layer(1));



            Random random = new Random();

            int ee = 0;

            var instances = points.Select(x => new Instance()
            {
                Position = x,
                Colour = new Vector3(0, 0.5f, 0) + new Vector3((float)(random.NextDouble() * 0.1f))
            }).ToArray();

            entity.AddComponent(new RenderComponent(tri, new Engine.Render.Core.VAO.Instanced.InstanceSettings()
            {
                Instances = instances
            })
            { MinZoom = 0.1f, Shader = Engine.Render.Core.Shaders.Enums.ShaderType.InstancedRotated });
            entity.AddComponent(new PositionComponent());

            return entity;
        }

        private static VertexArray leaf()
        {
            float width = 0.3f;
            BezierCurveCubic a = new BezierCurveCubic(
                new Vector2(0, 0),
                new Vector2(0, 1),
                new Vector2(width * 1.5f, 0f),
                new Vector2(width, 1f));

            var resolution = 16;

            float step = 1.0f / resolution;

            Vector2[] points = new Vector2[resolution + 1];
            for(int i = 0; i < points.Length; i++)
            {
                points[i] = a.CalculatePoint(i * step);
            }

            Vector2[] flippedPoints = new Vector2[resolution + 1];
            points.CopyTo(flippedPoints, 0);
            flippedPoints = flippedPoints.Reverse().Select(x => x * new Vector2(-1, 1)).ToArray();

            var allPoints = new List<Vector2>();
            allPoints.AddRange(points);
            allPoints.AddRange(flippedPoints);

            return Polygon.Generate(allPoints.ToList());
        }

        private static VertexArray layer(float angle)
        {
            var tri = new VertexArray(new Vertex[0], new ushort[0]);

            int count = 6;
            float step = (float)(Math.PI * 2) / count;

            for (int i = 0; i < count; i++)
            {
                var tri2 = leaf();
                tri2 = VertexHelper.Rotate(tri2, i * step);
                tri = VertexHelper.Combine(tri, tri2);
            }

            tri = VertexHelper.Scale(tri, 0.1f);
            /*var dup = tri.Copy();
            dup.Rotate(1);

            tri.Add(dup);

            tri.SetColour(new OpenTK.Mathematics.Vector3(0, 0.6f, 0));
            tri.Rotate(angle);*/
            return tri;
        }
    }
}
