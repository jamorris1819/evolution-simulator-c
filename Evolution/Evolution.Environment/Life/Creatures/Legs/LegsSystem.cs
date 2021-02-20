using Engine.Core;
using Engine.Core.Components;
using Engine.Core.Managers;
using Engine.Physics;
using Engine.Physics.Core;
using Engine.Render;
using Engine.Render.Core;
using Engine.Render.Core.Data.Primitives;
using Evolution.Environment.Life.Creatures.Legs;
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

            var legsComponent = entity.GetComponent<LimbComponent>();

            if (!legsComponent.Initialised)
            {
                /*legsComponent.LeftSide = new WalkingLimb(entity, _entityManager, legsComponent.LegModel);
                legsComponent.RightSide = new WalkingLimb(entity, _entityManager, legsComponent.LegModel.Flip());
                legsComponent.LeftSide.Counterpart = legsComponent.RightSide;
                legsComponent.RightSide.Counterpart = legsComponent.LeftSide;*/
                legsComponent.Initialised = true;
            }

            var speed = entity.GetComponent<PhysicsComponent>().PhysicsBody.LinearVelocity.Length * PhysicsSettings.InvScale;

            legsComponent.LeftSide.Update(deltaTime, speed);
            legsComponent.RightSide.Update(deltaTime, speed);

            var leftMoving = !((WalkingLimb)legsComponent.LeftSide).IsFootDown;
            var rightMoving = !((WalkingLimb)legsComponent.RightSide).IsFootDown;

            if (entity.GetComponent<PhysicsComponent>().PhysicsBody.LinearVelocity.Length > 1)
            {
                if (leftMoving)
                {
                    entity.GetComponent<PhysicsComponent>().PhysicsBody.ApplyTorque(4);
                }
                if (rightMoving)
                {
                    entity.GetComponent<PhysicsComponent>().PhysicsBody.ApplyTorque(-4);
                }
            }
        }

        public void OnUpdate(float deltaTime)
        {
        }
    }
}
