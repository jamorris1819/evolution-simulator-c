using Engine.Core;
using Engine.Core.Components;
using Engine.Core.Events.Input.Keyboard;
using OpenTK.Mathematics;
using Redbus.Interfaces;
using System.Collections.Generic;
using System.Linq;
using tainicom.Aether.Physics2D.Dynamics;
using tainicom.Aether.Physics2D.Dynamics.Joints;

namespace Engine.Physics
{
    public class PhysicsSystem : SystemBase, ISystem
    {
        private readonly World _world;
        private readonly IEventBus _eventBus;

        private List<PhysicsComponent> components;

        public World World => _world;

        public PhysicsSystem(Vector2 gravity, IEventBus eventBus)
        {
            _world = new World(new tainicom.Aether.Physics2D.Common.Vector2(gravity.X, gravity.Y));

            Mask = ComponentType.COMPONENT_PHYSICS | ComponentType.COMPONENT_POSITION;

            _eventBus = eventBus;

            components = new List<PhysicsComponent>();

            _world.CreateRectangle(50 * 4f, 10*4, 0, new tainicom.Aether.Physics2D.Common.Vector2(0, -10 * 4f));

            // Control debuggable creatures
            _eventBus.Subscribe<KeyDownEvent>(x =>
            {
                if (!GetDebuggable().Any()) return;

                if (x.Key == OpenTK.Windowing.GraphicsLibraryFramework.Keys.W)
                {
                    var bodies = GetDebuggable().ToArray();
                    foreach (var body in bodies)
                    {
                        body.PhysicsBody.MoveForward(3);
                    }
                }

                if (x.Key == OpenTK.Windowing.GraphicsLibraryFramework.Keys.A)
                {
                    var bodies = GetDebuggable().ToArray();
                    foreach (var body in bodies)
                    {
                        body.PhysicsBody.ApplyTorque(.4f);
                    }
                }

                if (x.Key == OpenTK.Windowing.GraphicsLibraryFramework.Keys.D)
                {
                    var bodies = GetDebuggable().ToArray();
                    foreach (var body in bodies)
                    {
                        body.PhysicsBody.ApplyTorque(-2f);
                    }
                }
            });
        }

        public override void OnUpdate(float deltaTime)
        {
            _world.Step(1.0f / 60.0f);
        }

        public override void OnUpdate(Entity entity, float deltaTime)
        {
            if (!MaskMatch(entity)) return;

            var physicsComponent = entity.GetComponent<PhysicsComponent>();
            var positionComponent = entity.GetComponent<PositionComponent>();

            positionComponent.Position = physicsComponent.PhysicsBody.Position;
            positionComponent.Angle = physicsComponent.PhysicsBody.Rotation;
        }

        private IEnumerable<PhysicsComponent> GetDebuggable() => components.Where(x => x.PhysicsBody.Debug);
    }
}
