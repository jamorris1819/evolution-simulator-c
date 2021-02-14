using Evolution.Genetics.Creature.Modules;
using System;
using System.Collections.Generic;
using System.Text;

namespace Evolution.Genetics.Modules
{
    public abstract class Module : IModule
    {
        public ModuleType ModuleType => throw new NotImplementedException();

        public abstract IModule Cross(IModule other);

        public abstract IModule Mutate();
    }
}
