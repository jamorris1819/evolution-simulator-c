
using Evolution.Genetics.Creature;
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
                var phenotype = new Phenotype<int>(genotype);

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
                var phenotype = new Phenotype<int>(genotype);

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
                var phenotype = new Phenotype<int>(genotype);

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
                var phenotype = new Phenotype<int>(genotype);

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
                var phenotype = new Phenotype<float>(genotype);

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
                var phenotype = new Phenotype<float>(genotype);

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
                var phenotype = new Phenotype<float>(genotype);

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
                var phenotype = new Phenotype<float>(genotype);

                // Assert
                Assert.Equal(expectedValue, phenotype.Data);
            }
        }
        public class WhenGenotypeIsVector2
        {
            [Fact]
            public void AndBothGenesDominant_AverageIsReturned()
            {
                // Arrange
                var geneA = GenotypeHelper<Vector2>.CreateGene(new Vector2(0, 0), true);
                var geneB = GenotypeHelper<Vector2>.CreateGene(new Vector2(10, 10), true);
                var genotype = GenotypeHelper<Vector2>.CreateGenotype(geneA, geneB);

                var expectedValue = new Vector2(5, 5);

                // Act
                var phenotype = new Phenotype<Vector2>(genotype);

                // Assert
                Assert.Equal(expectedValue, phenotype.Data);
            }

            [Fact]
            public void AndBothGenesRecessive_AverageIsReturned()
            {
                // Arrange
                var geneA = GenotypeHelper<Vector2>.CreateGene(new Vector2(0, 0), false);
                var geneB = GenotypeHelper<Vector2>.CreateGene(new Vector2(10, 10), false);
                var genotype = GenotypeHelper<Vector2>.CreateGenotype(geneA, geneB);

                var expectedValue = new Vector2(5, 5);

                // Act
                var phenotype = new Phenotype<Vector2>(genotype);

                // Assert
                Assert.Equal(expectedValue, phenotype.Data);
            }

            [Fact]
            public void AndOnlyGeneADominant_GeneAValueIsReturned()
            {
                // Arrange
                var geneA = GenotypeHelper<Vector2>.CreateGene(new Vector2(0, 0), true);
                var geneB = GenotypeHelper<Vector2>.CreateGene(new Vector2(10, 10), false);
                var genotype = GenotypeHelper<Vector2>.CreateGenotype(geneA, geneB);

                var expectedValue = new Vector2(0, 0);

                // Act
                var phenotype = new Phenotype<Vector2>(genotype);

                // Assert
                Assert.Equal(expectedValue, phenotype.Data);
            }

            [Fact]
            public void AndOnlyGeneBDominant_GeneBValueIsReturned()
            {
                // Arrange
                var geneA = GenotypeHelper<Vector2>.CreateGene(new Vector2(0, 0), false);
                var geneB = GenotypeHelper<Vector2>.CreateGene(new Vector2(10, 10), true);
                var genotype = GenotypeHelper<Vector2>.CreateGenotype(geneA, geneB);

                var expectedValue = new Vector2(10, 10);

                // Act
                var phenotype = new Phenotype<Vector2>(genotype);

                // Assert
                Assert.Equal(expectedValue, phenotype.Data);
            }
        }
        public class WhenGenotypeIsVector3
        {
            [Fact]
            public void AndBothGenesDominant_AverageIsReturned()
            {
                // Arrange
                var geneA = GenotypeHelper<Vector3>.CreateGene(new Vector3(0, 0, 0), true);
                var geneB = GenotypeHelper<Vector3>.CreateGene(new Vector3(10, 10, 10), true);
                var genotype = GenotypeHelper<Vector3>.CreateGenotype(geneA, geneB);

                var expectedValue = new Vector3(5, 5, 5);

                // Act
                var phenotype = new Phenotype<Vector3>(genotype);

                // Assert
                Assert.Equal(expectedValue, phenotype.Data);
            }

            [Fact]
            public void AndBothGenesRecessive_AverageIsReturned()
            {
                // Arrange
                var geneA = GenotypeHelper<Vector3>.CreateGene(new Vector3(0, 0, 0), false);
                var geneB = GenotypeHelper<Vector3>.CreateGene(new Vector3(10, 10, 10), false);
                var genotype = GenotypeHelper<Vector3>.CreateGenotype(geneA, geneB);

                var expectedValue = new Vector3(5, 5, 5);

                // Act
                var phenotype = new Phenotype<Vector3>(genotype);

                // Assert
                Assert.Equal(expectedValue, phenotype.Data);
            }

            [Fact]
            public void AndOnlyGeneADominant_GeneAValueIsReturned()
            {
                // Arrange
                var geneA = GenotypeHelper<Vector3>.CreateGene(new Vector3(0, 0, 0), true);
                var geneB = GenotypeHelper<Vector3>.CreateGene(new Vector3(10, 10, 10), false);
                var genotype = GenotypeHelper<Vector3>.CreateGenotype(geneA, geneB);

                var expectedValue = new Vector3(0, 0, 0);

                // Act
                var phenotype = new Phenotype<Vector3>(genotype);

                // Assert
                Assert.Equal(expectedValue, phenotype.Data);
            }

            [Fact]
            public void AndOnlyGeneBDominant_GeneBValueIsReturned()
            {
                // Arrange
                var geneA = GenotypeHelper<Vector3>.CreateGene(new Vector3(0, 0, 0), false);
                var geneB = GenotypeHelper<Vector3>.CreateGene(new Vector3(10, 10, 10), true);
                var genotype = GenotypeHelper<Vector3>.CreateGenotype(geneA, geneB);

                var expectedValue = new Vector3(10, 10, 10);

                // Act
                var phenotype = new Phenotype<Vector3>(genotype);

                // Assert
                Assert.Equal(expectedValue, phenotype.Data);
            }
        }
    }
}