using Evolution.Environment.Life.Creatures.Mouth.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Evolution.Environment.Life.Creatures.Mouth.Factory
{
    public class MouthFactoryBuilder
    {
        public static MouthFactory GetFactory(MouthType type)
        {
            return type switch
            {
                MouthType.Pincer => new PincerMouthFactory(),
                _ => throw new ArgumentOutOfRangeException(nameof(type)),
            };
        }
    }
}
