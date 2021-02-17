using Evolution.Genetics.Utilities;
using OpenTK.Mathematics;

namespace Evolution.Genetics.Creature.Readers
{
    public static class DNAReader
    {
        // Readers
        public static readonly GenotypeReader<int> BodyStepsReader = new GenotypeReader<int>(32, 64);
        public static readonly GenotypeReader<float> BodyOffsetsReader = new GenotypeReader<float>(0, 10);
        public static readonly GenotypeReader<int> BodySegmentCountReader = new GenotypeReader<int>(3, 12);

        /// <summary>
        /// Converts the genotype into a boolean value
        /// </summary>
        public static bool ReadValueBool(Genotype genotype) => genotype.GetExpression() > 127;

        /// <summary>
        /// Converts the genotype into an integer value
        /// </summary>
        public static int ReadValueInt(Genotype genotype, int min, int max)
        {
            if (max <= min) throw new GeneticException("Gene cannot be read when min and max are identical.");

            int diff = max - min;
            float delta = (float)(genotype.GetExpression() / 255f);

            return (int)(min + diff * delta);
        }

        public static int ReadValueInt(Genotype genotype, GenotypeReader<int> reader) => ReadValueInt(genotype, reader.Min, reader.Max);

        /// <summary>
        /// Converts the genotype into a float value
        /// </summary>
        public static float ReadValueFloat(Genotype genotype, float min, float max)
        {
            if (max <= min) throw new GeneticException("Gene cannot be read when min and max are identical.");

            float diff = max - min;
            float delta = (float)(genotype.GetExpression() / 255f);

            return min + diff * delta;
        }

        public static float ReadValueFloat(Genotype genotype, GenotypeReader<float> reader) => ReadValueFloat(genotype, reader.Min, reader.Max);

        /// <summary>
        /// Converts the genotypes into a colour
        /// </summary>
        public static Vector3 ReadValueColour(Genotype r, Genotype g, Genotype b)
            => new Vector3(ReadValueFloat(r, 0, 1), ReadValueFloat(g, 0, 1), ReadValueFloat(b, 0, 1));
    }
}
