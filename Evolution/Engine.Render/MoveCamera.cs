using Engine.Render.Shaders;
using Redbus.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Render
{
    public class MoveCamera : Camera
    {
        public MoveCamera(int width, int height, IEventBus eventBus, ShaderManager shaderManager)
            : base(width, height, eventBus, shaderManager)
        {
        }

        public override void Update(double deltaTime)
        {
            
            Position += new OpenTK.Mathematics.Vector2(0.1f, 0);
            base.Update(deltaTime);
        }
    }
}
