using Engine.Core;
using Engine.Render.Data;
using Engine.Render.Managers;
using Engine.Render.Shaders;
using OpenTK.Graphics.ES30;
using OpenTK.Mathematics;
using Redbus.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Render
{
    public class RenderSystem : SystemBase, ISystem
    {
        private IEventBus _eventBus;
        private ShaderManager _shaderManager;
        private VAOLoader _vaoLoader;

        public RenderSystem(IEventBus eventBus, ShaderManager shaderManager)
        {
            _eventBus = eventBus;
            _shaderManager = shaderManager;
            _vaoLoader = new VAOLoader();
        }

        public override void OnRender(Entity entity)
        {
            if (!MaskMatch(entity)) return;

            RenderComponent comp = entity.GetComponent<RenderComponent>();
            var def = Matrix4.CreateTranslation(new Vector3(0, 0, 0));

            var shader = _shaderManager.GetShader(comp.Shader);
            shader.Bind();
            shader.SetUniformMat4(Shaders.Enums.ShaderUniforms.Model, def);

            GL.BindVertexArray(comp.VertexArrayObject.VAO[0]);

            if (comp.VertexArrayObject is InstancedVertexArrayObject iVAO)
            {
                GL.DrawElementsInstanced(PrimitiveType.Triangles, iVAO.VertexArray.Indices.Length, DrawElementsType.UnsignedShort, IntPtr.Zero, iVAO.Positions.Length);
            }
            else
            {
                GL.DrawElements(PrimitiveType.Triangles, comp.VertexArray.Indices.Length, DrawElementsType.UnsignedShort, IntPtr.Zero);
            }

            GL.BindVertexArray(0);
        }

        public override void OnUpdate(Entity entity)
        {
            if (!MaskMatch(entity)) return;

            RenderComponent comp = entity.GetComponent<RenderComponent>();
            
            if(!comp.VertexArrayObject.Initialised)
            {
                InitialiseVAO(comp.VertexArrayObject);
            }
            if(comp.VertexArrayObject.Reload)
            {
                _vaoLoader.Reload(comp.VertexArrayObject);
            }
        }

        private void InitialiseVAO(VertexArrayObject vao)
        {
            _vaoLoader.Load(vao);
        }
    }
}
