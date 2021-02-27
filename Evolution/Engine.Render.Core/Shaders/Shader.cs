using Engine.Render.Core.Data;
using Engine.Render.Core.Shaders.Enums;
using OpenTK.Graphics.ES30;
using OpenTK.Mathematics;
using System.Collections.Generic;

namespace Engine.Render.Core.Shaders
{
    public class Shader
    {
        private Dictionary<ShaderUniforms, int> _uniforms;

        public int ProgramId { get; protected set; }

        public bool Outline { get; set; }

        public PrimitiveType PrimitiveType { get; set; } = PrimitiveType.Triangles;

        public int SortingLayer { get; set; }

        public Shader(int programId)
        {
            ProgramId = programId;
            _uniforms = new Dictionary<ShaderUniforms, int>();
        }

        public void Bind()
        {
            GL.UseProgram(ProgramId);
        }

        public int GetUniform(ShaderUniforms name) => _uniforms.ContainsKey(name) ? _uniforms[name] : -1;

        public void AddUniform(ShaderUniforms name, int id) => _uniforms.Add(name, id);

        public void SetUniformMat4(ShaderUniforms name, Matrix4 mat)
        {
            int id = GetUniform(name);
            if (id == -1) return;

            GL.UniformMatrix4(id, false, ref mat);
        }

        public void SetUniform(ShaderUniforms name, float value)
        {
            int id = GetUniform(name);
            if (id == -1) return;

            GL.Uniform1(id, value);
        }
    }
}
