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

        public PlantDNA(int size, int layers, int leaves, LeafData leafShape, Vector3 colour)
        {
            Size = size;
            Layers = layers;
            Leaves = leaves;
            LeafShape = leafShape;
            Colour = colour;
            Berries = null;
        }

        public PlantDNA(int size, int layers, int leaves, LeafData leafShape, Vector3 colour, Vector3 berries)
        {
            Size = size;
            Layers = layers;
            Leaves = leaves;
            LeafShape = leafShape;
            Colour = colour;
            Berries = berries;
        }
    }
}
