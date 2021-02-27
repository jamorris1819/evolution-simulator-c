using Engine.Core;
using System.Collections.Generic;

namespace Engine.Render
{
    public class RenderLayer
    {
        public int Layer { get; set; }

        public List<Entity> Entities { get; set; }
    }
}
