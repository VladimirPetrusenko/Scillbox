using System.Windows;


namespace AnimalRepository
{
    /// <summary>
    /// Логика взаимодействия для RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window, IView
    {
        //private ObservableCollection<IAnimal> AnimalCollection = new ObservableCollection<IAnimal>();

        Presenter presenter;
        public RegisterWindow()
        {
            InitializeComponent();

            presenter = new Presenter(this);

            DataContext = presenter.AnimalCollection;

            viewAnimal.ItemsSource = presenter.LoadData(/*presenter.AnimalCollection*/);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AddAnimalWindow AddAnimalWindow = new AddAnimalWindow(presenter);
            AddAnimalWindow.ShowDialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Animal Animal = (Animal)viewAnimal.SelectedItem;
            string animal = $"{Animal.Id.ToString()}/{Animal.Name.ToString()}/{Animal.Type.ToString()}/{Animal.LifeTime.ToString()}/{Animal.Speed.ToString()}/{Animal.Habitat.ToString()}";
            EditAnimalWindow EditAnimalWindow = new EditAnimalWindow(presenter, animal);
            EditAnimalWindow.ShowDialog();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Animal Animal = (Animal)viewAnimal.SelectedItem;
            string animal = $"{Animal.Id.ToString()}/{Animal.Name.ToString()}/{Animal.Type.ToString()}/{Animal.LifeTime.ToString()}/{Animal.Speed.ToString()}/{Animal.Habitat.ToString()}";
            presenter.DeleteData(animal);
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Animal Animal = (Animal)viewAnimal.SelectedItem;
            presenter.GetInformation(Animal);
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            Animal Animal = (Animal)viewAnimal.SelectedItem;
            string Name = $"{Animal.Name.ToString()}";
            presenter.Write(Animal, "Json", Name);
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            Animal Animal = (Animal)viewAnimal.SelectedItem;
            string Name = $"{Animal.Name.ToString()}";
            presenter.Write(Animal, "Xml", Name);
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            presenter.SaveData();
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
