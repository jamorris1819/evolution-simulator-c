using Engine.UI.Popups.Properties;
using Redbus.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.UI.Popups.Events
{
    public abstract class PopupEvent<T> : EventBase where T: PopupProperties
    {
        public T Properties { get; set; }
    }
}
