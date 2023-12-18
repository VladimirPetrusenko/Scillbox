using System.Windows;


namespace DesktopAppStoreEF
{
    /// <summary>
    /// Логика взаимодействия для ClientEditWindow.xaml
    /// </summary>
    public partial class ClientEditWindow : Window
    {
        private ClientEditWindow()
        {
            InitializeComponent();
        }

        public ClientEditWindow(Clients newClient) : this()
        {
            txtSurname.Text = newClient.Фамилия;
            txtName.Text = newClient.Имя;
            txtPatronimyc.Text = newClient.Отчество;
            txtNumberPhone.Text = newClient.Номер_телефона;
            txtEmail.Text = newClient.Email;

            cancelBtn.Click += delegate { this.DialogResult = false; };

            okBtn.Click += delegate
            {
                newClient.Фамилия = txtSurname.Text;
                newClient.Имя = txtName.Text;
                newClient.Отчество = txtPatronimyc.Text;
                newClient.Номер_телефона = txtNumberPhone.Text;
                newClient.Email = txtEmail.Text;
                this.DialogResult = !false;
            };
        }
    }
}
