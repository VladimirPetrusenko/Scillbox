using System.Windows;


namespace Petrusenko_Skillbox._11._6
{
    /// <summary>
    /// Логика взаимодействия для EditClientListConsultant.xaml
    /// </summary>
    public partial class EditClientListConsultant : Window
    {
        public EditClientListConsultant()
        {
            InitializeComponent();
        }

        public string Record = string.Empty;
        public bool IsRecord = false;
        int number_phone_change = 0;
        public string textbox;
        public int Number_phone_change
        {
            get { return number_phone_change; }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (TextBox1.Text == "")
            {
                MessageBox.Show("Заполните поле номера телефона");
            }
            else
            {
                textbox = TextBox1.Text;
                number_phone_change = 1;
                IsRecord = true;
                this.Close();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (TextBox1.Text == "")
            {
                MessageBox.Show("Заполните поле номера телефона");
            }
            else
            {
                textbox = TextBox1.Text;
                number_phone_change = 1;
                IsRecord = true;
                this.Close();
            }
        }
    }
}
