using Redbus.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Core.Events
{
    public class LogEvent : EventBase
    {
        public string Data { get; private set; }

        public LogEvent(string data)
        {
            Data = data;
        }
    }
}
