using System;
using System.Windows;


namespace DesktopAppStoreEF
{
    /// <summary>
    /// Логика взаимодействия для OrderAddWindow.xaml
    /// </summary>
    public partial class OrderAddWindow : Window
    {
        public OrderAddWindow()
        {
            InitializeComponent();
        }

        public OrderAddWindow(Orders newOrder) : this()
        {
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
