using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.UI.Popups.Properties
{
    public abstract class PopupProperties
    {
        public string Body { get; set; }
        public Action OnClose { get; set; }
    }
}
