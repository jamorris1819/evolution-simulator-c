using System;
using System.Collections.Generic;
using System.Text;

namespace Evolution.Genetics.Creature
{
    public readonly struct Gene<T> where T: IEquatable<T>
    {
        public T Data { get; }
        public bool Dominant { get; }

        public Gene(T data, bool dominant)
        {
            Data = data;
            Dominant = dominant;
        }
    }
}
