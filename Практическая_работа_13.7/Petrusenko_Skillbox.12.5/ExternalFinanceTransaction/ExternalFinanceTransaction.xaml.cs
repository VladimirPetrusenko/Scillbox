using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace Petrusenko_Skillbox._11._6
{
    /// <summary>
    /// Логика взаимодействия для ExternalFinanceTransaction.xaml
    /// </summary>
    public partial class ExternalFinanceTransaction : Window
    {
        private double moneyAccount1;
        public double MoneyAccount1
        {
            get { return moneyAccount1; }
            set { moneyAccount1 = value; }
        }
        private double moneyAccount2;
        public double MoneyAccount2
        {
            get { return moneyAccount2; }
            set { moneyAccount2 = value; }
        }
        private double finalMoneyAccount1;
        public double FinalMoneyAccount1
        {
            get { return finalMoneyAccount1; }
            set { finalMoneyAccount1 = value; }
        }
        private double finalMoneyAccount2;
        public double FinalMoneyAccount2
        {
            get { return finalMoneyAccount2; }
            set { finalMoneyAccount2 = value; }
        }
        private double rateAccount1;
        public double RateAccount1
        {
            get { return rateAccount1; }
            set { rateAccount1 = value; }
        }
        private double rateAccount2;
        public double RateAccount2
        {
            get { return rateAccount2; }
            set { rateAccount2 = value; }
        }
        private double transMoney;
        public double TransMoney
        {
            get { return transMoney; }
            set { transMoney = value; }
        }

        public string Record = string.Empty;
        public bool IsRecord = false;
        public bool IsTransact = false;

        public ExternalFinanceTransaction()
        {
            InitializeComponent();
            this.TextBox.PreviewTextInput += new TextCompositionEventHandler(TextInputEvent);
        }

        // Проверка на ввод некооректных данных
        private void TextInputEvent(object sender, TextCompositionEventArgs e)
        {
            if (!(Char.IsDigit(e.Text, 0) || (e.Text == ",")
               && (!TextBox.Text.Contains(",")
               && TextBox.Text.Length != 0)))
            {
                e.Handled = true;
            }
        }

        // Выбор счета-получателя
        public List<FinalAccount> FinalAccount3 = new List<FinalAccount>();
        public void SelectFinalAccountByTransaction(DataGrid dataGrid)
        {
            if (dataGrid.SelectedCells.Count != 0)
            {
                int index = dataGrid.SelectedIndex;

                if (!(FinalAccount3[index].IDChek.ToString().Contains("_Closed")))
                {
                    if (FinalAccount3[index].IDChek.ToString() != Label1.Content.ToString() &&
                    !((FinalAccount3[index].Surname.ToString() == Label2.Content.ToString()) &&
                    (FinalAccount3[index].Name.ToString() == Label3.Content.ToString()) &&
                    (FinalAccount3[index].Patronymic.ToString() == Label4.Content.ToString())))
                    {
                        Label9.Content = FinalAccount3[index].IDChek.ToString();
                        Label10.Content = FinalAccount3[index].Surname.ToString();
                        Label11.Content = FinalAccount3[index].Name.ToString();
                        Label12.Content = FinalAccount3[index].Patronymic.ToString();
                        Label13.Content = FinalAccount3[index].Typerate.ToString();
                        Label14.Content = FinalAccount3[index].Money.ToString();
                        Label15.Content = FinalAccount3[index].Rate.ToString();
                        Label16.Content = FinalAccount3[index].FinalMoney.ToString();
                        Label18.Content = FinalAccount3[index].Deposit.ToString();
                    }
                    else
                    {
                        MessageBox.Show("Это счет отправителя");
                    }
                }
                else
                {
                    MessageBox.Show("Счет закрыт");
                }
            }
        }

        // Перевод
        public void Transaction(DataGrid dataGrid)
        {
            if (dataGrid.SelectedCells.Count != 0)
            {
                moneyAccount1 = Convert.ToDouble(Label6.Content.ToString());

                rateAccount1 = Convert.ToDouble(Label7.Content.ToString());

                finalMoneyAccount1 = Convert.ToDouble(Label8.Content.ToString());

                moneyAccount2 = Convert.ToDouble(Label14.Content.ToString());

                rateAccount2 = Convert.ToDouble(Label15.Content.ToString());

                finalMoneyAccount2 = Convert.ToDouble(Label16.Content.ToString());

                transMoney = Convert.ToDouble(TextBox.Text.ToString());

                if (transMoney <= moneyAccount1)
                {
                    if (Label17.Content.ToString() == "Deposit" || Label18.Content.ToString() == "Deposit")
                    {
                        Deposit deposit = new Deposit(double.Parse(TextBox.Text.ToString()));
                        ITransaction<Deposit> transaction = new Transaction();
                        transaction.Transact(deposit);

                        moneyAccount1 = moneyAccount1 - transMoney;
                        moneyAccount2 = moneyAccount2 + deposit.Money;
                    }
                    else 
                    {
                        Deposit noDeposit = new Deposit(double.Parse(TextBox.Text.ToString()));
                        ITransaction<NoDeposit> transaction = new Transaction();
                        transaction.Transact(noDeposit);

                        moneyAccount1 = moneyAccount1 - transMoney;
                        moneyAccount2 = moneyAccount2 + noDeposit.Money;
                    }

                    if (Label5.Content.ToString() == "noCapital") { finalMoneyAccount1 = moneyAccount1 * rateAccount1; }
                    else { finalMoneyAccount1 = moneyAccount1 * rateAccount1 * 2; }

                    if (Label13.Content.ToString() == "noCapital") { finalMoneyAccount2 = moneyAccount2 * rateAccount2; }
                    else { finalMoneyAccount2 = moneyAccount2 * rateAccount2 * 2; }

                    for (int i = 0; i < FinalAccount3.Count; i++)
                    {
                        if (FinalAccount3[i].IDChek.ToString() == Label1.Content.ToString())
                        {
                            FinalAccount3[i].Money = moneyAccount1.ToString();
                            MessageBox.Show(FinalAccount3[i].Money.ToString());
                            FinalAccount3[i].FinalMoney = finalMoneyAccount1.ToString();
                        }
                    }

                    for (int i = 0; i < FinalAccount3.Count; i++)
                    {
                        if (FinalAccount3[i].IDChek.ToString() == Label9.Content.ToString())
                        {
                            FinalAccount3[i].Money = moneyAccount2.ToString();
                            FinalAccount3[i].FinalMoney = finalMoneyAccount2.ToString();
                        }
                    }

                    Record = $"[Manager {DateTime.Now}] Клиент {Label2.Content.ToString()} {Label3.Content.ToString()} {Label4.Content.ToString()} осуществил денежный перевод со счета номер {Label1.Content.ToString()}" +
                        $" клиенту {Label10.Content.ToString()} {Label11.Content.ToString()} {Label12.Content.ToString()} на счет номер {Label9.Content.ToString()}";
                    IsRecord = true;
                    IsTransact = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Сумма перевода превышает остаток на счете");
                }

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SelectFinalAccountByTransaction(DG7);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Transaction(DG7);
        }
    }
}
