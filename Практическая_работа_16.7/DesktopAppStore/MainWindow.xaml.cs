using System.Windows;


namespace DesktopAppStore
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DesktopStoreClients DesktopStoreClientsWindow = new DesktopStoreClients();
            DesktopStoreClientsWindow.ShowDialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            DesktopStoreOrders DesktopStoreOrdersWindow = new DesktopStoreOrders("full");
            DesktopStoreOrdersWindow.ShowDialog();
        }
    }
}
