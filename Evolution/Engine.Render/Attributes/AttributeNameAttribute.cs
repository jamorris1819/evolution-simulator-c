using OpenTK.Graphics.ES30;
using System;

namespace Engine.Render.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    internal class AttributeNameAttribute : Attribute
    {
        public string Name { get; private set; }

        public int Size { get; private set; }

        public VertexAttribPointerType Type { get; private set; }

        public AttributeNameAttribute(string name, int size, VertexAttribPointerType type)
        {
            Name = name;
            Size = size;
            Type = type;
        }
    }
}
