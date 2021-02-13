using Evolution.Genetics.Creature.Modules;
using System.Linq;

namespace Evolution.Genetics
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
