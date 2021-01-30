using MiscUtil;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Evolution.Genetics.Creature
{
    public readonly struct Phenotype<T> where T: IEquatable<T>
    {
        public T Data { get; }

        public Phenotype(Genotype<T> genotype)
        {
            var strandA = genotype.GeneA;
            var strandB = genotype.GeneB;
            if (strandA.Dominant && !strandB.Dominant) Data = strandA.Data;
            else if (!strandA.Dominant && strandB.Dominant) Data = strandB.Data;
            else
            {
                var data = Operator.Add(strandA.Data, strandB.Data);

                // TODO: there must be a better way!?
                try
                {
                    data = Operator.DivideAlternative(data, 2);
                }
                catch
                {
                    try
                    {
                        data = Operator.MultiplyAlternative(data, 0.5f);
                    }
                    catch
                    {
                        Data = strandA.Data;
                    }
                }

                Data = data;
            }
        }
    }
}
