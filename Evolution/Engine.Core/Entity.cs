using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine.Core
{
    public class Entity
    {
        private IList<IComponent> _components;
        private ComponentType _mask;
        private string _name;

        public string Name { get => _name; }

        public ComponentType Mask { get => _mask; }

        public Entity(string name)
        {
            _name = name;
            _components = new List<IComponent>();
        }

        public void AddComponent(IComponent component)
        {
            if (component == null) throw new ArgumentNullException(nameof(component));

            if ((_mask & component.Type) == component.Type) throw new Exception("Entity already contains component");

            _components.Add(component);
            _mask |= component.Type;
        }


        public T GetComponent<T>() where T : IComponent
            => (T)_components.FirstOrDefault(x => x is T);
    }
}
