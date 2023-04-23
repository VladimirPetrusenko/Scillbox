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
using System.Windows.Shapes;

namespace Petrusenko_Skillbox._11._6
{
    /// <summary>
    /// Логика взаимодействия для EditClientListManager.xaml
    /// </summary>
    public partial class EditClientListManager : Window
    {
        public string surname2 = "";
        public string name2 = "";
        public string patronimyc2 = "";
        public string number_phone2 = "";
        public string passport2 = "";
        public string status2 = "";

        public EditClientListManager()
        {
            InitializeComponent();
            surname2 = TextBox1.ToString();
            name2 = TextBox2.ToString();
            patronimyc2 = TextBox3.ToString();
            number_phone2 = TextBox4.ToString();
            passport2 = TextBox5.ToString();
            status2 = ComboBox1.Text.ToString();
        }

        public bool IsAddNewClient = false;
        public string textbox1 = "";

        int surname_change = 0;
        int name_change = 0;
        int patronimyc_change = 0;
        int number_phone_change = 0;
        int passport_change = 0;
        int status_change = 0;

        public int Surname_change
        {
            get { return surname_change; }
        }
        public int Name_change
        {
            get { return name_change; }
        }
        public int Patronimyc_change
        {
            get { return patronimyc_change; }
        }
        public int Number_phone_change
        {
            get { return number_phone_change; }
        }
        public int Passport_change
        {
            get { return passport_change; }
        }
        public int Status_change
        {
            get { return status_change; }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (TextBox1.Text == "" || TextBox2.Text == "" || TextBox3.Text == "" || TextBox4.Text == "" || TextBox5.Text == "" || ComboBox1.Text == "")
            {
                MessageBox.Show("Заполните пустые поля");
            }
            else
            {
                if (IsAddNewClient == false)
                {
                    if (surname2 != TextBox1.ToString()) { surname_change = 1; }
                    if (name2 != TextBox2.ToString()) { name_change = 1; }
                    if (patronimyc2 != TextBox3.ToString()) { patronimyc_change = 1; }
                    if (number_phone2 != TextBox4.ToString()) { number_phone_change = 1; }
                    if (passport2 != TextBox5.ToString()) { passport_change = 1; }
                    if (status2 != ComboBox1.Text.ToString()) { status_change = 1; }
                }
                this.Close();
            }
        }
    }
}
