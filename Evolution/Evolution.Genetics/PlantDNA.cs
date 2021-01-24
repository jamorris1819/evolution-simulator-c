using OpenTK.Mathematics;

namespace Evolution.Genetics
{
    public readonly struct PlantDNA
    {
        public int Size { get; }

        public int Leaves { get; }

        public LeafData LeafShape { get; }

        public Vector3 Colour { get; }

        public PlantDNA(int size, int leaves, LeafData leafShape, Vector3 colour)
        {
            Size = size;
            Leaves = leaves;
            LeafShape = leafShape;
            Colour = colour;
        }
    }
}
