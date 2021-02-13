namespace Evolution.Genetics.Creature.Modules.Body
{
    public abstract class Body : IModule
    {
        public ModuleType ModuleType => ModuleType.Body;

        public Genotype<float> Size { get; set; }
        public Genotype<float> ColourR { get; }
        public Genotype<float> ColourG { get; }
        public Genotype<float> ColourB { get; }

        public BodyType Type { get; protected set; }

        public abstract IModule Cross(IModule other);
    }
}
