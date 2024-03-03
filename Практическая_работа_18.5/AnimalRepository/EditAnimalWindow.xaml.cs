
using System.Windows;
using System.Windows.Controls;


namespace AnimalRepository
{
    /// <summary>
    /// Логика взаимодействия для EditAnimalWindow.xaml
    /// </summary>
    public partial class EditAnimalWindow : Window
    {
        string Animal;
        internal Presenter presenter;

        internal EditAnimalWindow(Presenter presenter, string Animal)
        {
            InitializeComponent();

            this.presenter = presenter;
            this.Animal = Animal;

            txtName.Text = Animal.Split('/')[1];
            txtType.Text = Animal.Split('/')[2];
            txtLifeTime.Text = Animal.Split('/')[3];
            txtSpeed.Text = Animal.Split('/')[4];
            txtHabitat.Text = Animal.Split('/')[5];
        }

        private void okBtn_Click(object sender, RoutedEventArgs e)
        {
            Animal = $"{Animal.Split('/')[0]}/{txtName.Text}/{txtType.Text}/{txtLifeTime.Text}/{txtSpeed.Text}/{txtHabitat.Text}";
            presenter.EditData(Animal);
            this.Close();
        }

        private void txtType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
