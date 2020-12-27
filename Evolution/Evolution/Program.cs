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

            using (SceneManager sc = new SceneManager(g, e))
            {
                GameScene scene = new GameScene();
                sc.PushScene(scene);

                sc.Run();
            }
        }
    }
}
