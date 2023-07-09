using System;
using System.Windows;
using ConsultantManagerLibrary;


namespace Petrusenko_Skillbox._11._6
{
    /// <summary>
    /// Логика взаимодействия для EditAccountBankManager.xaml
    /// </summary>
    public partial class EditAccountBankManager : Window
    {
        private string idchek2 = "";
        private string surname2 = "";
        private string name2 = "";
        private string patronimyc2 = "";
        private string typerate2 = "";
        private string money2 = "";
        private string rate2 = "";
        private string finalmoney2 = "";

        private int changedata = 0;
        public int Changedata
        {
            get { return changedata; }
        }

        public string Idchek2
        {
            get { return idchek2; }
        }
        public string Surname2
        {
            get { return surname2; }
        }
        public string Name2
        {
            get { return name2; }
        }
        public string Patronimyc2
        {
            get { return patronimyc2; }
        }
        public string Typerate2
        {
            get { return typerate2; }
        }
        public string Money2
        {
            get { return money2; }
        }
        public string Rate2
        {
            get { return rate2; }
        }
        public string Finalmoney2
        {
            get { return finalmoney2; }
        }

        public EditAccountBankManager()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            idchek2 = TextBlock1.Text.ToString();
            surname2 = TextBlock2.Text.ToString();
            name2 = TextBlock3.Text.ToString(); ;
            patronimyc2 = TextBlock4.Text.ToString();
            typerate2 = ComboBox1.Text.ToString();
            money2 = TextBlock5.Text.ToString();
            rate2 = TextBlock6.Text.ToString();
            finalmoney2 = TextBlock7.Text.ToString();

            if (typerate2 == "" || rate2 == "")
            { 
                MessageBox.Show("Заполните все поля"); 
            }
            else 
            {
                if (rate2.Contains(","))
                {
                    BankAccount<double, double> BankAccount2 = new BankAccount<double, double>(typerate2, Convert.ToDouble(money2), Convert.ToDouble(rate2));
                    finalmoney2 = BankAccount2.FinalYearMoney(typerate2, Convert.ToDouble(money2), Convert.ToDouble(rate2));
                    TextBlock7.Text = finalmoney2;
                }
                else 
                {
                    BankAccount<double, int> BankAccount2 = new BankAccount<double, int>(typerate2, Convert.ToDouble(money2), Convert.ToInt32(rate2));
                    finalmoney2 = BankAccount2.FinalYearMoney(typerate2, Convert.ToDouble(money2), Convert.ToInt32(rate2));
                    TextBlock7.Text = finalmoney2;
                }
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            changedata = 1;
            this.Close();
        }
    }
}
