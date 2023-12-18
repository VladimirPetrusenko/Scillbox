using System.Windows;

namespace DesktopAppStoreEF
{
    /// <summary>
    /// Логика взаимодействия для DesktopStoreClients.xaml
    /// </summary>
    public partial class DesktopStoreClients : Window
    {
        DBClientsEntities1 context;
        public DesktopStoreClients()
        {
            InitializeComponent();
            context = new DBClientsEntities1();
            context.LoadClients(viewClients);
        }

        //Пункт контекстного меню "Добавить"
        private void MenuItemAddClick(object sender, RoutedEventArgs e)
        {
            context.AddClient();
        }

        //Пункт контекстного меню "Удалить"
        private void MenuItemDeleteClick(object sender, RoutedEventArgs e)
        {
            Clients client = (DesktopAppStoreEF.Clients)(viewClients.SelectedItem);
            context.DeleteClient(client);
        }

        private void MenuItemEditClick(object sender, RoutedEventArgs e)
        {
            Clients client = (DesktopAppStoreEF.Clients)(viewClients.SelectedItem);
            context.EditClient(client);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Clients client = (DesktopAppStoreEF.Clients)(viewClients.SelectedItem);
            context.LoadClientOrders(client);
        }
    }
}
