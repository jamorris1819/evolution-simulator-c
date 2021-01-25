using Engine.Render.Core.Data;
using Engine.Render.Core.Shaders;
using OpenTK.Graphics.ES30;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine.Render.Core.VAO.Instanced
{
    public class PooledInstanceVAO : InstancedVertexArrayObject
    {
        private List<Guid> _toRemove;
        private Dictionary<Guid, int> _pooledLocations;
        private bool[] _pool;
        private int _tracker;

        public PooledInstanceVAO(VertexArray va, int poolSize) : base(va, new Instance[poolSize])
        {
            _toRemove = new List<Guid>();
            _pooledLocations = new Dictionary<Guid, int>();
            _pool = Enumerable.Range(0, poolSize).Select(x => true).ToArray();

        }

        public void Defrag()
        {
            if (_toRemove.Count == 0) return;

            for(int i = 0; i < _toRemove.Count; i++)
            {
                Guid g = _toRemove[i];
                int location = _pooledLocations[g];
                _pool[location] = false;
            }
        }

        public PooledInstance CreateInstance(Instance instance)
        {
            if (_tracker == Instances.Length - 1) throw new Exception("");

            Instances[_tracker] = instance;
            VBO.First(x => x.Name == "Instances").QueueReload();
            _pool[_tracker] = true;
            Guid g = Guid.NewGuid();
            _pooledLocations.Add(g, _tracker);

            var pi = new PooledInstance(() => _toRemove.Add(g));
            _tracker++;

            return pi;
        }

        protected override void AddAttributes()
        {
            Attributes.Add(new BufferAttribute<Instance>("Instances", Instances.ToArray(), BufferUsageHint.DynamicDraw));
        }

        public override void Render(Shader shader)
        {
            Bind();
            GL.DrawElementsInstanced(shader.PrimitiveType, VertexArray.Indices.Length, DrawElementsType.UnsignedShort, IntPtr.Zero, _tracker);
        }
    }
}
