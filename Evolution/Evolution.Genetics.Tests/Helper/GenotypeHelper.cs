﻿using Evolution.Genetics.Creature;
using System;
using System.Collections.Generic;
using System.Text;

namespace Evolution.Genetics.Tests.Helper
{
    internal class GenotypeHelper<T> where T: IEquatable<T>
    {
        public static Gene<T> CreateGene(T data, bool dominant) => new Gene<T>(data, dominant);

        public static Genotype<T> CreateGenotype(Gene<T> a, Gene<T> b) => new Genotype<T>(a, b);
    }
}