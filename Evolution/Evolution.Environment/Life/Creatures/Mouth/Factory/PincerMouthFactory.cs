using Engine.Core.Managers;
using Evolution.Genetics.Creature;
using System;

namespace Evolution.Environment.Life.Creatures.Mouth.Factory
{
    public class PincerMouthFactory : MouthFactory
    {
        public override Mouth CreateMouth(in DNA dna)
        {
            var mouth = new PincerMouth();
            mouth.Build(dna);
            return mouth;
        }
    }
}
