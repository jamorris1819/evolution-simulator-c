using Engine.Render.Core;
using Engine.Render.Core.Data;
using Engine.Render.Core.Data.Primitives;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Evolution.Environment.Life.Creatures.Mouth.ConstructionModels
{
    public readonly struct PincerModel
    {
        public float Length { get; }
        public float CurveHeight { get; }
        public float BaseDiameter { get; }
        public float Thickness { get; }

        public PincerModel(float length, float curveHeight, float baseDiameter, float thickness)
        {
            Length = length;
            CurveHeight = curveHeight;
            BaseDiameter = Math.Min(baseDiameter, 0.5f);
            Thickness = thickness;
        }

        public VertexArray GenerateShape(int resolution)
        {
            var origin = new Vector2(BaseDiameter / 2, 0);

            var topCurve = GetTopCurvePoints(resolution);
            var bottomCurve = GetBottomCurvePoints(resolution);

            var pincer = CombineCurves(topCurve.ToArray(), bottomCurve.ToArray());
            pincer = VertexHelper.Translate(pincer, -origin * Length);
            var semiCircle = Polygon.Generate(CreateSemiCircle(resolution).ToList());

            var fullPincer = VertexHelper.Combine(pincer, semiCircle);

            return fullPincer;
        }

        private IEnumerable<Vector2> CreateSemiCircle(int resolution)
        {
            float step = 2f * (float)Math.PI / (resolution - 1);
            float radius = BaseDiameter * 0.5f;

            for (int i = 0; i < resolution; i++)
            {
                var direction = new Vector2((float)Math.Cos(i * step), -(float)Math.Sin(i * step));

                yield return direction * radius * new Vector2(Length, 1);
            }
        }

        private IEnumerable<Vector2> GetTopCurvePoints(int resolution)
        {
            float step = 1f / (resolution - 1);
            float curveHeight = Math.Max(CurveHeight, 0.2f);
            var curve = new BezierCurveQuadric(new Vector2(0), new Vector2(1, 0), new Vector2(0.5f, curveHeight));

            for (int i = 0; i < resolution; i++)
            {
                yield return curve.CalculatePoint(i * step) * new Vector2(Length, 1);
            }
        }

        private IEnumerable<Vector2> GetBottomCurvePoints(int resolution)
        {
            float step = 1f / (resolution - 1);
            float curveHeight = Math.Max(CurveHeight, 0.2f);
            var thickness = Math.Max(0.1f, curveHeight - Thickness);
            var curve = new BezierCurveQuadric(new Vector2(BaseDiameter, 0), new Vector2(1, 0), new Vector2(0.5f, thickness));

            for (int i = 0; i < resolution; i++)
            {
                yield return curve.CalculatePoint(i * step) * new Vector2(Length, 1);
            }
        }

        /// <summary>
        /// Combines two curves into a shape by converting to triangles
        /// </summary>
        private VertexArray CombineCurves(Vector2[] topCurve, Vector2[] bottomCurve)
        {
            var vertices = new List<Vertex>();

            for (int i = 0; i < topCurve.Length - 1; i++)
            {
                var t1v1 = new Vertex(topCurve[i].ToVector3());
                var t1v2 = new Vertex(bottomCurve[i].ToVector3());
                var t1v3 = new Vertex(bottomCurve[i + 1].ToVector3());

                vertices.Add(t1v1);
                vertices.Add(t1v2);
                vertices.Add(t1v3);


                var t2v1 = new Vertex(topCurve[i + 1].ToVector3());
                var t2v2 = new Vertex(topCurve[i].ToVector3());
                var t2v3 = new Vertex(bottomCurve[i + 1].ToVector3());

                vertices.Add(t2v1);
                vertices.Add(t2v2);
                vertices.Add(t2v3);
            }

            var va = new VertexArray(vertices.ToArray(), Enumerable.Range(0, vertices.Count).Select(x => (ushort)x).ToArray()); // TODO: stop duplication of vertices

            return va;
        }
    }
}
