using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Core
{
    [Flags]
    public enum ComponentType
    {
        COMPONENT_NONE = 0,
        COMPONENT_RENDER = 1 << 0,
        COMPONENT_POSITION = 1 << 1,
        COMPONENT_PHYSICS = 1 << 2,
        COMPONENT_LEGS = 1 << 3
    }

    public interface IComponent
    {
        ComponentType Type { get; }
    }
}
