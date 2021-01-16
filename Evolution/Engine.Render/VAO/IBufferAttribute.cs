namespace Engine.Render.VAO
{
    interface IBufferAttribute
    {
        public string Name { get; set; }

        IVertexBufferObject[] GenerateBufferObjects();
    }
}
