
using System.Collections.ObjectModel;

namespace AnimalRepository
{
    public interface IModel
    {
        ObservableCollection<IAnimal> LoadData(ObservableCollection<IAnimal> DataCollection);

        ObservableCollection<IAnimal> AddData(ObservableCollection<IAnimal> DataCollection, string Animal);

        ObservableCollection<IAnimal> EditData(ObservableCollection<IAnimal> DataCollection, string Animal);

        ObservableCollection<IAnimal> DeleteData(ObservableCollection<IAnimal> DataCollection, string Animal);

        void SaveData();

        string GetInformation(IAnimal Animal);
    }
}
 