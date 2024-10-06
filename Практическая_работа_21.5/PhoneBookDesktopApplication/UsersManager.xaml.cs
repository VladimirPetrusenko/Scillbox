using System.Windows;


namespace PhoneBookDesktopApplication
{
    /// <summary>
    /// Логика взаимодействия для UsersManager.xaml
    /// </summary>
    public partial class UsersManager : Window
    {
        public DBDesktopPhoneBookEntities userContext;

        public UsersManager()
        {
            InitializeComponent();
            userContext = new DBDesktopPhoneBookEntities();
            viewUsers.ItemsSource = userContext.GetUsers();
        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            Users newUser = new Users();
            AddUser AddUserWindow = new AddUser(newUser);
            AddUserWindow.ShowDialog();

            if (AddUserWindow.DialogResult.Value)
            {
                userContext.RegisterUser(newUser.Username, newUser.Password);
            }
            this.Close();
        }

        private void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            Users newUser = (Users)(viewUsers.SelectedItem);
            userContext.DeleteUser(newUser);
            this.Close();
        }
    }
}

