using OpenTK.Graphics.ES30;
using System;

namespace Engine.Render.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    internal class AttributeNameAttribute : Attribute
    {
        public string Name { get; private set; }

        public int Size { get; private set; }

        public bool Instanced { get; private set; }

        public VertexAttribPointerType Type { get; private set; }

        public BufferUsageHint BufferUsage { get; private set; } = BufferUsageHint.StaticDraw;

        public AttributeNameAttribute(string name, int size, VertexAttribPointerType type)
            : this(name, size, type, false, BufferUsageHint.StaticDraw) { }

        public AttributeNameAttribute(string name, int size, VertexAttribPointerType type, bool instanced)
            : this(name, size, type, instanced, BufferUsageHint.StaticDraw) { }

        public AttributeNameAttribute(string name, int size, VertexAttribPointerType type, BufferUsageHint hint)
            : this(name, size, type, false, hint) { }

        public AttributeNameAttribute(string name, int size, VertexAttribPointerType type, bool instanced, BufferUsageHint hint)
        {
            Name = name;
            Size = size;
            Type = type;
            Instanced = instanced;
            BufferUsage = hint;
        }
    }
}
