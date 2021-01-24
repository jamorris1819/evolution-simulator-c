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
            var plants = new BiomeFlora();
            plants.AddPlant(Plants.Spikey, 1);
            _flora.Add(Biome.TemperateGrassland, plants);
            _flora.Add(Biome.RainForest, plants);
            _flora.Add(Biome.HotDesert, plants);
        }

        public BiomeFlora GetFlora(Biome biome) => _flora[biome];

        public bool HasFlora(Biome biome) => _flora.ContainsKey(biome);
    }
}
