using Evolution.Genetics.Creature.Readers;
using Evolution.Genetics.Tests.Helper;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Evolution.Genetics.Tests
{
    public class DNAReaderTests
    {
        public class WhenReadingBool
        {
            [Theory]
            [InlineData(200, true)]
            [InlineData(100, false)]
            [InlineData(127, false)]
            [InlineData(128, true)]
            public void ThenCorrectValueIsReturned(byte val, bool expected)
            {
                // Arrange
                var genotype = GenotypeHelper.CreateGenotype(val);

                // Act
                bool readValue = DNAReader.ReadValueBool(genotype);

                // Assert
                Assert.Equal(expected, readValue);
            }
        }

        public class WhenReadingInt
        {
            [Theory]
            [InlineData(0, 0, 10, 0)]
            [InlineData(128, 0, 10, 5)]
            [InlineData(195, 0, 255, 195)]
            [InlineData(255, 0, 255, 255)]
            [InlineData(0, -10, -5, -10)]
            public void AndMaxGreaterThanMin_ThenCorrectValueReturned(byte val, int min, int max, int expected)
            {
                // Arrange
                var genotype = GenotypeHelper.CreateGenotype(val);

                // Act
                var readValue = DNAReader.ReadValueInt(genotype, min, max);

                // Assert
                Assert.Equal(expected, readValue);
            }

            [Theory]
            [InlineData(0, 0)]
            [InlineData(1, 0)]
            public void AndMaxnotGreaterThanMin_ThenExceptionIsThrown(int min, int max)
            {
                // Arrange
                var genotype = GenotypeHelper.CreateGenotype(10);

                // Act
                Action readValue = () => DNAReader.ReadValueInt(genotype, min, max);

                // Assert
                Assert.Throws<GeneticException>(readValue);
            }
        }

        public class WhenReadingFloat
        {
            [Theory]
            [InlineData(0, 0, 10, 0)]
            [InlineData(195, 0, 255, 195)]
            [InlineData(255, 0, 255, 255)]
            [InlineData(0, -10, -5, -10)]
            public void AndMaxGreaterThanMin_ThenCorrectValueReturned(byte val, float min, float max, float expected)
            {
                // Arrange
                var genotype = GenotypeHelper.CreateGenotype(val);

                // Act
                var readValue = DNAReader.ReadValueFloat(genotype, min, max);

                // Assert
                Assert.Equal(expected, readValue);
            }

            [Theory]
            [InlineData(0, 0)]
            [InlineData(1, 0)]
            public void AndMaxnotGreaterThanMin_ThenExceptionIsThrown(float min, float max)
            {
                // Arrange
                var genotype = GenotypeHelper.CreateGenotype(10);

                // Act
                Action readValue = () => DNAReader.ReadValueFloat(genotype, min, max);

                // Assert
                Assert.Throws<GeneticException>(readValue);
            }
        }
    }
}
