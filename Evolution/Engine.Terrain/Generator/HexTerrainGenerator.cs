﻿using DotnetNoise;
using Engine.Grid;
using Engine.Render.Core.Data.Primitives;
using Engine.Terrain.Biomes;
using Engine.Terrain.Data;
using OpenTK.Mathematics;
using Redbus.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Engine.Terrain.Generator
{
    public class HexTerrainGenerator : TerrainGenerator
    {
        private IList<TerrainUnit> _terrain;
        private FastNoise _noise;


        public HexTerrainGenerator(IEventBus eb) : base(eb)
        {
            _terrain = new List<TerrainUnit>();
            Layout = new Layout(Orientation.Layout_Pointy, new Vector2(4, 4));
            _noise = new FastNoise();

            TerrainShape = Polygon.Generate(Layout.GetHexPoints());
            PointPicker.Initialise();
        }

        public override void Generate()
        {

            var units = Map.GenerateHexagon((int)TerrainProfile.Size.X);
            var positions = units.Select(x => Layout.HexToPixel(x)).ToArray();

            _terrain = new TerrainUnit[positions.Length];

            var map = Combinator.Generate(TerrainProfile, positions);

            map.Temperature = units.Select(CalculateTemperature).ToArray();

            if (TerrainProfile.Island)
            {
                _noise = new FastNoise(TerrainProfile.IslandSeed);
                var heightMask = units.Select(x => CalculateDropOff(x, (int)TerrainProfile.Size.X)).ToArray();

                for (int i = 0; i < map.Heights.Length; i++)
                {
                    map.Heights[i] += heightMask[i];
                }
            }

            for (int i = 0; i < positions.Length; i++)
            {
                var biome = BiomePainter.Determine(map.Heights[i], TerrainProfile.SeaLevel, TerrainProfile.TideLevel, map.Rainfall[i], map.Temperature[i]);

                static int density(Biome biome)
                {
                    switch (biome)
                    {
                        case Biome.RainForest: return 8;
                        case Biome.TemperateGrassland: return 4;
                        case Biome.Savanna: return 14;
                        case Biome.HotDesert: return 1;
                        default: return 0;
                    }
                }

                _terrain[i] = new TerrainUnit()
                {
                    Hex = units.ElementAt(i),
                    Position = positions[i],
                    Height = map.Heights[i],
                    Rainfall = map.Rainfall[i],
                    Temperature = 1f,
                    GrowingPoints = PointPicker.GetPoints(density(biome)).Select(x => x + positions[i]).ToArray(),
                    Biome = biome
                };
            }
        }

        public override IList<TerrainUnit> GetTerrain()
        {
            return _terrain;
        }

        private float CalculateTemperature(Hex hex)
        {
            return 1.0f - Math.Abs((float)hex.R / TerrainProfile.Size.X) - 0.1f * (float)Math.Sin(hex.Q * 0.071f);
            //return 1.0f - ((hex.R + TerrainProfile.Size.X) / (TerrainProfile.Size.X * 2)) - 0.025f * (float)Math.Sin(hex.Q * 0.071f);
        }

        private float CalculateDropOff(Hex hex, int size)
        {
            var len = hex.Length() + _noise.GetPerlin(hex.Q, hex.R, hex.S) * TerrainProfile.IslandEdgeDistortion;

            float dropDistance = TerrainProfile.DropOffPoint;
            if (len < size * dropDistance) return 0;

            float steepness = TerrainProfile.DropOffSteepness;
            float dist = size * (1 - dropDistance);
            float adjustedLen = len - size * dropDistance;


            float dropoff = (adjustedLen / dist);
            return -Math.Max(dropoff, 0) * steepness;
        }
    }
}
