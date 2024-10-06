using System.ComponentModel;
using System.Windows;


namespace PhoneBookDesktopApplication
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        PhoneBookApi phoneBookApi;
        PhoneBookWindow PhoneBookWindow;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            MessageBox.Show("Режим просмотра не дост.");
        }

        public MainWindow()
        {
            InitializeComponent();
            phoneBookApi = new PhoneBookApi();
            //ViewMode_Button.IsEnabled = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Авторизация");
            Authorization AuthorizationWindow = new Authorization();
            AuthorizationWindow.ShowDialog();
            
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Регистрация");
            Registration RegistrationWindow = new Registration();
            RegistrationWindow.ShowDialog();
        }

        private async void Button_Click_3(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Режим просмотра");
            PhoneBookWindow = new PhoneBookWindow(phoneBookApi, await phoneBookApi.GetPhoneBookEntry(), "view");
            PhoneBookWindow.ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Режим просмотра не дост.");
        }
    }
}
