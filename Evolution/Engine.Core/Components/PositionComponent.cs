using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Core.Components
{
    public class PositionComponent : IComponent
    {
        public ComponentType Type => ComponentType.COMPONENT_POSITION;

        public Vector2 Position { get; set; }

        public float Angle { get; set; } // TODO: Create RotationComponent

        public PositionComponent Parent { get; set; }

        public PositionComponent() : this(Vector2.Zero) { }

        public PositionComponent(float x, float y) : this(new Vector2(x, y)) { }

        public PositionComponent(Vector2 pos)
        {
            Position = pos;
        }
    }
}
