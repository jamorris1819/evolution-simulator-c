using Engine.Core.Managers;
using Evolution.Genetics;
using Evolution.Genetics.Modules.Limbs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Evolution.Environment.Life.Creatures.Limbs.Factory
{
    public static class LimbFactoryCreator
    {
        public static LimbFactory Get(EntityManager entityManager, LimbType type)
        {
            switch (type)
            {
                case LimbType.WalkingLeg:
                    return new WalkingLimbFactory()
                    {
                        EntityManager = entityManager
                    };
                default:
                    throw new ArgumentOutOfRangeException(nameof(type));
            }
        }
    }
}
