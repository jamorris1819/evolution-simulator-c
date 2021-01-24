using Evolution.Genetics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Evolution.Environment.Life.Plants
{
    public class BiomeFlora
    { 
        public List<PlantDNA> Plants { get; private set; }

        public BiomeFlora()
        {
            Plants = new List<PlantDNA>();
        }

        public PlantDNA GetRandomPlant()
        {
            Random random = new Random();
            return Plants[random.Next(Plants.Count)];
        }

        /// <summary>
        /// Adds a plant dna to the biome flora profile
        /// </summary>
        /// <param name="count">How many times it should be added</param>
        public void AddPlant(PlantDNA dna, int count)
        {
            for(int i = 0; i < count; i++)
            {
                Plants.Add(dna);
            }
        }
    }
}
