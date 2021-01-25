using Engine.Terrain.Biomes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Evolution.Environment.Life.Plants
{
    public class FloraManager
    {
        private Dictionary<Biome, BiomeFlora> _flora;

        public FloraManager()
        {
            _flora = new Dictionary<Biome, BiomeFlora>();
        }

        public void Initialise()
        {
            var grasslandPlants = new BiomeFlora();
            grasslandPlants.AddPlant(Plants.Tree1, 1);
            grasslandPlants.AddPlant(Plants.Tree3, 1);
            grasslandPlants.AddPlant(Plants.Spikey, 100);
            grasslandPlants.AddPlant(Plants.Clover, 200);
            grasslandPlants.AddPlant(Plants.Flat, 10);
            grasslandPlants.AddPlant(Plants.Bush, 6);
            grasslandPlants.AddPlant(Plants.Bush2, 6);
            _flora.Add(Biome.TemperateGrassland, grasslandPlants);

            var rainforestPlants = new BiomeFlora();
            rainforestPlants.AddPlant(Plants.Tree1, 1);
            rainforestPlants.AddPlant(Plants.Tree3, 2);
            rainforestPlants.AddPlant(Plants.Spikey, 10);
            rainforestPlants.AddPlant(Plants.Clover, 40);
            rainforestPlants.AddPlant(Plants.Flat, 20);
            rainforestPlants.AddPlant(Plants.Bush2, 2);
            rainforestPlants.AddPlant(Plants.Bush3, 2);
            _flora.Add(Biome.RainForest, rainforestPlants);

            var desertPlants = new BiomeFlora();
            desertPlants.AddPlant(Plants.Spikey, 10);
            desertPlants.AddPlant(Plants.Clover, 1);
            _flora.Add(Biome.HotDesert, desertPlants);

            var savannaPlants = new BiomeFlora();
            savannaPlants.AddPlant(Plants.SavannaGrass1, 400);
            savannaPlants.AddPlant(Plants.SavannaGrass2, 400);
            savannaPlants.AddPlant(Plants.Tree2, 1);
            savannaPlants.AddPlant(Plants.Bush3, 3);
            _flora.Add(Biome.Savanna, savannaPlants);
        }

        public BiomeFlora GetFlora(Biome biome) => _flora[biome];

        public bool HasFlora(Biome biome) => _flora.ContainsKey(biome);
    }
}
