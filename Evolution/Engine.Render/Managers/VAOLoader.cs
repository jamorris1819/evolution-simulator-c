using Engine.Render.Data;
using OpenTK.Graphics.ES30;
using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Render.Managers
{
    internal class VAOLoader
    {
        public VAOLoader()
        {

        }

        public void Load(VertexArrayObject vao)
        {
            if (vao.Initialised) throw new Exception("VertexArrayObject is already initialised");

            GenerateArrays(vao);
            GL.BindVertexArray(vao.VAO[0]);
            AllocateMemory(vao);

            // maybe this could be stored in the vertex array object temporarily?
            float[] vertexData = GenerateVertexData(vao);
            LoadToMemory(vao, vertexData);
            AssignAttributes(vao);

            vao.Initialised = true;
        }

        private void GenerateArrays(VertexArrayObject vao)
        {
            GL.GenVertexArrays(1, out int vertexArrayObj);
            vao.VAO[0] = vertexArrayObj;

            GL.BindVertexArray(vao.VAO[0]);

            GL.GenBuffers(2, vao.VBO);

            GL.BindVertexArray(0);
        }

        private float[] GenerateVertexData(VertexArrayObject vao)
        {
            int numOfVertices = vao.VertexArray.Vertices.Length;

            int databits = 5; // todo: count vertex data
            float[] vertexData = new float[numOfVertices * databits];

            for(int i = 0; i < numOfVertices; i++)
            {
                int offset = databits * i;

                Vertex v = vao.VertexArray.Vertices[i];

                vertexData[offset] = v.Position.X;
                vertexData[offset + 1] = v.Position.Y;
                vertexData[offset + 2] = v.Colour.X;
                vertexData[offset + 3] = v.Colour.Y;
                vertexData[offset + 4] = v.Colour.Z;
            }

            return vertexData;
        }

        private void AllocateMemory(VertexArrayObject vao)
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, vao.VBO[0]);
            GL.BufferData(BufferTarget.ArrayBuffer, vao.VertexArray.Vertices.Length * 5 * sizeof(float), IntPtr.Zero, BufferUsageHint.StaticDraw);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, vao.VBO[1]);
            GL.BufferData(BufferTarget.ElementArrayBuffer, vao.VertexArray.Indices.Length * sizeof(ushort), IntPtr.Zero, BufferUsageHint.StaticDraw);
        }

        private void LoadToMemory(VertexArrayObject vao, float[] vertexData)
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, vao.VBO[0]);
            GL.BufferSubData(BufferTarget.ArrayBuffer, IntPtr.Zero, vertexData.Length * sizeof(float), vertexData);
            GL.GetBufferParameter(BufferTarget.ArrayBuffer, BufferParameterName.BufferSize, out int size);

            if(vertexData.Length * sizeof(float) != size)
            {
                throw new Exception("Vertex data not loaded correctly");
            }

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, vao.VBO[1]);
            GL.BufferSubData(BufferTarget.ElementArrayBuffer, IntPtr.Zero, vao.VertexArray.Indices.Length * sizeof(ushort), vao.VertexArray.Indices);
            GL.GetBufferParameter(BufferTarget.ElementArrayBuffer, BufferParameterName.BufferSize, out size);

            if (vao.VertexArray.Indices.Length * sizeof(ushort) != size)
            {
                throw new Exception("Index data not loaded correctly");
            }
        }

        private void AssignAttributes(VertexArrayObject vao)
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, vao.VBO[0]);

            GL.EnableVertexAttribArray(0); // vertex position
            GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), IntPtr.Zero); // change 5

            GL.EnableVertexAttribArray(1); // colour position
            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 2 * sizeof(float)); // change 5

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }
    }
}
