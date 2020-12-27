using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Core
{
    public abstract class SystemBase : ISystem
    {
        public ComponentType Mask { get; set; }

        public virtual void OnRender(Entity entity)
        {
        }

        public virtual void OnUpdate(Entity entity)
        {
        }

        protected bool MaskMatch(Entity entity)
        {
            return (Mask & entity.Mask) == Mask;
        }
    }
}
