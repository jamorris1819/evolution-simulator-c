
using Evolution.Genetics.Creature;
using Evolution.Genetics.Creature.Helper;
using Evolution.Genetics.Tests.Helper;
using OpenTK.Mathematics;
using Xunit;

namespace Evolution.Genetics.Tests
{

    public class PhenotypeTests
    {
        public class WhenGenotypeIsInt
        {
            [Fact]
            public void AndBothGenesDominant_AverageIsReturned()
            {
                // Arrange
                var geneA = GenotypeHelper<int>.CreateGene(8, true);
                var geneB = GenotypeHelper<int>.CreateGene(12, true);
                var genotype = GenotypeHelper<int>.CreateGenotype(geneA, geneB);

                var expectedValue = 10;

                // Act
                var phenotype = Phenotype<int>.GetFromGenotype(genotype);

                // Assert
                Assert.Equal(expectedValue, phenotype.Data);
            }

            [Fact]
            public void AndBothGenesRecessive_AverageIsReturned()
            {
                // Arrange
                var geneA = GenotypeHelper<int>.CreateGene(8, false);
                var geneB = GenotypeHelper<int>.CreateGene(12, false);
                var genotype = GenotypeHelper<int>.CreateGenotype(geneA, geneB);

                var expectedValue = 10;

                // Act
                var phenotype = Phenotype<int>.GetFromGenotype(genotype);

                // Assert
                Assert.Equal(expectedValue, phenotype.Data);
            }

            [Fact]
            public void AndOnlyGeneADominant_GeneAValueIsReturned()
            {
                // Arrange
                var geneA = GenotypeHelper<int>.CreateGene(8, true);
                var geneB = GenotypeHelper<int>.CreateGene(12, false);
                var genotype = GenotypeHelper<int>.CreateGenotype(geneA, geneB);

                var expectedValue = 8;

                // Act
                var phenotype = Phenotype<int>.GetFromGenotype(genotype);

                // Assert
                Assert.Equal(expectedValue, phenotype.Data);
            }

            [Fact]
            public void AndOnlyGeneBDominant_GeneBValueIsReturned()
            {
                // Arrange
                var geneA = GenotypeHelper<int>.CreateGene(8, false);
                var geneB = GenotypeHelper<int>.CreateGene(12, true);
                var genotype = GenotypeHelper<int>.CreateGenotype(geneA, geneB);

                var expectedValue = 12;

                // Act
                var phenotype = Phenotype<int>.GetFromGenotype(genotype);

                // Assert
                Assert.Equal(expectedValue, phenotype.Data);
            }
        }

        public class WhenGenotypeIsFloat
        {
            [Fact]
            public void AndBothGenesDominant_AverageIsReturned()
            {
                // Arrange
                var geneA = GenotypeHelper<float>.CreateGene(8, true);
                var geneB = GenotypeHelper<float>.CreateGene(12, true);
                var genotype = GenotypeHelper<float>.CreateGenotype(geneA, geneB);

                var expectedValue = 10;

                // Act
                var phenotype = Phenotype<float>.GetFromGenotype(genotype);

                // Assert
                Assert.Equal(expectedValue, phenotype.Data);
            }

            [Fact]
            public void AndBothGenesRecessive_AverageIsReturned()
            {
                // Arrange
                var geneA = GenotypeHelper<float>.CreateGene(8, false);
                var geneB = GenotypeHelper<float>.CreateGene(12, false);
                var genotype = GenotypeHelper<float>.CreateGenotype(geneA, geneB);

                var expectedValue = 10;

                // Act
                var phenotype = Phenotype<float>.GetFromGenotype(genotype);

                // Assert
                Assert.Equal(expectedValue, phenotype.Data);
            }

            [Fact]
            public void AndOnlyGeneADominant_GeneAValueIsReturned()
            {
                // Arrange
                var geneA = GenotypeHelper<float>.CreateGene(8, true);
                var geneB = GenotypeHelper<float>.CreateGene(12, false);
                var genotype = GenotypeHelper<float>.CreateGenotype(geneA, geneB);

                var expectedValue = 8;

                // Act
                var phenotype = Phenotype<float>.GetFromGenotype(genotype);

                // Assert
                Assert.Equal(expectedValue, phenotype.Data);
            }

            [Fact]
            public void AndOnlyGeneBDominant_GeneBValueIsReturned()
            {
                // Arrange
                var geneA = GenotypeHelper<float>.CreateGene(8, false);
                var geneB = GenotypeHelper<float>.CreateGene(12, true);
                var genotype = GenotypeHelper<float>.CreateGenotype(geneA, geneB);

                var expectedValue = 12;

                // Act
                var phenotype = Phenotype<float>.GetFromGenotype(genotype);

                // Assert
                Assert.Equal(expectedValue, phenotype.Data);
            }
        }

        public class WhenMultipleGenotypes
        {
            [Fact]
            public void TwoGenomes_IntoVector2()
            {
                // Arrange
                var r = DNAHelper.CreateGenotypeBalanced(0.5f, 0.1f, true);
                var g = DNAHelper.CreateGenotypeBalanced(0.5f, 0.1f, true);

                // Act
                var vec = Phenotype<float>.GetFromGenotypes(r, g);

                // Assert
                Assert.Equal(0.5f, vec.Data.X);
                Assert.Equal(0.5f, vec.Data.Y);
            }

            [Fact]
            public void TwoGenomes_IntoVector3()
            {
                // Arrange
                var r = DNAHelper.CreateGenotypeBalanced(0.5f, 0.1f, true);
                var g = DNAHelper.CreateGenotypeBalanced(0.5f, 0.1f, true);
                var b = DNAHelper.CreateGenotypeBalanced(0.5f, 0.1f, true);

                // Act
                var vec = Phenotype<float>.GetFromGenotypes(r, g, b);

                // Assert
                Assert.Equal(0.5f, vec.Data.X);
                Assert.Equal(0.5f, vec.Data.Y);
                Assert.Equal(0.5f, vec.Data.Z);
            }
        }
    }
}