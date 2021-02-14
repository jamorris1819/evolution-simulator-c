using Engine.Core.Randomisers;
using Evolution.Genetics.Creature;
using Evolution.Genetics.Creature.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Evolution.Genetics.Utilities
{
    public static class DNAMutator
    {
        public static readonly Randomiser MutationRandomiser = new PlateauRandomiser(0, 0.2);

        public static MutationSeverity GetRandomMutationSeverity()
        {
            var result = MutationRandomiser.Roll(5);

            return (MutationSeverity)result;
        }

        public static Gene Mutate(Gene gene, MutationSeverity severity, Random random)
        {
            var randomNumber = random.NextDouble();
            bool increase = randomNumber > 0.5;

            byte currentData = gene.Data;
            bool currentDominant = gene.Dominant;

            switch(severity)
            {
                case MutationSeverity.None:
                    return gene;
                case MutationSeverity.Minor:
                    currentData += (byte)(increase ? 1 : -1);
                    break;
                case MutationSeverity.Medium:
                    currentData += (byte)(increase ? 2 : -2);
                    break;
                case MutationSeverity.Major:
                    currentData += (byte)(increase ? 5 : -5);
                    currentDominant = !currentDominant;
                    break;
                case MutationSeverity.Extreme:
                    currentData = (byte)(255.0f * (float)randomNumber);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(severity));
            }

            return new Gene(currentData, currentDominant);
        }

        public static Gene Mutate(Gene gene, MutationSeverity severity) => Mutate(gene, severity, new Random());

        public static Genotype Mutate(Genotype genotype, MutationSeverity severity, Random random)
        {
            bool mutateGeneA = random.NextDouble() > 0.5;

            return Mutate(genotype, severity, random, mutateGeneA);
        }

        public static Genotype Mutate(Genotype genotype, MutationSeverity severity, Random random, bool mutateGeneA)
        {
            var geneA = genotype.GeneA;
            var geneB = genotype.GeneB;

            if (mutateGeneA)
            {
                geneA = Mutate(geneA, severity, random);
            }
            else
            {
                geneB = Mutate(geneB, severity, random);
            }

            return new Genotype(geneA, geneB);
        }

        public static Genotype Mutate(Genotype gene, MutationSeverity severity) => Mutate(gene, severity, new Random());
    }
}
