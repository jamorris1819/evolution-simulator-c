﻿using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Terrain
{
    public struct TerrainUnit
    {
        public Vector2 Position { get; set; }
        public float Height { get; set; }
        public float Temperature { get; set; }
    }
}