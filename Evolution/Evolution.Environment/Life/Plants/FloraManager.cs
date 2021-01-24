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
            grasslandPlants.AddPlant(Plants.Spikey, 4);
            grasslandPlants.AddPlant(Plants.Bush, 1);
            _flora.Add(Biome.TemperateGrassland, grasslandPlants);

            var rainforestPlants = new BiomeFlora();
            rainforestPlants.AddPlant(Plants.Spikey, 3);
            rainforestPlants.AddPlant(Plants.Fern, 3);
            rainforestPlants.AddPlant(Plants.Bush, 1);
            _flora.Add(Biome.RainForest, rainforestPlants);

            var desertPlants = new BiomeFlora();
            desertPlants.AddPlant(Plants.Spikey, 10);
            desertPlants.AddPlant(Plants.Fern, 1);
            _flora.Add(Biome.HotDesert, desertPlants);
        }

        public BiomeFlora GetFlora(Biome biome) => _flora[biome];

        public bool HasFlora(Biome biome) => _flora.ContainsKey(biome);
    }
}
