using Engine;
using Engine.Core;
using Engine.Core.Events;
using Engine.Core.Events.Input.Mouse;
using Engine.Render;
using Engine.Render.Data.Primitives;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using Redbus.Interfaces;
using System;

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
            var rc = new RenderComponent(rect);
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
