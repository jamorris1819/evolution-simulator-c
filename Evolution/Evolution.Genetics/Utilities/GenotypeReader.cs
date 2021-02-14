namespace Evolution.Genetics.Utilities
{
    public readonly struct GenotypeReader<T> where T : struct
    {
        public T Min { get; }
        public T Max { get; }

        public GenotypeReader(T min, T max)
        {
            Min = min;
            Max = max;
        }
    }
}
