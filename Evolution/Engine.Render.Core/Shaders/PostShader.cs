namespace Engine.Render.Core.Shaders
{
    /// <summary>
    /// A post shader takes a rendered texture and performs post processing effects
    /// </summary>
    public sealed class PostShader : Shader
    {
        public PostShader(string vertexLocation, string fragmentLocation) : base(vertexLocation, fragmentLocation)
        {
        }
    }
}
