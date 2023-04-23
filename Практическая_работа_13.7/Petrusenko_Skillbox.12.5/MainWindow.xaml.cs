using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Petrusenko_Skillbox._11._6
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Consultant Cons = new Consultant();

        Manager Manag = new Manager();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ListClientsManager mngr = new ListClientsManager();
            mngr.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ListClientsConsultant cnslt = new ListClientsConsultant();
            cnslt.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Cons.ClientBaseCreate();
        }
    }
}
