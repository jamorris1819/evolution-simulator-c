using System;
using System.Collections.Generic;
using System.Text;

namespace Evolution.Genetics.Creature.Modules
{
    public interface IModule
    {
        ModuleType ModuleType { get; }

        IModule Cross(IModule other);

        IModule Mutate();
    }
}
