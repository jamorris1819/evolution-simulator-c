using Engine;
using Engine.Render;
using OpenTK.Windowing.Desktop;
using System;

namespace Evolution
{
    class Program
    {
        static void Main(string[] args)
        {
            using (Game game = new Game())
            {
                WorldScene ws = new WorldScene(game);
                game.AddScene(ws);

                game.Run();
            }
        }
    }
}
