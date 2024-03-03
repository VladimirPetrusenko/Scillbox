using System.Collections.ObjectModel;
using System.Windows;

namespace AnimalRepository
{
    class Presenter
    {
        IView view;
        IModel model;
        IWriteService writeService;

        public ObservableCollection<IAnimal> AnimalCollection = new ObservableCollection<IAnimal>();

        public Presenter(IView view)
        {
            this.view = view;

            model = new AnimalClassModel();
        }   

        public ObservableCollection<IAnimal> LoadData ()
        {
            AnimalCollection = model.LoadData(AnimalCollection);

            return AnimalCollection;
        }

        public ObservableCollection<IAnimal> AddData(string Animal)
        {
            AnimalCollection = model.AddData(AnimalCollection, Animal);

            return AnimalCollection;
        }

        public ObservableCollection<IAnimal> EditData(string Animal)
        {
            AnimalCollection = model.EditData(AnimalCollection, Animal);

            return AnimalCollection;
        }

        public ObservableCollection<IAnimal> DeleteData(string Animal)
        {
            AnimalCollection = model.DeleteData(AnimalCollection, Animal);

            return AnimalCollection;
        }

        public void SaveData()
        {
            model.SaveData();
        }

        public void Write(Animal Animal, string Format, string Name)
        {
            switch (Format)
            {
                case "Json": writeService = new JsonWriteService(); break;
                case "Xml": writeService = new XmlWriteService(); break;
                default: return;
            }
            writeService.Write(Animal, Name);
        }

        public void GetInformation(Animal Animal)
        {
            MessageBox.Show(Animal.GetInformation());
        }

    }
}
