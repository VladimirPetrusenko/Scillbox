
namespace AnimalRepository
{
    class UnknownTypeAnimal: Animal
    {
        public UnknownTypeAnimal(int Id, string Name, string Type, int LifeTime, int Speed, string Habitat)
            : base(Id, Name, Type, LifeTime, Speed, Habitat)
        {
        }
        public UnknownTypeAnimal()
            : base()
        {
        }

        public override string GetInformation()
        {
            return "Неизвестный тип животного.";
        }
    }
}
