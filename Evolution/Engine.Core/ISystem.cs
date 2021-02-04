using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Core
{
    public interface ISystem
    {
        ComponentType Mask { get; }
        void OnUpdate(Entity entity, float deltaTime);
        void OnUpdate(float deltaTime);
        void OnRender(Entity entity);
    }
}
