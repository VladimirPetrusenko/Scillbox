using System;
using System.Windows;


namespace DesktopAppStoreEF
{
    /// <summary>
    /// Логика взаимодействия для OrderEditWindow.xaml
    /// </summary>
    public partial class OrderEditWindow : Window
    {
        public OrderEditWindow()
        {
            InitializeComponent();
        }

        public OrderEditWindow(Orders newOrder) : this()
        {
            txtEmail.Text = newOrder.Email;
            txtProductCode.Text = (newOrder.Код_товара).ToString();
            txtProductName.Text = newOrder.Наименование_товара;

            cancelBtn.Click += delegate { this.DialogResult = false; };

            okBtn.Click += delegate
            {
                newOrder.Email = txtEmail.Text;
                newOrder.Код_товара = Convert.ToInt32(txtProductCode.Text);
                newOrder.Наименование_товара = txtProductName.Text;
                this.DialogResult = !false;
            };
        }
    }
}
