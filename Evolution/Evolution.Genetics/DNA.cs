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

        public DNA Mutate() => new DNA(Modules.Select(x => x.Mutate()).ToArray());

        public DNA Cross(DNA other)
        {
            return new DNA(new IModule[]
            {
                GetModule(ModuleType.Body).Cross(other.GetModule(ModuleType.Body))
            });
        }
    }
}
