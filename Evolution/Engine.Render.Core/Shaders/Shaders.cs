namespace Engine.Render.Core.Shaders
{
    /// <summary>
    /// Holds default shaders used for rendering.
    /// </summary>
    public static class Shaders
    {
        // Standard shaders
        public static StandardShader Standard = new StandardShader("Shaders/Standard/vshaderstandard.glsl", "Shaders/Standard/fshaderstandard.glsl");
        public static StandardShader StandardShadow = new StandardShader("Shaders/Standard/Shadow/vshadershadow.glsl", "Shaders/Standard/Shadow/fshadershadow.glsl");
        public static StandardShader StandardOutline = new StandardShader("Shaders/Standard/Outline/vshaderoutline.glsl", "Shaders/Standard/Outline/fshaderoutline.glsl");

        // Instanced shaders
        public static StandardShader Instanced = new StandardShader("Shaders/Instanced/vshaderinstanced.glsl", "Shaders/Instanced/fshaderinstanced.glsl");
        public static StandardShader InstancedRotated = new StandardShader("Shaders/Instanced/Rotated/vshaderinstancedrotated.glsl", "Shaders/Instanced/Rotated/fshaderinstancedrotated.glsl");

        public static PostShader FBORender = new PostShader("Shaders/vshaderfbo.glsl", "Shaders/fshaderfbo.glsl");


        public static Shader[] All => new Shader[]
        {
            Standard,
            StandardShadow,
            StandardOutline,

            Instanced,
            InstancedRotated,

            FBORender
        };


        public static void Initialise()
        {
            foreach(var shader in All)
            {
                shader.Initialise();
            }
        }
    }
}
