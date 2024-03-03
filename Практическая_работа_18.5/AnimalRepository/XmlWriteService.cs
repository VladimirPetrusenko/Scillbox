using System.Xml.Serialization;
using System.IO;

namespace AnimalRepository
{
    class XmlWriteService: IWriteService
    {
        public void Write(Animal Animal, string Name)
        {
            XmlSerializer xmlSerializer;

            switch (Animal.Type)
            {
                case "Mammal": xmlSerializer = new XmlSerializer(typeof(Mammal)); break;
                case "Bird": xmlSerializer = new XmlSerializer(typeof(Bird)); break;
                case "Amphibian": xmlSerializer = new XmlSerializer(typeof(Amphibian)); break;
                default: return;
            }

            using (FileStream fs = new FileStream($"{Name}.xml", FileMode.Create))
            {
                xmlSerializer.Serialize(fs, Animal);
            }
        }
    }
}
