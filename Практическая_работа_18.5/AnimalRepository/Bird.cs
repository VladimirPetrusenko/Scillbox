
namespace AnimalRepository
{
    //[System.Runtime.Serialization.DataContractAttribute]
    public class Bird: Animal
    {
        public Bird(int Id, string Name, string Type, int LifeTime, int Speed, string Habitat)
            : base(Id, Name, Type, LifeTime, Speed, Habitat)
        {
        }

        public Bird()
            : base()
        {
        }

        public override string GetInformation()
        {
            return "Птицы - группа теплокровных яйцекладущих позвоночных животных, традиционно рассматриваемая в ранге отдельного класса.";
        }
    }
}
