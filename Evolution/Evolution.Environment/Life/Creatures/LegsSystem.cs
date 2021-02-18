using Engine.Core;
using Engine.Core.Components;
using Engine.Core.Managers;
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
                legsComponent.LeftSide = new Leg(entity, new Vector2(-0.1f, 0), 0.2f, 2);
                legsComponent.RightSide = new Leg(entity, new Vector2(0.1f, 0), 0.2f, 2);
                legsComponent.RightSide.Initialise(_entityManager);
                legsComponent.LeftSide.Initialise(_entityManager);
                legsComponent.Initialised = true;
            }

            legsComponent.LeftSide.SetFootPosition(new Vector2(0));
            legsComponent.RightSide.SetFootPosition(new Vector2(0));
        }

        public void OnUpdate(float deltaTime)
        {
        }
    }
}
