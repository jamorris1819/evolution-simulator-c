using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Evolution.Environment.Food
{
    public readonly struct FoodInstance
    {
        public Vector2 Position { get; }

        public Vector3 Colour { get; }    

        public FoodInstance(Vector2 position, Vector3 colour)
        {
            Position = position;
            Colour = colour;
        }    
    }
}
