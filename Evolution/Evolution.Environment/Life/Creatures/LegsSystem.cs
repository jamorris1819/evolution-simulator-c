using Engine.Core;
using Engine.Core.Components;
using Engine.Core.Managers;
using Engine.Physics;
using Engine.Physics.Core;
using Engine.Render;
using Engine.Render.Core;
using Engine.Render.Core.Data.Primitives;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Evolution.Environment.Life.Creatures
{
    public class LegsSystem : SystemBase, ISystem
    {
        private EntityManager _entityManager;

        public LegsSystem(EntityManager manager)
        { 
            _entityManager = manager;
            Mask = ComponentType.COMPONENT_LEGS;
        }

        public void OnRender(Entity entity)
        {
        }

        public void OnUpdate(Entity entity, float deltaTime)
        {
            if (!MaskMatch(entity)) return;

            var legsComponent = entity.GetComponent<LegsComponent>();

            if (!legsComponent.Initialised)
            {
                Random random = new Random();
                legsComponent.LeftSide = new Leg(entity, new Vector2(-0.3f, 0), 0.75f, 0.5f, (float)random.NextDouble());
                legsComponent.RightSide = new Leg(entity, new Vector2(0.3f, 0), -0.75f, 0.5f, (float)random.NextDouble());
                legsComponent.RightSide.Initialise(_entityManager);
                legsComponent.LeftSide.Initialise(_entityManager);
                legsComponent.LeftSide.Counterpart = legsComponent.RightSide;
                legsComponent.RightSide.Counterpart = legsComponent.LeftSide;
                legsComponent.Initialised = true;
            }

            var speed = entity.GetComponent<PhysicsComponent>().PhysicsBody.LinearVelocity.Length * PhysicsSettings.InvScale;

            legsComponent.LeftSide.Update(deltaTime, speed);
            legsComponent.RightSide.Update(deltaTime, speed);
        }

        public void OnUpdate(float deltaTime)
        {
        }
    }
}
