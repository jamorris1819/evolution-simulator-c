using Box2DX.Collision;
using Box2DX.Dynamics;
using Engine.Core;
using Engine.Core.Components;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Physics
{
    public class PhysicsSystem : SystemBase, ISystem
    {
        private readonly World _world;

        public PhysicsSystem(Vector2 gravity)
        {
            var space = new AABB()
            {
                LowerBound = new Box2DX.Common.Vec2(-100, -100),
                UpperBound = new Box2DX.Common.Vec2(100, 100)
            };
            _world = new World(space, new Box2DX.Common.Vec2(gravity.X, gravity.Y), true);

            Mask = ComponentType.COMPONENT_PHYSICS | ComponentType.COMPONENT_POSITION;
        }

        public override void OnUpdate(float deltaTime)
        {
            _world.Step(1.0f / 60.0f, 10, 10);
        }

        public override void OnUpdate(Entity entity, float deltaTime)
        {
            if (!MaskMatch(entity)) return;

            var physicsComponent = entity.GetComponent<PhysicsComponent>();

            if (!physicsComponent.PhysicsBody.Initialised) InitialisePhysicsBody(physicsComponent);

            var positionComponent = entity.GetComponent<PositionComponent>();
            positionComponent.Position = physicsComponent.PhysicsBody.Position;
        }

        private void InitialisePhysicsBody(PhysicsComponent component) => component.PhysicsBody.Initialise(_world.CreateBody);
    }
}
