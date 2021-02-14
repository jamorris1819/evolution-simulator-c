using OpenTK.Mathematics;

namespace Evolution.Genetics
{
    public readonly struct PlantDNA
    {
        public int Size { get; }

        public int Leaves { get; }

        public LeafData LeafShape { get; }

        public Vector3 Colour { get; }

        public Vector3? Berries { get; }

        public int Layers { get; }

        public float? MinHeight { get; }

        public float? MaxHeight { get; }

        public PlantDNA(int size, int layers, int leaves, LeafData leafShape, Vector3 colour, float? minHeight, float? maxHeight)
        {
            Size = size;
            Layers = layers;
            Leaves = leaves;
            LeafShape = leafShape;
            Colour = colour;
            Berries = null;
            MinHeight = minHeight;
            MaxHeight = maxHeight;
        }

        public PlantDNA(int size, int layers, int leaves, LeafData leafShape, Vector3 colour, float? minHeight, float? maxHeight, Vector3 berries)
        {
            Size = size;
            Layers = layers;
            Leaves = leaves;
            LeafShape = leafShape;
            Colour = colour;
            Berries = berries;
            MinHeight = minHeight;
            MaxHeight = maxHeight;
        }
    }
}
