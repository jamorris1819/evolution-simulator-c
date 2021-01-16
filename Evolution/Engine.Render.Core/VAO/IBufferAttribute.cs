namespace Engine.Render.Core.VAO
{
    public interface IBufferAttribute
    {
        public string Name { get; set; }

        IVertexBufferObject[] GenerateBufferObjects();
    }
}
