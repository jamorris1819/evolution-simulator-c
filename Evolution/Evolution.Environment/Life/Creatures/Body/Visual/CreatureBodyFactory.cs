using Engine.Render.Core.Data;
using Evolution.Genetics.Creature;
using System.Collections.Generic;

namespace Evolution.Environment.Life.Creatures.Body.Visual
{
    public abstract class CreatureBodyFactory
    {
        public abstract IEnumerable<VertexArray> CreateBody(in DNA dna);
    }
}
