using Evolution.Genetics.Creature.Enums;
using Evolution.Genetics.Tests.Helper;
using Evolution.Genetics.Utilities;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Evolution.Genetics.Tests
{
    public class DNAMutatorTests
    {
        public class WhenMutateGene
        {
            [Theory]
            [InlineData(100, MutationSeverity.Minor, 1, 0.7)]
            [InlineData(10, MutationSeverity.Minor, 1, 0.27)]
            [InlineData(200, MutationSeverity.Minor, 1, 0.45)]
            [InlineData(100, MutationSeverity.Medium, 2, 0.7)]
            [InlineData(10, MutationSeverity.Medium, 2, 0.27)]
            [InlineData(200, MutationSeverity.Medium, 2, 0.45)]
            [InlineData(100, MutationSeverity.Major, 1, 0.7)]
            [InlineData(10, MutationSeverity.Major, 1, 0.27)]
            [InlineData(200, MutationSeverity.Major, 1, 0.45)]
            public void EnsureGeneticDriftIsCorrect(byte initialValue, MutationSeverity severity, int amountShouldChange, double randomNumber)
            {
                // Arrange
                var gene = GenotypeHelper.CreateGene(initialValue, true);
                var increased = randomNumber > 0.5;

                Mock<Random> random = new Mock<Random>();
                random.SetupSequence(x => x.NextDouble())
                    .Returns(randomNumber);

                // Act
                var newGene = DNAMutator.Mutate(gene, severity, random.Object);
                var difference = (int)newGene.Data - (int)gene.Data;

                // Assert
                Assert.Equal(increased, difference > 0);
                Assert.Equal(amountShouldChange, Math.Abs(difference));
            }

            [Fact]
            public void EnsureDominancyIsFlipped()
            {
                // Arrange
                var gene = GenotypeHelper.CreateGene(100, true);

                Mock<Random> random = new Mock<Random>();
                random.SetupSequence(x => x.NextDouble())
                    .Returns(0);

                // Act
                var newGene = DNAMutator.Mutate(gene, MutationSeverity.Major, random.Object);

                // Assert
                Assert.Equal(!gene.Dominant, newGene.Dominant);
            }

            [Fact]
            public void EnsureRandomisedIsCorrect()
            {
                // Arrange
                var gene = GenotypeHelper.CreateGene(100, true);

                Mock<Random> random = new Mock<Random>();
                random.SetupSequence(x => x.NextDouble())
                    .Returns(0.5);
                var expected = (byte)(0.5f * 255);

                // Act
                var newGene = DNAMutator.Mutate(gene, MutationSeverity.Extreme, random.Object);

                // Assert
                Assert.Equal(expected, newGene.Data);
            }
        }
    }
}
