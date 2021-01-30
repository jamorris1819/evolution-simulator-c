using Evolution.Genetics.Creature;
using Evolution.Genetics.Creature.Helper;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Evolution.Genetics.Tests
{
    public class DNAHelperTests
    {
        public class CreateGenotypeBalanced
        {
            public class ValidateData
            {
                [Theory]
                [InlineData(10, 2, 8, 12)]
                [InlineData(5, 1, 4, 6)]
                [InlineData(13, 3, 10, 16)]
                [InlineData(0, 10, -10, 10)]
                [InlineData(10, 200, -190, 210)]
                public void ValidateInt(int desiredValue, int offset, int expectedA, int expectedB)
                {
                    // Arrange

                    // Act
                    var genotype = DNAHelper.CreateGenotypeBalanced(desiredValue, offset, true);

                    // Assert
                    Assert.Equal(expectedA, genotype.GeneA.Data);
                    Assert.Equal(expectedB, genotype.GeneB.Data);
                }

                [Theory]
                [InlineData(10, 2, 8, 12)]
                [InlineData(5, 1, 4, 6)]
                [InlineData(13, 3, 10, 16)]
                [InlineData(0, 10, -10, 10)]
                [InlineData(10, 200, -190, 210)]
                public void ValidateFloat(float desiredValue, float offset, float expectedA, float expectedB)
                {
                    // Arrange

                    // Act
                    var genotype = DNAHelper.CreateGenotypeBalanced(desiredValue, offset, true);

                    // Assert
                    Assert.Equal(expectedA, genotype.GeneA.Data);
                    Assert.Equal(expectedB, genotype.GeneB.Data);
                }
            }

            public class CompareWithPhenotype
            {
                [Theory]
                [InlineData(10, 2)]
                [InlineData(5, 1)]
                [InlineData(13, 3)]
                [InlineData(0, 10)]
                [InlineData(10, 200)]
                public void CompareInt(int desiredValue, int offset)
                {
                    // Arrange

                    // Act
                    var genotype = DNAHelper.CreateGenotypeBalanced(desiredValue, offset, true);
                    var phenotype = Phenotype<int>.GetFromGenotype(genotype);

                    // Assert
                    Assert.Equal(desiredValue, phenotype.Data);
                }

                [Theory]
                [InlineData(10, 2)]
                [InlineData(5, 1)]
                [InlineData(13, 3)]
                [InlineData(0, 10)]
                [InlineData(10, 200)]
                public void ValidateFloat(float desiredValue, float offset)
                {
                    // Arrange

                    // Act
                    var genotype = DNAHelper.CreateGenotypeBalanced(desiredValue, offset, true);
                    var phenotype = Phenotype<float>.GetFromGenotype(genotype);

                    // Assert
                    Assert.Equal(desiredValue, phenotype.Data);
                }
            }
        }

        public class Mutations
        {
            public class Genes
            {
                [Theory]
                [InlineData(1)]
                [InlineData(2)]
                [InlineData(3)]
                [InlineData(4)]
                public void MutateGeneInt_NoMinOrMax(int mutationAmount)
                {
                    // Arrange
                    var gene = new Gene<int>(10, true);
                    var metadata = new GenotypeMetadata<int>(MutationChance.Normal, mutationAmount);

                    // Act
                    var newGene = DNAHelper.MutateGene(gene, metadata);

                    // Assert
                    var absDiff = Math.Abs(newGene.Data - gene.Data);
                    Assert.Equal(mutationAmount, absDiff);
                }

                [Fact]
                public void MutateGeneInt_StaysAboveMinimumValue()
                {
                    // Arrange
                    var gene = new Gene<int>(10, true);
                    var metadata = new GenotypeMetadata<int>(MutationChance.Normal, 1, 13, 20);

                    // Act
                    var newGene = DNAHelper.MutateGene(gene, metadata);

                    // Assert
                    Assert.Equal(13, newGene.Data);
                }

                [Fact]
                public void MutateGeneInt_StaysBelowMaximumValue()
                {
                    // Arrange
                    var gene = new Gene<int>(10, true);
                    var metadata = new GenotypeMetadata<int>(MutationChance.Normal, 1, 0, 5);

                    // Act
                    var newGene = DNAHelper.MutateGene(gene, metadata);

                    // Assert
                    Assert.Equal(5, newGene.Data);
                }


                [Theory]
                [InlineData(1f)]
                [InlineData(2)]
                [InlineData(3)]
                [InlineData(4)]
                public void MutateGeneFloat_NoMinOrMax(float mutationAmount)
                {
                    // Arrange
                    var gene = new Gene<float>(10, true);
                    var metadata = new GenotypeMetadata<float>(MutationChance.Normal, mutationAmount);

                    // Act
                    var newGene = DNAHelper.MutateGene(gene, metadata);

                    // Assert
                    var absDiff = Math.Abs(newGene.Data - gene.Data);
                    Assert.Equal(mutationAmount, absDiff);
                }

                [Fact]
                public void MutateGeneFloat_StaysAboveMinimumValue()
                {
                    // Arrange
                    var gene = new Gene<float>(10, true);
                    var metadata = new GenotypeMetadata<float>(MutationChance.Normal, 1, 13, 20);

                    // Act
                    var newGene = DNAHelper.MutateGene(gene, metadata);

                    // Assert
                    Assert.Equal(13, newGene.Data);
                }

                [Fact]
                public void MutateGeneFloat_StaysBelowMaximumValue()
                {
                    // Arrange
                    var gene = new Gene<float>(10, true);
                    var metadata = new GenotypeMetadata<float>(MutationChance.Normal, 1, 0, 5);

                    // Act
                    var newGene = DNAHelper.MutateGene(gene, metadata);

                    // Assert
                    Assert.Equal(5, newGene.Data);
                }
            }
        }
    }
}
