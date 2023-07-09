using System;
using System.Windows;
using System.Windows.Input;
using ConsultantManagerLibrary;


namespace Petrusenko_Skillbox._11._6
{
    /// <summary>
    /// Логика взаимодействия для AddMoneyAccount.xaml
    /// </summary>
    public partial class AddMoneyAccount : Window
    {
        private int changedata = 0;
        public int Changedata
        {
            get { return changedata; }
        }

        private string finalmoney;
        public string FinalMoney
        {
            get { return finalmoney; }
        }

        private string money;
        public string Money
        {
            get { return money; }
        }

        private double fee = 0;
        public string Record = string.Empty;
        public bool IsRecord = false;

        public AddMoneyAccount()
        {
            InitializeComponent();
            this.TextBox1.PreviewTextInput += new TextCompositionEventHandler(TextInputEvent);
        }

        private void TextInputEvent(object sender, TextCompositionEventArgs e)
        {
            if (!(Char.IsDigit(e.Text, 0) || (e.Text == ",")
               && (!TextBox1.Text.Contains(",")
               && TextBox1.Text.Length != 0)))
            {
                e.Handled = true;
            }
        }

        public void AddMoney()
        {
            if (TextBox1.Text.ToString() == "")
            {
                MessageBox.Show("Укажите сумму пополнения");
            }
            else
            {
                switch (Label9.Content.ToString())
                {
                    case "Deposit":
                        IAccountAddMoney<NoDeposit> nodeposit_1 = new AddMoneyDeposit();
                        NoDeposit deposit_1 = nodeposit_1.AccountAddMoney(double.Parse(TextBox1.Text.ToString()));
                        MessageBox.Show($"Сумма пополнения с учетом комиссии: {deposit_1.Money.ToString()}");
                        if (Label5.Content.ToString() == "Capital")
                        {
                            money = (deposit_1.Money + double.Parse(Label6.Content.ToString())).ToString();
                            finalmoney = (double.Parse(money) * double.Parse(Label7.Content.ToString()) * 2).ToString();
                        }
                        if (Label5.Content.ToString() == "noCapital")
                        {
                            money = (deposit_1.Money + double.Parse(Label6.Content.ToString())).ToString();
                            finalmoney = (double.Parse(money) * double.Parse(Label7.Content.ToString())).ToString();
                        }
                        changedata = 1;
                        break;
                    case "noDeposit":
                        IAccountAddMoney<NoDeposit> nodeposit_2 = new AddMoneyNoDeposit();
                        NoDeposit nodeposit_22 = nodeposit_2.AccountAddMoney(double.Parse(TextBox1.Text.ToString()));
                        MessageBox.Show($"Сумма пополнения: {nodeposit_22.Money.ToString()}");
                        if (Label5.Content.ToString() == "Capital")
                        {
                            money = (nodeposit_22.Money + double.Parse(Label6.Content.ToString())).ToString();
                            finalmoney = (double.Parse(money) * double.Parse(Label7.Content.ToString()) * 2).ToString();
                        }
                        if (Label5.Content.ToString() == "noCapital")
                        {
                            money = (nodeposit_22.Money + double.Parse(Label6.Content.ToString())).ToString();
                            finalmoney = (double.Parse(money) * double.Parse(Label7.Content.ToString())).ToString();
                        }
                        changedata = 1;
                        break;
                }
                Record = $"[Manager {DateTime.Now}] Клиент {Label2.Content.ToString()} {Label3.Content.ToString()} {Label4.Content.ToString()} пополнил счет c номером {Label1.Content.ToString()}";
                IsRecord = true;
                this.Close();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //changedata = 1;
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AddMoney();
        }
    }
}
