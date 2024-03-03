using System.Runtime.Serialization.Json;
using System.IO;

namespace AnimalRepository
{
    class JsonWriteService: IWriteService
    {

        public void Write(Animal Animal, string Name)
        {
            DataContractJsonSerializer formatter1;

            switch (Animal.Type)
            {
                case "Mammal": formatter1 = new DataContractJsonSerializer(typeof(Mammal)); break;
                case "Bird": formatter1 = new DataContractJsonSerializer(typeof(Bird)); break;
                case "Amphibian": formatter1 = new DataContractJsonSerializer(typeof(Amphibian)); break;
                default: return;
            }
            
            using (FileStream fs = new FileStream($"{Name}.json", FileMode.Create))
            {
                formatter1.WriteObject(fs, Animal);
            }
        }
    }
}
