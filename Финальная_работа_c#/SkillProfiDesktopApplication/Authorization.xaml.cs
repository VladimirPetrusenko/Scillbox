using System.Windows;

namespace SkillProfiDesktopApplication
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        DBSkillProfiDesktopApplicationEntities context;
        RequestServiceApi requestServiceApi;
        Requests requestsWindow;

        public Authorization()
        {
            InitializeComponent();
            context = new DBSkillProfiDesktopApplicationEntities();
            requestServiceApi = new RequestServiceApi();
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

            if (result == "Ок")
            {
                requestsWindow = new Requests(requestServiceApi, await requestServiceApi.GetAllRequests());
                requestsWindow.ShowDialog();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

