using System;
using System.Collections.Generic;
using System.Windows;
using System.Runtime.Serialization.Json;
using System.IO;
using ConsultantManagerLibrary;

namespace Petrusenko_Skillbox._11._6
{
    /// <summary>
    /// Логика взаимодействия для SelectFromTheList.xaml
    /// </summary>
    public partial class SelectFromTheListClients : Window
    {
        public bool IsOpenAccountFromListClient = false;

        public void IsOpenAccountFromListClientTrue(object sender, System.ComponentModel.CancelEventArgs e)
        {
            IsOpenAccountFromListClient = true;
        }

        private int count;
        public int Count
        {
            get { return count; }
            set { count = value; }
        }

        private double rate;
        public double Rate
        {
            get { return rate; }
            set { rate = value; }
        }

        private double finalMoney;
        public double FinalMoney
        {
            get { return finalMoney; }
            set { finalMoney = value; }
        }

        public string Record = string.Empty;
        public bool IsRecord = false;
 
        // Объявить строку с текстом про открытие счета с номером таким-то клиенту такому-то, которую буду далее прокидывать в 
        // OpenNewAccount и AccountBankManager

        List<FinalAccount> FinalAccManage = new List<FinalAccount>();
        public SelectFromTheListClients()
        {
            InitializeComponent();

            this.Closing += IsOpenAccountFromListClientTrue;

            DataContractJsonSerializer formatter1 = new DataContractJsonSerializer(typeof(List<FinalAccount>));
            using (FileStream fs = new FileStream("2.json", FileMode.OpenOrCreate))
            {
                FinalAccManage = (List<FinalAccount>)formatter1.ReadObject(fs);
            }
            Count = FinalAccManage.Count;
        }
        
        public void CalculateFinalMoney()
        {
            if (TextBox1.Text != "" && ComboBox1.Text.ToString() != "")
            {
                Rate = Convert.ToDouble(Label5.Content);
                if (TextBox1.Text != "")
                {
                    try
                    {
                        if (Double.Parse(TextBox1.Text) < 0)
                        {
                            throw new NegativeBalanceException();
                        }
                    }
                    catch (NegativeBalanceException e)
                    {
                        e.ErrorMessage();
                        TextBox1.Text = "0";
                    }
                    finally
                    {
                        if (ComboBox1.Text.ToString() == "Да")
                        {
                            FinalMoney = Convert.ToDouble(TextBox1.Text.ToString()) * Rate * 2;
                            Label6.Content = FinalMoney.ToString();
                        }
                        if (ComboBox1.Text.ToString() == "Нет")
                        {
                            FinalMoney = Convert.ToDouble(TextBox1.Text.ToString()) * Rate;
                            Label6.Content = FinalMoney.ToString();
                        }
                    }
                }
            }
        }

        public void OpenNewAccountFromClient()
        {
            string id = "";
            string capital = "";
            string deposit = "";
            Count++;
            id = $"{Count.ToString()}";
            if (ComboBox1.Text.ToString() == "Да")
            {
                capital = "Capital";
            }
            if (ComboBox1.Text.ToString() == "Нет")
            {
                capital = "noCapital";
            }
            if (ComboBox2.Text.ToString() == "Депозитный")
            {
                deposit = "Deposit";
            }
            if (ComboBox2.Text.ToString() == "Недепозитный")
            {
                deposit = "noDeposit";
            }

            FinalAccManage.Add(new FinalAccount(id.ToString(), Label1.Content.ToString(), Label2.Content.ToString(),
                Label3.Content.ToString(), capital.ToString(), TextBox1.Text.ToString(),
                Label5.Content.ToString(), Label6.Content.ToString(), deposit.ToString()));

            Record = $"[Manager {DateTime.Now}] Клиент {Label1.Content.ToString()} {Label2.Content.ToString()} {Label3.Content.ToString()} открыл новый счет c номером {id.ToString()}";
            IsRecord = true;
            // Присвоить строке текст про открытие счета с номером таким-то клиенту такому-то 

            DataContractJsonSerializer formatter1 = new DataContractJsonSerializer(typeof(List<FinalAccount>));
            using (FileStream fs = new FileStream("2.json", FileMode.Create))
            {
                formatter1.WriteObject(fs, FinalAccManage);
            }
            //OpenNewAccount.IsOpenNewAccount = true;
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenNewAccountFromClient();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CalculateFinalMoney();
        }
    }
}
