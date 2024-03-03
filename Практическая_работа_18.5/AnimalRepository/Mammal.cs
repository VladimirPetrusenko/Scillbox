
namespace AnimalRepository
{
    //[System.Runtime.Serialization.DataContractAttribute]
    public class Mammal: Animal
    {
        public Mammal(int Id, string Name, string Type, int LifeTime, int Speed, string Habitat) 
            : base (Id, Name, Type, LifeTime, Speed, Habitat)
        {
        }

        public Mammal()
            : base()
        {
        }

        public override string GetInformation()
        {
            return "Млекопита́ющие — класс позвоночных животных, основной отличительной особенностью которых является вскармливание детёнышей молоком.";
        }
    }
}
