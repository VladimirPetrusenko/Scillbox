using System.Windows;

namespace DesktopAppStoreEF
{
    /// <summary>
    /// Логика взаимодействия для DesktopStoreOrders.xaml
    /// </summary>
    public partial class DesktopStoreOrders : Window
    {
        DBOrdersEntities1 context;
        public DesktopStoreOrders(string selectOrders)
        {
            InitializeComponent();
            context = new DBOrdersEntities1();
            context.LoadOrders(selectOrders, viewOrders);
        }

        //Пункт контекстного меню "Добавить"
        private void MenuItemAddClick(object sender, RoutedEventArgs e)
        {
            context.AddOrder();
        }

        //Пункт контекстного меню "Удалить"
        private void MenuItemDeleteClick(object sender, RoutedEventArgs e)
        {
            Orders order = (DesktopAppStoreEF.Orders)(viewOrders.SelectedItem);
            context.DeleteOrder(order);
        }

        //Пункт контекстного меню "Редактировать"
        private void MenuItemEditClick(object sender, RoutedEventArgs e)
        {
            Orders order = (DesktopAppStoreEF.Orders)(viewOrders.SelectedItem);
            context.EditOrder(order);
        }
    }
}
