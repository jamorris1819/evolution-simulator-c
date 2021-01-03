using DotnetNoise;
using Engine;
using Engine.Render;
using Engine.Terrain;
using Engine.Terrain.Generator;
using Evolution.UI;
using OpenTK.Windowing.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Evolution
{
    public class WorldScene : GameScene
    {
        private TerrainManager _terrainManager;

        Camera cam;
        double counter = 0;

        public WorldScene(Game game) : base(game)
        {
            _terrainManager = new TerrainManager(EntityManager, new HexTerrainGenerator(Game.EventBus), Game.EventBus);
            _terrainManager.Initialise();

            game.UIManager.Windows.Add(new TerrainWindow(_terrainManager, Game.EventBus));

            cam = new MouseCamera(1920, 1080, EventBus, Game.ShaderManager);

            /* EventBus.Subscribe<MouseDownEvent>((e) =>
             {
                 var pos = cam.ScreenToWorld(e.Location);
                 var hex = terrainGenerator.Layout.PixelToHex(pos).Round();
                 Console.WriteLine($"{pos} -> {hex}");
             });

             EventBus.Subscribe<TerrainUpdateEvent>(x =>
             {
                 var set = GenerateTerrain(x.Profile);
                 rc.UpdateInstanceSettings(set);
             });*/
        }

        public override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
        }

        public override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            cam.Update(0.01666);

            counter += e.Time;
            /*rc.UpdateInstanceSettings(new InstanceSettings()
            {
                Instances = terrainGenerator.GetTerrain().Select(x =>


                    new Instance()
                    {
                        Position = x.Position,
                        Colour = new Vector3((float)Math.Abs(Math.Cos(counter)))
                    }
                ).ToArray()
            });*/
        }
    }
}
