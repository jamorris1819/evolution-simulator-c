namespace Evolution.Genetics.Creature.Modules.Body
{
    public abstract class BodyModule : IModule
    {
        public ModuleType ModuleType => ModuleType.Body;

        public Genotype Size { get; set; }
        public Genotype ColourR { get; set; }
        public Genotype ColourG { get; set; }
        public Genotype ColourB { get; set; }
        public Genotype BodySteps { get; set; }
        public Genotype BodyOffset { get; set; }

        public BodyType Type { get; protected set; }

        public abstract IModule Cross(IModule other);

        public abstract IModule Mutate();
    }
}
