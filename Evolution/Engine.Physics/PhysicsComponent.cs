using Engine.Core;
using Engine.Physics.Core;
using System;

namespace Engine.Physics
{
    public class PhysicsComponent : IComponent
    {
        public ComponentType Type => ComponentType.COMPONENT_PHYSICS;

        public PhysicsBody PhysicsBody { get; private set; }

        public PhysicsComponent(PhysicsBody body)
        {
            PhysicsBody = body;
        }
    }
}
