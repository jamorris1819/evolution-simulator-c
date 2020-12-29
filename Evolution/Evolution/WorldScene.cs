using Engine;
using Engine.Core;
using Engine.Core.Events;
using Engine.Core.Events.Input.Mouse;
using Engine.Render;
using Engine.Render.Data;
using Engine.Render.Data.Primitives;
using Engine.Terrain;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using Redbus.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Evolution
{
    public class WorldScene : GameScene
    {
        Camera cam;
        ITerrainGenerator terrainGenerator;

        public WorldScene(Game game) : base(game)
        {
            terrainGenerator = new HexTerrainGenerator();
            terrainGenerator.Generate(100);
            var shape = terrainGenerator.TerrainShape;

            Entity e = new Entity("Terrain");
            Random random = new Random();

            var units = terrainGenerator.GetTerrain();

            var settings = new InstanceSettings()
            {
                Instances = units.Select(x =>
                
                    new Instance()
                    {
                        Position = x.Position,
                        Colour = new Vector3((float)random.NextDouble(),
                                    (float)random.NextDouble(),
                                    (float)random.NextDouble())
                    }
                ).ToArray()
            };

            var rc = new RenderComponent(shape, settings);
            rc.Shader = Engine.Render.Shaders.Enums.ShaderType.StandardInstanced;
            e.AddComponent(rc);
            EntityManager.AddEntity(e);

            cam = new MouseCamera(1920, 1080, EventBus, Game.ShaderManager);

            EventBus.Subscribe<MouseDragEvent>((e) => Console.WriteLine($"Mouse drag {e.Button} {e.Location} {e.Delta}"));
        }

        public override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            cam.Update(0.01666);
        }
    }
}
