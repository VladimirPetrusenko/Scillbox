using System.Windows;

namespace DesktopAppStoreEF
{
    /// <summary>
    /// Логика взаимодействия для ClientAddWindow.xaml
    /// </summary>
    public partial class ClientAddWindow : Window
    {
        private ClientAddWindow()
        {
            InitializeComponent();
        }

        public ClientAddWindow(Clients newClient) : this()
        {
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
