using Evolution.Genetics.Creature.Helper;
using Evolution.Genetics.Creature.Modules;
using Evolution.Genetics.Creature.Modules.Body;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evolution.Genetics.Creature
{
    public readonly struct DNA
    {
        public IModule[] Modules { get; }

        public DNA(IModule[] modules)
        {
            Modules = modules;
        }

        public IModule GetModule(ModuleType type) => Modules.First(x => x.ModuleType == type);
    }
}
