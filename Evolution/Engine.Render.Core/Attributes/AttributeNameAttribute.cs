using OpenTK.Graphics.OpenGL4;
using System;

namespace Engine.Render.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    internal class AttributeNameAttribute : Attribute
    {
        public string Name { get; private set; }

        public int Size { get; private set; }

        public bool Instanced { get; private set; }

        public int Divisor { get; private set; }

        public VertexAttribPointerType Type { get; private set; }

        public BufferUsageHint BufferUsage { get; private set; } = BufferUsageHint.StaticDraw;

        public AttributeNameAttribute(string name, int size, VertexAttribPointerType type)
            : this(name, size, type, BufferUsageHint.StaticDraw, false, 0) { }

        public AttributeNameAttribute(string name, int size, VertexAttribPointerType type, bool instanced, int divisor)
            : this(name, size, type, BufferUsageHint.StaticDraw, instanced, divisor) { }

        public AttributeNameAttribute(string name, int size, VertexAttribPointerType type, BufferUsageHint hint)
            : this(name, size, type, hint, false, 0) { }

        public AttributeNameAttribute(string name, int size, VertexAttribPointerType type, BufferUsageHint hint, bool instanced, int divisor)
        {
            Name = name;
            Size = size;
            Type = type;
            Instanced = instanced;
            BufferUsage = hint;
            Divisor = divisor;
        }
    }
}
