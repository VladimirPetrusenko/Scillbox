using System.Windows;
using System.Data;

namespace DesktopAppStore
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

        public ClientAddWindow(DataRow row): this()
        {
            cancelBtn.Click += delegate { this.DialogResult = false; };

            okBtn.Click += delegate
            {
                row["Фамилия"] = txtSurname.Text;
                row["Имя"] = txtName.Text;
                row["Отчество"] = txtPatronimyc.Text;
                row["Номер_телефона"] = txtNumberPhone.Text;
                row["Email"] = txtEmail.Text;
                this.DialogResult = !false;
            };
        }
    }
}
