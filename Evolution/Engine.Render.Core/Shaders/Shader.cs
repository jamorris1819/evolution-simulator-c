using Engine.Render.Core.Data;
using Engine.Render.Core.Shaders.Enums;
using OpenTK.Graphics.ES30;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.IO;

namespace Engine.Render.Core.Shaders
{
    public abstract class Shader
    {
        private readonly string _vertexLocation;
        private readonly string _fragmentLocation;
        private bool _initialised;

        private Dictionary<ShaderUniforms, int> _uniforms;

        public int ProgramId { get; protected set; }

        public bool Outline { get; set; }

        public PrimitiveType PrimitiveType { get; set; } = PrimitiveType.Triangles;

        public int SortingLayer { get; set; }

        public Shader(string vertexLocation, string fragmentLocation)
        {
            _vertexLocation = vertexLocation;
            _fragmentLocation = fragmentLocation;
            _uniforms = new Dictionary<ShaderUniforms, int>();
        }

        /// <summary>
        /// Loads the vertex data then creates & configures the shader in OpenGL.
        /// </summary>
        public void Initialise()
        {
            if (_initialised) return;

            // Get the data
            string vsData = File.ReadAllText(_vertexLocation);
            string fsData = File.ReadAllText(_fragmentLocation);

            // Compile the shaders
            int vShader = CompileShader(OpenTK.Graphics.ES30.ShaderType.VertexShader, vsData);
            int fShader = CompileShader(OpenTK.Graphics.ES30.ShaderType.FragmentShader, fsData);

            // Create the program
            ProgramId = CreateShaderProgram(vShader, fShader);

            // TODO: set uniforms

            _initialised = true;
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

        /// <summary>
        /// Creates the shader and compile from given data
        /// </summary>
        private static int CompileShader(OpenTK.Graphics.ES30.ShaderType type, string data)
        {
            int shader = GL.CreateShader(type);
            GL.ShaderSource(shader, data);
            GL.CompileShader(shader);
            GL.GetShader(shader, ShaderParameter.CompileStatus, out int state);
            if (state == 0)
            {
                var log = GL.GetShaderInfoLog(shader);
                throw new RenderException("Error compiling shader: " + log);
            }

            return shader;
        }

        /// <summary>
        /// Creates the shader program on the GPU
        /// </summary>
        private static int CreateShaderProgram(int vShader, int fShader)
        {
            int program = GL.CreateProgram();
            GL.AttachShader(program, vShader);
            GL.AttachShader(program, fShader);
            GL.LinkProgram(program);

            GL.GetProgram(program, GetProgramParameterName.LinkStatus, out int state);

            if (state == 0)
            {
                throw new RenderException("Error linking shaders");
            }

            return program;
        }
    }
}
