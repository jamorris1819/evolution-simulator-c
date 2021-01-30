using MiscUtil;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Evolution.Genetics.Creature
{
    public readonly struct Phenotype<T> where T: struct, IEquatable<T>
    {
        public T Data { get; }

        private Phenotype(T data)
        {
            Data = data;
        }

        public static Phenotype<T> GetFromGenotype(in Genotype<T> genotype) => new Phenotype<T>(GetValue(genotype));

        public static Phenotype<Vector2> GetFromGenotypes(in Genotype<float> x, in Genotype<float> y)
            => new Phenotype<Vector2>(new Vector2(
                    GetValue(x),
                    GetValue(y)
                ));

        public static Phenotype<Vector3> GetFromGenotypes(in Genotype<float> x, in Genotype<float> y, in Genotype<float> z)
            => new Phenotype<Vector3>(new Vector3(
                    GetValue(x),
                    GetValue(y),
                    GetValue(z)
                ));

        private static T GetValue<T>(in Genotype<T> genotype) where T: struct, IEquatable<T>
        {
            var strandA = genotype.GeneA;
            var strandB = genotype.GeneB;
            if (strandA.Dominant && !strandB.Dominant) return strandA.Data;
            else if (!strandA.Dominant && strandB.Dominant) return strandB.Data;
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
                        return strandA.Data;
                    }
                }

                return data;
            }
        }
    }
}
