using Engine.Core;
using Evolution.Environment.Life.Creatures.Legs;
using Evolution.Environment.Life.Creatures.Limbs;

namespace Evolution.Environment.Life.Creatures
{
    public class LimbComponent : IComponent
    {
        public ComponentType Type => ComponentType.COMPONENT_LEGS;

        public Limb LeftSide { get; set; }
        public Limb RightSide { get; set; }
        public bool Initialised { get; set; }
        public LegModel LegModel { get; set; }
    }
}
