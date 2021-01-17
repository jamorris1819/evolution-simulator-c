using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Render.Core.VAO.Instanced
{
    public class PooledInstance
    {
        private Action _remove;

        public Guid Guid { get; }

        public PooledInstance(Action action)
        {
            _remove = action;
            Guid = Guid.NewGuid();
        }

        public void Release() => _remove();
    }
}
