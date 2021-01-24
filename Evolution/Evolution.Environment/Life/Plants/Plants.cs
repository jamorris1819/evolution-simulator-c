using Evolution.Genetics;
using OpenTK.Mathematics;

namespace Evolution.Environment.Life.Plants
{
    public class Plants
    {
        public readonly static PlantDNA Spikey = new PlantDNA(
                10,
                4,
                5,
                new LeafData(new Vector2(0.2f, 0), new Vector2(0.2f, 0.5f)),
                new Vector3(60 / 255.0f, 108 / 255.0f, 34 / 255.0f),
                0.15f, null
                );

        public readonly static PlantDNA Bush = new PlantDNA(
                30,
                2,
                7,
                new LeafData(new Vector2(0.5f), new Vector2(0.5f, 1.0f)),
                new Vector3(60 / 255.0f, 108 / 255.0f, 34 / 255.0f),
                null, null,
                new Vector3(241 / 255.0f, 80 / 255.0f, 37 / 255.0f)
                );

        public readonly static PlantDNA Bush2 = new PlantDNA(
                30,
                2,
                6,
                new LeafData(new Vector2(0.5f), new Vector2(0.5f, 1.0f)),
                new Vector3(60 / 255.0f, 108 / 255.0f, 34 / 255.0f),
                null, null,
                new Vector3(157 / 255.0f, 105 / 255.0f, 163 / 255.0f)
                );

        public readonly static PlantDNA Bush3 = new PlantDNA(
                30,
                3,
                5,
                new LeafData(new Vector2(0.5f), new Vector2(0.5f, 1.0f)),
                new Vector3(60 / 255.0f, 108 / 255.0f, 34 / 255.0f),
                null, null,
                new Vector3(47 / 255.0f, 25 / 255.0f, 95 / 255.0f)
                );

        public readonly static PlantDNA Clover = new PlantDNA(
                       5,
                       1,
                       3,
                       new LeafData(new Vector2(0.3f, 0.5f), new Vector2(0.5f, 1f)),
                       new Vector3(60 / 255.0f, 108 / 255.0f, 34 / 255.0f),
                0.5f, null
                       );

        public readonly static PlantDNA Flat = new PlantDNA(
                       12,
                       3,
                       4,
                       new LeafData(new Vector2(0.5f, 0.5f), new Vector2(0f, 0.5f)),
                       new Vector3(60 / 255.0f, 108 / 255.0f, 34 / 255.0f),
                0.15f, null
                       );

        public readonly static PlantDNA SavannaGrass1 = new PlantDNA(
                       10,
                       1,
                       7,
                       new LeafData(new Vector2(0.2f, 0.0f), new Vector2(0f, 1f)),
                       new Vector3(183 / 255.0f, 182 / 255.0f, 122 / 255.0f),
                0.15f, null
                       );

        public readonly static PlantDNA SavannaGrass2 = new PlantDNA(
                       10,
                       1,
                       7,
                       new LeafData(new Vector2(0.2f, 0.0f), new Vector2(0f, 1f)),
                       new Vector3(131 / 255.0f, 148 / 255.0f, 103 / 255.0f),
                0.15f, null
                       );

        public readonly static PlantDNA Tree1 = new PlantDNA(
                      500,
                      3,
                      7,
                      new LeafData(new Vector2(0.5f, 0.0f), new Vector2(0.35f, 1f)),
                new Vector3(60 / 255.0f, 108 / 255.0f, 34 / 255.0f),
               null, 0.15f
                      );

        public readonly static PlantDNA Tree2 = new PlantDNA(
                      600,
                      2,
                      3,
                      new LeafData(new Vector2(0.5f), new Vector2(0.35f, 0.65f)),
                new Vector3(119 / 255.0f, 192 / 255.0f, 99 / 255.0f),
               null, 0.15f
                      );

        public readonly static PlantDNA Tree3 = new PlantDNA(
                      400,
                      8,
                      1,
                      new LeafData(new Vector2(1f, 1f), new Vector2(0.5f, 1.25f)),
                new Vector3(57 / 255.0f, 122 / 255.0f, 76 / 255.0f),
               null, 0.15f
                      );


    }
}
