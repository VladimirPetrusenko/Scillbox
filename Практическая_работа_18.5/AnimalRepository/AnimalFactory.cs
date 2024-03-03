
namespace AnimalRepository
{
    class AnimalFactory
    {
        public IAnimal GetAnimal(int Id, string Name, string Type, int LifeTime, int Speed, string Habitat)
        {
            switch (Type)
            {
                case "Млекопитающее": return new Mammal(Id, Name, Type, LifeTime, Speed, Habitat);
                case "Птица": return new Bird(Id, Name, Type, LifeTime, Speed, Habitat);
                case "Земноводное": return new Amphibian(Id, Name, Type, LifeTime, Speed, Habitat);
                case "Неизвестный тип": return new UnknownTypeAnimal(Id, Name, Type, LifeTime, Speed, Habitat);
                default: return null;
            }
        }
    }
}
