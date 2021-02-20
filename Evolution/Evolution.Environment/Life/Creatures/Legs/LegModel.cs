using Engine.Render.Core.Data;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Evolution.Environment.Life.Creatures.Legs
{
    /// <summary>
    /// Holds data describing how a leg looks.
    /// </summary>
    public readonly struct LegModel
    {
        public VertexArray Segment1 { get; }

        public VertexArray Segment2 { get; }

        public float Length { get; }

        public float LegDirection { get; }

        public Vector2 BaseOffset { get; }

        public LegModel(VertexArray s1, VertexArray s2, float length, float legDirection, Vector2 baseOffset)
        {
            Segment1 = s1;
            Segment2 = s2;
            Length = length;
            LegDirection = legDirection;
            BaseOffset = baseOffset;
        }

        public LegModel Flip()
            => new LegModel(Segment1, Segment2, Length, -LegDirection, BaseOffset * new Vector2(-1, 1));
    }
}
