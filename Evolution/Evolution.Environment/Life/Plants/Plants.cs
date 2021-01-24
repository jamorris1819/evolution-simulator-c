using Evolution.Genetics;
using OpenTK.Mathematics;

namespace Evolution.Environment.Life.Plants
{
    public class Plants
    {
        public readonly static PlantDNA Spikey = new PlantDNA(
                10,
                5,
                new LeafData(new Vector2(0.2f, 0), new Vector2(0.2f, 0.5f)),
                new Vector3(60 / 255.0f, 108 / 255.0f, 34 / 255.0f)
                );
    }
}
