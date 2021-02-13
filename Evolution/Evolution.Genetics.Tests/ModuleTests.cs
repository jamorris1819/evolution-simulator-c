using Evolution.Genetics.Creature.Modules.Body;
using Evolution.Genetics.Tests.Helper;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Evolution.Genetics.Tests
{
    public class ModuleTests
    {
        public class BodyParts
        {
            [Fact]
            public void CrossSingle()
            {
                // Arrange
                var a = new SinglePartBody()
                {
                    BodySteps = GenotypeHelper<int>.CreateGenotype(10),
                    BodyOffset = GenotypeHelper<float>.CreateGenotype(10f),
                    Size = GenotypeHelper<float>.CreateGenotype(5f)
                };

                var b = new SinglePartBody()
                {
                    BodySteps = GenotypeHelper<int>.CreateGenotype(8),
                    BodyOffset = GenotypeHelper<float>.CreateGenotype(5f),
                    Size = GenotypeHelper<float>.CreateGenotype(10f)
                };

                // Act
                var c = a.Cross(b) as SinglePartBody;

                // Assert
                Assert.Equal(9, c.BodySteps.GetPhenotype().Data);
                Assert.Equal(7.5f, c.BodyOffset.GetPhenotype().Data);
                Assert.Equal(7.5f, c.Size.GetPhenotype().Data);
            }
        }
    }
}
