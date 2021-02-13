using OpenTK.Mathematics;

namespace Evolution.Genetics.Creature.Readers
{
    public static class DNAReader
    {
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

        /// <summary>
        /// Converts the genotypes into a colour
        /// </summary>
        public static Vector3 ReadValueColour(Genotype r, Genotype g, Genotype b)
            => new Vector3(ReadValueFloat(r, 0, 255), ReadValueFloat(g, 0, 255), ReadValueFloat(b, 0, 255));
    }
}
