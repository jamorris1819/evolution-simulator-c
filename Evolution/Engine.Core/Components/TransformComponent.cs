using OpenTK.Mathematics;

namespace Engine.Core.Components
{
    public class TransformComponent : IComponent
    {
        public ComponentType Type => ComponentType.COMPONENT_TRANSFORM;

        public Vector2 Position { get; set; }

        public float Angle { get; set; }

        public TransformComponent Parent { get; set; }

        public TransformComponent() : this(Vector2.Zero) { }

        public TransformComponent(float x, float y) : this(new Vector2(x, y)) { }

        public TransformComponent(Vector2 pos)
        {
            Position = pos;
        }
    }
}
