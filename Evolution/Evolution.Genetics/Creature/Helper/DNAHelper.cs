using MiscUtil;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Evolution.Genetics.Creature.Helper
{
    public partial class DNAHelper
    {
        private static Random _random = new Random();

        public static DNA Cross(DNA a, DNA b)
        {
            var colourR = Cross(a.ColourR, b.ColourR);
            var colourG = Cross(a.ColourG, b.ColourG);
            var colourB = Cross(a.ColourB, b.ColourB);

            return new DNA(colourR, colourG, colourB);
        }

        public static Genotype<T> Cross<T>(in Genotype<T> a, in Genotype<T> b) where T : struct, IEquatable<T>
        {
            var rand = Math.Round(_random.NextDouble());

            Gene<T> geneA;
            Gene<T> geneB;

            if (rand == 0)
            {
                geneA = a.GeneA;
                geneB = b.GeneB;
            }
            else
            {
                geneA = b.GeneA;
                geneB = a.GeneB;
            }

            return new Genotype<T>(geneA, geneB);
        }

        /// <summary>
        /// Creates DNA based on the provided template.
        /// </summary>
        public static DNA CreateDNA(DNATemplate template)
        {
            var colourMetadata = new GenotypeMetadata<float>(MutationChance.Normal, 0.05f, 0, 1);

            
            return new DNA(
                CreateGenotypeBalanced(template.Colour.X, 0.1f, true, colourMetadata),
                CreateGenotypeBalanced(template.Colour.Y, 0.1f, true, colourMetadata),
                CreateGenotypeBalanced(template.Colour.Z, 0.1f, true, colourMetadata)
                );
        }

        /// <summary>
        /// Creates a genotype which is balanced by 2 genes. 
        /// 
        /// The provided value will be present in the phenotype, although it exists as
        /// an average of the 2 genes in the genotype.
        /// </summary>
        /// <param name="data">The desired data for the phenotype</param>
        /// <param name="offset">How far away each gene should be from the phenotype</param>
        /// <param name="dominant">Whether the genes are dominant or recessive</param>
        public static Genotype<T> CreateGenotypeBalanced<T>(T data, T offset, bool dominant) where T : struct, IEquatable<T>
         => CreateGenotypeBalanced(data, offset, dominant, new GenotypeMetadata<T>());

        /// <summary>
        /// Creates a genotype which is balanced by 2 genes. 
        /// 
        /// The provided value will be present in the phenotype, although it exists as
        /// an average of the 2 genes in the genotype.
        /// </summary>
        /// <param name="data">The desired data for the phenotype</param>
        /// <param name="offset">How far away each gene should be from the phenotype</param>
        /// <param name="dominant">Whether the genes are dominant or recessive</param>
        public static Genotype<T> CreateGenotypeBalanced<T>(T data, T offset, bool dominant, GenotypeMetadata<T> metadata) where T : struct, IEquatable<T>
        {
            var min = Operator.Subtract(data, offset);
            var max = Operator.Add(data, offset);

            return new Genotype<T>(new Gene<T>(min, dominant), new Gene<T>(max, dominant), metadata);
        }
    }
}
