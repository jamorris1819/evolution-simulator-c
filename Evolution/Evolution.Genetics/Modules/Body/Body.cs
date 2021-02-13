namespace Evolution.Genetics.Creature.Modules.Body
{
    public abstract class Body : IModule
    {
        public ModuleType ModuleType => ModuleType.Body;

        public Genotype Size { get; set; }
        public Genotype ColourR { get; }
        public Genotype ColourG { get; }
        public Genotype ColourB { get; }

        public BodyType Type { get; protected set; }

        public abstract IModule Cross(IModule other);

        public abstract IModule Mutate();
    }
}
