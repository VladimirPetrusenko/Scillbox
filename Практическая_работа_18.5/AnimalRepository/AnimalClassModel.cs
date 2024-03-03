using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;

namespace AnimalRepository
{
    class AnimalClassModel: IModel
    {
        string path = "Animals.txt";

        AnimalFactory animalFactory = new AnimalFactory();

        private int id;

        int changeIndex;

        List<string> fileLines = new List<string>();

        List<string> fileLinesTemp = new List<string>();

        public ObservableCollection<IAnimal> LoadData(ObservableCollection<IAnimal> AnimalCollection)
        {
            fileLines.Clear();
            StreamReader reader = new StreamReader(path);

            string line;
            line = reader.ReadLine();
            
            if (line != "")
            {
                while (line != null)
                {
                    fileLines.Add(line);
                    string[] lineparths = line.Split('/');

                    id = Convert.ToInt32(lineparths[0]);
                    line = reader.ReadLine();
                }

                foreach (var fileLine in fileLines)
                {
                    AddAnimalToCollection(AnimalCollection, fileLine);
                }
            }

            return AnimalCollection;
        }

        public ObservableCollection<IAnimal> AddData(ObservableCollection<IAnimal> AnimalCollection, string Animal)
        {
            id++;

            AddAnimalToCollection(AnimalCollection, id.ToString() + "/" + Animal);

            fileLines.Add(id.ToString() + '/' + Animal);

            return AnimalCollection;
        }

        public ObservableCollection<IAnimal> EditData(ObservableCollection<IAnimal> AnimalCollection, string Animal)
        {
            /*foreach (var fileLine in fileLines) //коллекция была изменена, невозможно выполнить перечисление
            {
                if (Convert.ToInt32(fileLine.Split('/')[0]) == Convert.ToInt32(Animal.Split('/')[0]))
                {
                    fileLines[fileLines.IndexOf(fileLine)] = Animal;
                }
            }*/
            PrepareToMethod(AnimalCollection);

            foreach (var fileLineTemp in fileLinesTemp)
            {
                if (Convert.ToInt32(fileLineTemp.Split('/')[0]) == Convert.ToInt32(Animal.Split('/')[0]))
                {
                    AddAnimalToCollection(AnimalCollection, Animal);
                    changeIndex = fileLines.IndexOf(fileLineTemp);
                }
                else
                {
                    AddAnimalToCollection(AnimalCollection, fileLineTemp);
                }
            }

            fileLines[changeIndex] = Animal;
            MessageBox.Show(fileLines[changeIndex]);

            return AnimalCollection;
        }

        public ObservableCollection<IAnimal> DeleteData(ObservableCollection<IAnimal> AnimalCollection, string Animal)
        {
            PrepareToMethod(AnimalCollection);

            foreach (var fileLineTemp in fileLinesTemp)
            {
                if (Convert.ToInt32(fileLineTemp.Split('/')[0]) == Convert.ToInt32(Animal.Split('/')[0]))
                {
                    changeIndex = fileLines.IndexOf(fileLineTemp);
                }
                else
                {
                    AddAnimalToCollection(AnimalCollection, fileLineTemp);
                }
            }

            fileLines.Remove(fileLines[changeIndex]);

            return AnimalCollection;
        }

        public void SaveData()
        {
            StreamWriter writer = new StreamWriter(path, false);

            foreach (var fileLines in fileLines)
            {
                writer.WriteLine(fileLines);
            }

            writer.Close();
        }

        public string GetInformation(IAnimal Animal)
        {
            return Animal.GetInformation();
        }

        public void AddAnimalToCollection(ObservableCollection<IAnimal> AnimalCollection, string Animal)
        {
            AnimalCollection.Add(animalFactory.GetAnimal
                (Convert.ToInt32(Animal.Split('/')[0]), Animal.Split('/')[1], Animal.Split('/')[2],
                 Convert.ToInt32(Animal.Split('/')[3]), Convert.ToInt32(Animal.Split('/')[4]), Animal.Split('/')[5]));
        }

        public ObservableCollection<IAnimal> PrepareToMethod(ObservableCollection<IAnimal> AnimalCollection)
        {
            changeIndex = 0;
            fileLinesTemp = fileLines;
            AnimalCollection.Clear();
            return AnimalCollection;
        }
    }
}
