using Engine.Core;
using Evolution.Genetics.Creature;

namespace Evolution.Environment.Life.Creatures.Mouth
{
    public interface IMouth
    {
        void Build(in DNA dna);
        void SetParent(Entity entity);
        void Update(float delta);
    }
}
