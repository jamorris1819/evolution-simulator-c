namespace Engine.Render.Core.Shaders
{
    /// <summary>
    /// A shader takes vertex data and converts it into a texture to be rendered to screen.
    /// </summary>
    public sealed class StandardShader : Shader
    {
        public StandardShader(string vertexLocation, string fragmentLocation) : base(vertexLocation, fragmentLocation)
        {
        }
    }
}
