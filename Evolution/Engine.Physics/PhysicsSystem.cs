using Box2DX.Collision;
using Box2DX.Dynamics;
using Engine.Core;
using Engine.Core.Components;
using Engine.Core.Events.Input.Mouse;
using OpenTK.Mathematics;
using Redbus.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine.Physics
{
    public class PhysicsSystem : SystemBase, ISystem
    {
        private readonly World _world;
        private readonly IEventBus _eventBus;

        private List<PhysicsComponent> components;

        public PhysicsSystem(Vector2 gravity, IEventBus eventBus)
        {
            var space = new AABB()
            {
                LowerBound = new Box2DX.Common.Vec2(-100000, -100000),
                UpperBound = new Box2DX.Common.Vec2(100000, 100000)
            };
            _world = new World(space, new Box2DX.Common.Vec2(gravity.X, gravity.Y), true);

            Mask = ComponentType.COMPONENT_PHYSICS | ComponentType.COMPONENT_POSITION;

            _eventBus = eventBus;

            components = new List<PhysicsComponent>();

            _eventBus.Subscribe<MouseDownEvent>(x =>
            {
                if (!GetDebuggable().Any()) return;

                if (x.Button == OpenTK.Windowing.GraphicsLibraryFramework.MouseButton.Left)
                {
                    var bodies = GetDebuggable().ToArray();
                    foreach (var body in bodies)
                    {
                        body.PhysicsBody.ApplyForce(new Vector2(0, 100));
                    }
                }
            });
        }

        public override void OnUpdate(float deltaTime)
        {
            _world.Step(1.0f / 60.0f, 10, 3);
        }

        public override void OnUpdate(Entity entity, float deltaTime)
        {
            if (!MaskMatch(entity)) return;

            var physicsComponent = entity.GetComponent<PhysicsComponent>();
            var positionComponent = entity.GetComponent<PositionComponent>();

            if (!physicsComponent.PhysicsBody.Initialised) InitialisePhysicsBody(physicsComponent, positionComponent.Position, positionComponent.Angle);

            positionComponent.Position = physicsComponent.PhysicsBody.Position;
            positionComponent.Angle = physicsComponent.PhysicsBody.Angle;
        }

        private void InitialisePhysicsBody(PhysicsComponent component, Vector2 pos, float angle)
        {
            component.PhysicsBody.Initialise(_world.CreateBody);
            component.PhysicsBody.Position = pos;
            component.PhysicsBody.Angle = angle;

            components.Add(component);
        }

        private IEnumerable<PhysicsComponent> GetDebuggable() => components.Where(x => x.PhysicsBody.Debug);
    }
}
