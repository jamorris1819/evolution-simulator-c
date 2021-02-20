using Engine.Core;
using Engine.Core.Components;
using Engine.Core.Managers;
using Evolution.Genetics.Modules.Limbs;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Evolution.Environment.Life.Creatures.Limbs
{
    public abstract class Limb
    {
        protected readonly PositionComponent _parentPosition;

        protected Vector2 _footPosition;

        public bool Initialised { get; protected set; }

        public Limb Counterpart { get; set; }

        public abstract LimbType LimbType { get; }

        public Limb(Entity parent, EntityManager entityManager)
        {
            _parentPosition = parent.GetComponent<PositionComponent>();
        }

        public abstract void Initialise(EntityManager entityManager);

        public abstract void Update(float deltaTime, float bodySpeed);
    }
}
