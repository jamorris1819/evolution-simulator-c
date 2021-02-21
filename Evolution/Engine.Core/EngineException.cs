using System;

namespace Engine.Core
{
    public class EngineException : Exception
    {
        public EngineException(string message) : base(message)
        {
        }
    }
}
