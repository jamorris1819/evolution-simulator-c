using System.Collections.Generic;
using System.Linq;

namespace Engine.Render.Core.Shaders
{
    /// <summary>
    /// A shader configuration specifies how shaders should be used in order to draw to the 
    /// </summary>
    public class ShaderConfiguration
    {
        /// <summary>
        /// The shader to be used for rendering vertex data.
        /// </summary>
        public StandardShader MainShader { get; set; }

        /// <summary>
        /// Shaders to be used for applying post processing effects.
        /// </summary>
        public List<PostShader> PostShaders { get; set; } = new List<PostShader>();

        /// <summary>
        /// Should post processing effects be run for every entity, or the entire sorting layer?
        /// </summary>
        public bool RunPostEveryEntity { get; set; }

        /// <summary>
        /// The sorting layer where this shader should be run.
        /// 
        /// Note: sorting layers have higher precedence than the rendercomponent's layer.
        /// Shaders with a lower sorting layer will be run first, but the components will be rendered in order.
        /// </summary>
        public int SortingLayer { get; set; }

        /// <summary>
        /// Should this shader write to the stencil buffer?
        /// </summary>
        public bool StencilWrite { get; set; }

        /// <summary>
        /// Should this shader only render if the stencil is clear?
        /// </summary>
        public bool StencilRead { get; set; }

        public ShaderConfiguration(StandardShader main, List<PostShader> post)
        {
            MainShader = main;
            PostShaders = post;
        }

        public ShaderConfiguration(StandardShader main) : this(main, new List<PostShader>()) { }

        public ShaderConfiguration(StandardShader main, params PostShader[] post) : this(main, post.ToList()) { }
    }
}
