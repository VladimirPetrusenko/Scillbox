using System.Windows;
using System.Data;

namespace DesktopAppStore
{
    /// <summary>
    /// Логика взаимодействия для OrderAddWindow.xaml
    /// </summary>
    public partial class OrderAddWindow : Window
    {
        private OrderAddWindow()
        {
            InitializeComponent();
        }

        public OrderAddWindow(DataRow row) : this()
        {
            cancelBtn.Click += delegate { this.DialogResult = false; };

            okBtn.Click += delegate
            {
                row["Email"] = txtEmail.Text;
                row["Код_товара"] = txtProductCode.Text;
                row["Наименование_товара"] = txtProductName.Text;
                this.DialogResult = !false;
            };
        }
    }
}
