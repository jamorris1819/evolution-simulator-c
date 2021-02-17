using Engine.Core.Randomisers;
using Evolution.Genetics;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var randomiser = new PlateauRandomiser(0, 0.44);
            Random random = new Random();
            Plants = Plants.Distinct().ToList();
            int num = randomiser.Roll(Plants.Count);
            return Plants[num];
        }

        /// <summary>
        /// Adds a plant dna to the biome flora profile
        /// </summary>
        /// <param name="count">How many times it should be added</param>
        public void AddPlant(PlantDNA dna)
        {
            Plants.Add(dna);
        }
    }
}
