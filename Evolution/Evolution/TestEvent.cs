using Evolution.Environment.Life.Creatures.Mouth.Models;
using Redbus.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Evolution
{
    public class TestEvent : EventBase
    {
        public PincerModel Model { get; set; }
    }
}
