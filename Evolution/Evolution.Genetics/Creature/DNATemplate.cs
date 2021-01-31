using OpenTK.Mathematics;

namespace Evolution.Genetics.Creature
{
    public readonly struct DNATemplate
    {
        public Vector3 Colour { get; }
        public int BodySteps { get; }
        public float BodyOffset { get; }

        public DNATemplate(Vector3 colour, int bodySteps, float bodyOffset)
        {
            Colour = colour;
            BodySteps = bodySteps;
            BodyOffset = bodyOffset;
        }
    }
}
