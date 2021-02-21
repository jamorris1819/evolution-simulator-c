using Engine.Core;
using Engine.Core.Managers;
using Engine.Physics;
using Engine.Physics.Core;
using Evolution.Environment.Life.Creatures.Legs;

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

            var speed = entity.GetComponent<PhysicsComponent>().PhysicsBody.LinearVelocity.Length * PhysicsSettings.InvScale;

            legsComponent.LeftSide.Update(deltaTime, speed);
            legsComponent.RightSide.Update(deltaTime, speed);

            var leftMoving = !((WalkingLimb)legsComponent.LeftSide).IsFootDown;
            var rightMoving = !((WalkingLimb)legsComponent.RightSide).IsFootDown;

            if (leftMoving)
            {
                entity.GetComponent<PhysicsComponent>().PhysicsBody.ApplyTorque(-6 * speed);
            }
            if (rightMoving)
            {
                entity.GetComponent<PhysicsComponent>().PhysicsBody.ApplyTorque(6 * speed);
            }
        }

        public void OnUpdate(float deltaTime)
        {
        }
    }
}
