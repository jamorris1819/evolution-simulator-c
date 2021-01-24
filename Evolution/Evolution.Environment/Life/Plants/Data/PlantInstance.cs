using OpenTK.Mathematics;
using System;

namespace Evolution.Environment.Life.Plants.Data
{
    public struct PlantInstance
    {
        public Guid Id { get; }

        public Vector2 Position { get; set; }

        public Vector3 Colour { get; set; }

        public PlantInstance(Guid id, Vector2 pos, Vector3 colour)
        {
            Id = id;
            Position = pos;
            Colour = colour;
        }
    }
}
