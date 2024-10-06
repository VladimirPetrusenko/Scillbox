using System.Windows;


namespace PhoneBookDesktopApplication
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        DBDesktopPhoneBookEntities context;
        PhoneBookApi phoneBookApi;
        PhoneBookWindow PhoneBookWindow;
        string role;

        public Authorization()
        {
            InitializeComponent();
            context = new DBDesktopPhoneBookEntities();
            phoneBookApi = new PhoneBookApi();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            string userName = this.nameBox.Text;
            string password = this.passwordBox.Text;
            string result = context.FindUser(userName, password);

            if (result == "Неверный пароль")
            {
                MessageBox.Show("Неверный пароль");
            }

            if (result == "Пользователь не найден")
            {
                MessageBox.Show("Пользователь не найден");
            }

            if (result.Contains("Ок"))
            {
                MessageBox.Show($"{userName}/{password}/{result.Split('_')[1].ToString()}");
                if (result.Contains("admin"))
                {
                    role = "admin";
                }
                else 
                {
                    role = "user";
                }
                PhoneBookWindow = new PhoneBookWindow(phoneBookApi, await phoneBookApi.GetPhoneBookEntry(), role);
                PhoneBookWindow.HandleChangeEvent += UpdatePhoneBookWindow;
                PhoneBookWindow.ShowDialog();
            }
        }

        public async void UpdatePhoneBookWindow()
        {
            PhoneBookWindow.Close();
            PhoneBookWindow = new PhoneBookWindow(phoneBookApi, await phoneBookApi.GetPhoneBookEntry(), role);
            PhoneBookWindow.HandleChangeEvent += UpdatePhoneBookWindow;
            PhoneBookWindow.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
