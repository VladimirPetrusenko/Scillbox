
namespace AnimalRepository
{
    public class Amphibian: Animal
    {
        public Amphibian(int Id, string Name, string Type, int LifeTime, int Speed, string Habitat)
            : base(Id, Name, Type, LifeTime, Speed, Habitat)
        {
        }
        public Amphibian()
            : base()
        {
        }

        public override string GetInformation()
        {
            return "Земноводные - класс четвероногих позвоночных животных, включающий в себя (в числе прочих) тритонов, саламандр, лягушек и червяг и насчитывающий около 8 700 современных видов.";
        }
    }
}
