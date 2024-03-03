
namespace AnimalRepository
{
    public abstract class Animal: IAnimal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int LifeTime { get; set; }
        public int Speed { get; set; }
        public string Habitat { get; set; }

        public Animal (int Id, string Name, string Type, int LifeTime, int Speed, string Habitat)
        {
            this.Id = Id;

            this.Name = Name;

            this.Type = Type;

            this.LifeTime = LifeTime;

            this.Speed = Speed;

            this.Habitat = Habitat;
        }

        public Animal()
        {
            
        }

        public virtual string GetInformation()
        {
            return "";
        }
    }
}
