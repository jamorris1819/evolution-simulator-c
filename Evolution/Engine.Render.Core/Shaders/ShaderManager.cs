using Engine.Render.Core.Shaders.Enums;
using OpenTK.Graphics.ES30;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Engine.Render.Core.Shaders
{
    public class ShaderManager
    {
        private Dictionary<Enums.ShaderType, Shader> _shaders;

        public IList<Shader> All { get => _shaders.Values.ToList(); }

        public ShaderManager()
        {
            _shaders = new Dictionary<Enums.ShaderType, Shader>();
        }

        public void CreateShader(Enums.ShaderType type, string vLocation, string fLocation, PrimitiveType primitiveType = PrimitiveType.Triangles, bool outline = false)
        {
            if (_shaders.ContainsKey(type)) throw new Exception("Duplicate shader");

            var vData = LoadContents(vLocation);
            var fData = LoadContents(fLocation);

            int vShader = CompileShader(OpenTK.Graphics.ES30.ShaderType.VertexShader, vData);
            int fShader = CompileShader(OpenTK.Graphics.ES30.ShaderType.FragmentShader, fData);

            int program = GL.CreateProgram();
            GL.AttachShader(program, vShader);
            GL.AttachShader(program, fShader);
            GL.LinkProgram(program);

            GL.GetProgram(program, GetProgramParameterName.LinkStatus, out int state);

            if(state == 0)
            {
                throw new Exception("Error linking shaders");
            }

            var shader = new Shader(program);
            shader.PrimitiveType = primitiveType;
            shader.Outline = outline;

            shader.AddUniform(ShaderUniforms.View, GL.GetUniformLocation(program, "uView"));
            shader.AddUniform(ShaderUniforms.Projection, GL.GetUniformLocation(program, "uProjection"));
            shader.AddUniform(ShaderUniforms.Model, GL.GetUniformLocation(program, "uModel"));
            shader.AddUniform(ShaderUniforms.Alpha, GL.GetUniformLocation(program, "alpha"));

            _shaders.Add(type, shader);
        }

        public Shader GetShader(Enums.ShaderType type) => _shaders[type];

        private string LoadContents(string location)
            => File.ReadAllText(location);

        private int CompileShader(OpenTK.Graphics.ES30.ShaderType type, string data)
        {
            int shader = GL.CreateShader(type);
            GL.ShaderSource(shader, data);
            GL.CompileShader(shader);
            GL.GetShader(shader, ShaderParameter.CompileStatus, out int state);
            var a = GL.GetShaderInfoLog(shader);
            if(state == 0)
            {
                throw new Exception("Error compiling shader");
            }

            return shader;
        }
    }
}
