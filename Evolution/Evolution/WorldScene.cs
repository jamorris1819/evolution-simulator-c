using Engine;
using Engine.Core;
using Engine.Core.Events;
using Engine.Core.Events.Input.Mouse;
using Engine.Render;
using Engine.Render.Data;
using Engine.Render.Data.Primitives;
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

        public WorldScene(Game game) : base(game)
        {
            Entity e = new Entity("test");
            var rect = new Rectangle(1, 1);
            rect.SetColour(new Vector3(1, 1, 1));

            List<Vector2> positions = new List<Vector2>();
            for(int x = 0; x < 1000; x++)
            {
                for(int y= 0; y< 1000; y++)
                {
                    positions.Add(new Vector2(x, y));
                }
            }

            var settings = new InstanceSettings()
            {
                Instances = positions.Select(x =>
                
                    new Instance()
                    {
                        Position = x,
                        Colour = new Vector3(x.X / 1000, x.Y / 1000, x.X + x.Y / 2000)
                    }
                ).ToArray()
            };

            var rc = new RenderComponent(rect, settings);
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
