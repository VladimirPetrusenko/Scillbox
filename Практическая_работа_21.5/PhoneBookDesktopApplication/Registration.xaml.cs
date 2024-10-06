using System.Windows;


namespace PhoneBookDesktopApplication
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        DBDesktopPhoneBookEntities context;

        public Registration()
        {
            InitializeComponent();
            context = new DBDesktopPhoneBookEntities();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string userName = this.userNameBox.Text;
            string password = this.passwordBox.Password;
            string confirmPassword = this.confirmPasswordBox.Password;
            if (password == confirmPassword)
            {
                string result = context.RegisterUser(userName, password);
                MessageBox.Show(result);
            }
            else
            {
                MessageBox.Show("Пароли не совпадают");
            }
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
