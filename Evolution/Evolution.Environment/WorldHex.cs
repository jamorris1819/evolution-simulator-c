using Engine.Terrain.Data;
using Evolution.Environment.Food;
using Evolution.Environment.Life.Plants.Data;
using Evolution.Genetics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Evolution.Environment
{
    public struct WorldHex
    {
        public TerrainUnit Terrain { get; set; }

        public Dictionary<PlantDNA, List<PlantInstance>> Instances { get; set; }

        public List<FoodSource> FoodSources { get; set; }
    }
}
