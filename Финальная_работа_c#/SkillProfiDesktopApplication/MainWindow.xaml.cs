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
using System.ComponentModel;


namespace SkillProfiDesktopApplication
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //requestServiceApi = new RequestServiceApi();

        }

        /*private async void Start_Button_Click(object sender, RoutedEventArgs e)
        {
            RequestsWindow = new Requests(requestServiceApi, await requestServiceApi.GetAllRequests());
            RequestsWindow.ShowDialog();
        }*/

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Authorization AuthorizationWindow = new Authorization();
            AuthorizationWindow.ShowDialog();
        }
    }
}
