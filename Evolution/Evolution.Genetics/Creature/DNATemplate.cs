using OpenTK.Mathematics;

namespace Evolution.Genetics.Creature
{
    public readonly struct DNATemplate
    {
        public Vector3 Colour { get; }

        public DNATemplate(Vector3 colour)
        {
            Colour = colour;
        }
    }
}
