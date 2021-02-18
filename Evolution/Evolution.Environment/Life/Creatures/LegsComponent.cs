using Engine.Core;

namespace Evolution.Environment.Life.Creatures
{
    public class LegsComponent : IComponent
    {
        public ComponentType Type => ComponentType.COMPONENT_LEGS;

        public Leg LeftSide { get; set; }
        public Leg RightSide { get; set; }
        public bool Initialised { get; set; }
    }
}
