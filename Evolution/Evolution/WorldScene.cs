using Engine;
using Engine.Core.Events.Input.Mouse;
using Engine.Render;
using Engine.Render.Core.VAO.Instanced;
using Engine.Render.Events;
using Engine.Terrain;
using Engine.Terrain.Biomes;
using Engine.Terrain.Generator;
using Evolution.Environment;
using Evolution.Environment.Life.Plants;
using Evolution.Genetics;
using Evolution.UI;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using System;
using System.Linq;

namespace Evolution
{
    public class WorldScene : GameScene
    {
        private readonly Environment.Environment _environment;

        Camera cam;
        double counter = 0;

        PooledInstanceVAO a;
        PooledInstance last;

        public WorldScene(Game game) : base(game)
        {
            _environment = new Environment.Environment(EntityManager, EventBus);
            _environment.Initialise();

            game.UIManager.Windows.Add(new TerrainWindow(_environment.TerrainManager, Game.EventBus));

            cam = new MouseCamera(1920, 1080, EventBus, Game.ShaderManager);

            EventBus.Publish(new CameraChangeEvent() { Camera = cam });

           

             EventBus.Subscribe<MouseDownEvent>((e) =>
             {
                 var pos = cam.ScreenToWorld(e.Location);
                 var hex = _environment.TerrainManager.Layout.PixelToHex(pos).Round();
                 Console.WriteLine($"{pos} -> {hex}");
                 Console.WriteLine(e.Location);
             });
        }

        public override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
        }

        public override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            cam.Update(0.01666);
        }
    }
}
