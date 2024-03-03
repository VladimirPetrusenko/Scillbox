using System.Windows;


namespace AnimalRepository
{
    /// <summary>
    /// Логика взаимодействия для AddAnimalWindow.xaml
    /// </summary>
    public partial class AddAnimalWindow : Window
    {
        internal Presenter presenter;

        internal AddAnimalWindow(Presenter presenter)
        {
            InitializeComponent();

            this.presenter = presenter;
        }

        private void okBtn_Click(object sender, RoutedEventArgs e)
        {
            string Animal = $"{txtName.Text.ToString()}/{txtType.Text.ToString()}/{txtLifeTime.Text.ToString()}/{txtSpeed.Text.ToString()}/{txtHabitat.Text.ToString()}";
            presenter.AddData(Animal);
            this.Close();
        }
    }
}
