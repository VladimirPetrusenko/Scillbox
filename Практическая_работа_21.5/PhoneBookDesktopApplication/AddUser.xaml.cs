using System.Windows;

namespace PhoneBookDesktopApplication
{
    /// <summary>
    /// Логика взаимодействия для AddUser.xaml
    /// </summary>
    public partial class AddUser : Window
    {
        public AddUser()
        {
            InitializeComponent();
        }

        public AddUser(Users user) : this()
        {
            cancelBtn.Click += delegate { this.DialogResult = false; };

            okBtn.Click += delegate
            {
                user.Username = txtUsername.Text;
                user.Password = txtPassword.Text;
                if (txtPassword.Text == txtConfirmPassword.Text)
                {
                    this.DialogResult = !false;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Пароли не совпадают");
                }
            };
        }

        private void okBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
