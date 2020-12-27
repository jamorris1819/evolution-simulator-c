using Engine.Render;
using OpenTK.Windowing.Desktop;
using System;

namespace Evolution
{
    class Program
    {
        static void Main(string[] args)
        {
            var g = new GameWindowSettings();
            var e = new NativeWindowSettings();

            e.Size = new OpenTK.Mathematics.Vector2i(1920, 1080);

            using (SceneManager sc = new SceneManager(g, e))
            {
                GameScene scene = new GameScene();
                sc.PushScene(scene);

                sc.Run();
            }
        }
    }
}
