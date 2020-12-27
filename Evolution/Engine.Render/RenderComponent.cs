﻿using Engine.Core;
using Engine.Render.Data;
using Engine.Render.Shaders.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Render
{
    public class RenderComponent : IComponent
    {
        public VertexArray VertexArray { get; set; }

        internal VertexArrayObject VertexArrayObject { get; set; }

        public ShaderType Shader { get; set; } = ShaderType.Standard;

        public ComponentType Type => ComponentType.COMPONENT_RENDER;


        public RenderComponent(VertexArray va)
        {
            VertexArray = va;
            VertexArrayObject = new VertexArrayObject(va);
        }
    }
}