using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Runtime.Serialization.Json;
using System.IO;
using System;
using System.Text;

namespace Petrusenko_Skillbox._11._6
{
    /// <summary>
    /// Логика взаимодействия для AccountBankManager.xaml
    /// </summary>
    public partial class AccountBankManager : Window
    {
        string[] id1 = new string[50];
        string[] surname1 = new string[50];
        string[] name1 = new string[50];
        string[] patronymic1 = new string[50];
        string[] typerate1 = new string[50];
        string[] money1 = new string[50];
        string[] rate1 = new string[50];
        string[] finalmoney1 = new string[50];
        string[] deposit1 = new string[50];

        private int count;

        public int Count
        {
            get { return count; }
            set { count = value; }
        }

        public string Record = string.Empty;

        public static event Action<string> RecordChanged;

        public void Handler(string s) 
        {
            using (StreamWriter sw = new StreamWriter("LogRecords.txt", true, Encoding.Unicode))
            {
                sw.WriteLine(s);
            }
            Record = s; 
            MessageBox.Show(Record); 
        }

        public AccountBankManager()
        {
            InitializeComponent();
            List<FinalAccount> FinalAccManage = new List<FinalAccount>();
            DataContractJsonSerializer formatter1 = new DataContractJsonSerializer(typeof(List<FinalAccount>));
            using (FileStream fs = new FileStream("2.json", FileMode.OpenOrCreate))
            {
                FinalAccManage = (List<FinalAccount>)formatter1.ReadObject(fs);
            }
            DG4.ItemsSource = FinalAccManage;
            Count = FinalAccManage.Count;
            for (int i = 0; i < FinalAccManage.Count; i++)
            {
                id1[i] = FinalAccManage[i].IDChek;
                surname1[i] = FinalAccManage[i].Surname;
                name1[i] = FinalAccManage[i].Name;
                patronymic1[i] = FinalAccManage[i].Patronymic;
                typerate1[i] = FinalAccManage[i].Typerate;
                money1[i] = FinalAccManage[i].Money;
                rate1[i] = FinalAccManage[i].Rate;
                finalmoney1[i] = FinalAccManage[i].FinalMoney;
                deposit1[i] = FinalAccManage[i].Deposit;
            }
        }

        // Редактирование информации о счете
        public void EditAccountsList(DataGrid dataGrid)
        {
            if (dataGrid.SelectedCells.Count != 0)
            {
                int index = dataGrid.SelectedIndex;
                if (!(id1[index].Contains("_Closed")))
                {
                    EditAccountBankManager EditAccountBankManagerWindow = new EditAccountBankManager();
                    EditAccountBankManagerWindow.TextBlock1.Text = id1[index];
                    EditAccountBankManagerWindow.TextBlock2.Text = surname1[index].ToString();
                    EditAccountBankManagerWindow.TextBlock3.Text = name1[index].ToString();
                    EditAccountBankManagerWindow.TextBlock4.Text = patronymic1[index].ToString();
                    EditAccountBankManagerWindow.ComboBox1.Text = typerate1[index].ToString();
                    EditAccountBankManagerWindow.TextBlock5.Text = money1[index].ToString();
                    EditAccountBankManagerWindow.TextBlock6.Text = rate1[index].ToString();
                    EditAccountBankManagerWindow.TextBlock7.Text = finalmoney1[index].ToString();

                    EditAccountBankManagerWindow.ShowDialog();

                    if (EditAccountBankManagerWindow.Changedata == 1)
                    {
                        id1[index] = EditAccountBankManagerWindow.TextBlock1.Text.ToString();
                        surname1[index] = EditAccountBankManagerWindow.TextBlock2.Text.ToString();
                        name1[index] = EditAccountBankManagerWindow.TextBlock3.Text.ToString();
                        patronymic1[index] = EditAccountBankManagerWindow.TextBlock4.Text.ToString();
                        typerate1[index] = EditAccountBankManagerWindow.ComboBox1.Text.ToString();
                        money1[index] = EditAccountBankManagerWindow.TextBlock5.Text.ToString();
                        rate1[index] = EditAccountBankManagerWindow.TextBlock6.Text.ToString();
                        finalmoney1[index] = EditAccountBankManagerWindow.TextBlock7.Text.ToString();
                        deposit1[index] = EditAccountBankManagerWindow.ComboBox2.Text.ToString();

                        string text1, text2, text3, text4, text5, text6, text7, text8, text9;
                        List<FinalAccount> FinalAccount1 = new List<FinalAccount>();
                        DataContractJsonSerializer formatter1 = new DataContractJsonSerializer(typeof(List<FinalAccount>));

                        for (int i = 0; i < Count; i++)
                        {
                            text1 = id1[i].ToString();
                            text2 = surname1[i].ToString();
                            text3 = name1[i].ToString();
                            text4 = patronymic1[i].ToString();
                            text5 = typerate1[i].ToString();
                            text6 = money1[i].ToString();
                            text7 = rate1[i].ToString();
                            text8 = finalmoney1[i].ToString();
                            text9 = deposit1[i].ToString();

                            FinalAccount FinalAccount2 = new FinalAccount(text1, text2, text3, text4, text5, text6, text7, text8, text9);
                            FinalAccount1.Add(FinalAccount2);
                        }
                        using (FileStream fs = new FileStream("2.json", FileMode.Create))
                        {
                            formatter1.WriteObject(fs, FinalAccount1);
                        }
                        using (FileStream fs = new FileStream("2.json", FileMode.OpenOrCreate))
                        {
                            FinalAccount1 = (List<FinalAccount>)formatter1.ReadObject(fs);
                        }
                        DG4.ItemsSource = FinalAccount1;
                    }
                    
                }
                else 
                {
                    MessageBox.Show("Счет закрыт");
                }
            }
            else
            {
                MessageBox.Show("Выберите клиента");
            }
        }

        // Транзакции между счетами одного клиента
        public void InnerFinanceTransaction(DataGrid dataGrid)
        {
            RecordChanged += Handler;
            if (dataGrid.SelectedCells.Count != 0)
            {
                int index = dataGrid.SelectedIndex;
                if (!(id1[index].Contains("_Closed")))
                {
                    InnerFinanceTransaction InnerFinanceTransactionWindow = new InnerFinanceTransaction();
                    InnerFinanceTransactionWindow.Label1.Content = id1[index];
                    InnerFinanceTransactionWindow.Label2.Content = surname1[index].ToString();
                    InnerFinanceTransactionWindow.Label3.Content = name1[index].ToString();
                    InnerFinanceTransactionWindow.Label4.Content = patronymic1[index].ToString();
                    InnerFinanceTransactionWindow.Label5.Content = typerate1[index].ToString();
                    InnerFinanceTransactionWindow.Label6.Content = money1[index].ToString();
                    InnerFinanceTransactionWindow.Label7.Content = rate1[index].ToString();
                    InnerFinanceTransactionWindow.Label8.Content = finalmoney1[index].ToString();
                    InnerFinanceTransactionWindow.Label17.Content = deposit1[index].ToString();

                    FinalAccount FinalAccount = new FinalAccount(id1[index].ToString(), surname1[index].ToString(),
                                name1[index].ToString(), patronymic1[index].ToString(), typerate1[index].ToString(),
                                money1[index].ToString(), rate1[index].ToString(), finalmoney1[index].ToString(), deposit1[index].ToString());
                    InnerFinanceTransactionWindow.FinalAccount3.Add(FinalAccount);

                    for (int i = 0; i < Count; i++)
                    {
                        if (surname1[index].ToString() == surname1[i].ToString()
                            && name1[index].ToString() == name1[i].ToString() && patronymic1[index].ToString() == patronymic1[i].ToString()
                            && id1[index] != id1[i])
                        {
                            FinalAccount FinalAccount2 = new FinalAccount(id1[i].ToString(), surname1[i].ToString(),
                                name1[i].ToString(), patronymic1[i].ToString(), typerate1[i].ToString(),
                                money1[i].ToString(), rate1[i].ToString(), finalmoney1[i].ToString(), deposit1[i].ToString());
                            InnerFinanceTransactionWindow.FinalAccount3.Add(FinalAccount2);
                        }
                    }
                    InnerFinanceTransactionWindow.DG5.ItemsSource = InnerFinanceTransactionWindow.FinalAccount3;
                    InnerFinanceTransactionWindow.ShowDialog();

                    if (InnerFinanceTransactionWindow.IsTransact == true)
                    {
                        for (int a = 0; a < InnerFinanceTransactionWindow.FinalAccount3.Count; a++)
                        {
                            for (int j = 0; j < Count; j++)
                            {
                                string s1 = id1[j].ToString();
                                string s2 = InnerFinanceTransactionWindow.FinalAccount3[a].IDChek.ToString();
                                if (s1 == s2)
                                {
                                    id1[j] = InnerFinanceTransactionWindow.FinalAccount3[a].IDChek.ToString();
                                    surname1[j] = InnerFinanceTransactionWindow.FinalAccount3[a].Surname.ToString();
                                    name1[j] = InnerFinanceTransactionWindow.FinalAccount3[a].Name.ToString();
                                    patronymic1[j] = InnerFinanceTransactionWindow.FinalAccount3[a].Patronymic.ToString();
                                    typerate1[j] = InnerFinanceTransactionWindow.FinalAccount3[a].Typerate.ToString();
                                    money1[j] = InnerFinanceTransactionWindow.FinalAccount3[a].Money.ToString();
                                    rate1[j] = InnerFinanceTransactionWindow.FinalAccount3[a].Rate.ToString();
                                    finalmoney1[j] = InnerFinanceTransactionWindow.FinalAccount3[a].FinalMoney.ToString();
                                    deposit1[j] = InnerFinanceTransactionWindow.FinalAccount3[a].Deposit.ToString();
                                    break;
                                }
                            }
                        }

                        DataContractJsonSerializer formatter1 = new DataContractJsonSerializer(typeof(List<FinalAccount>));
                        List<FinalAccount> FinalAccount1 = new List<FinalAccount>();
                        for (int j = 0; j < Count; j++)
                        {
                            FinalAccount FinalAccount2 = new FinalAccount(id1[j].ToString(), surname1[j].ToString(),
                                name1[j].ToString(), patronymic1[j].ToString(), typerate1[j].ToString(),
                                money1[j].ToString(), rate1[j].ToString(), finalmoney1[j].ToString(), deposit1[j].ToString());

                            FinalAccount1.Add(FinalAccount2);
                        }
                        using (FileStream fs = new FileStream("2.json", FileMode.Create))
                        {
                            formatter1.WriteObject(fs, FinalAccount1);
                        }
                        DG4.ItemsSource = FinalAccount1;
                    }
                    if (InnerFinanceTransactionWindow.IsRecord == true)
                    {
                        RecordChanged?.Invoke(InnerFinanceTransactionWindow.Record);
                        RecordChanged -= Handler;
                    }
                }
                else
                {
                    MessageBox.Show("Счет закрыт");
                }
            }
            else
            {
                MessageBox.Show("Выберите клиента");
            }
        }

        // Транзакции между счетами разных клиентов 
        public void ExternalFinanceTransaction(DataGrid dataGrid)
        {
            RecordChanged += Handler;
            if (dataGrid.SelectedCells.Count != 0)
            {
                int index = dataGrid.SelectedIndex;
                if (!(id1[index].Contains("_Closed")))
                {
                    ExternalFinanceTransaction ExternalFinanceTransactionWindow = new ExternalFinanceTransaction();
                    ExternalFinanceTransactionWindow.Label1.Content = id1[index];
                    ExternalFinanceTransactionWindow.Label2.Content = surname1[index].ToString();
                    ExternalFinanceTransactionWindow.Label3.Content = name1[index].ToString();
                    ExternalFinanceTransactionWindow.Label4.Content = patronymic1[index].ToString();
                    ExternalFinanceTransactionWindow.Label5.Content = typerate1[index].ToString();
                    ExternalFinanceTransactionWindow.Label6.Content = money1[index].ToString();
                    ExternalFinanceTransactionWindow.Label7.Content = rate1[index].ToString();
                    ExternalFinanceTransactionWindow.Label8.Content = finalmoney1[index].ToString();
                    ExternalFinanceTransactionWindow.Label17.Content = deposit1[index].ToString();

                    for (int i = 0; i < Count; i++)
                    {
                        FinalAccount FinalAccount2 = new FinalAccount(id1[i].ToString(), surname1[i].ToString(),
                            name1[i].ToString(), patronymic1[i].ToString(), typerate1[i].ToString(),
                            money1[i].ToString(), rate1[i].ToString(), finalmoney1[i].ToString(), deposit1[i].ToString());
                        ExternalFinanceTransactionWindow.FinalAccount3.Add(FinalAccount2);
                    }
                    ExternalFinanceTransactionWindow.DG7.ItemsSource = ExternalFinanceTransactionWindow.FinalAccount3;
                    ExternalFinanceTransactionWindow.ShowDialog();

                    if (ExternalFinanceTransactionWindow.IsTransact == true)
                    {
                        for (int a = 0; a < ExternalFinanceTransactionWindow.FinalAccount3.Count; a++)
                        {
                            for (int j = 0; j < Count; j++)
                            {
                                string s1 = id1[j].ToString();
                                string s2 = ExternalFinanceTransactionWindow.FinalAccount3[a].IDChek.ToString();
                                if (s1 == s2)
                                {
                                    id1[j] = ExternalFinanceTransactionWindow.FinalAccount3[a].IDChek.ToString();
                                    surname1[j] = ExternalFinanceTransactionWindow.FinalAccount3[a].Surname.ToString();
                                    name1[j] = ExternalFinanceTransactionWindow.FinalAccount3[a].Name.ToString();
                                    patronymic1[j] = ExternalFinanceTransactionWindow.FinalAccount3[a].Patronymic.ToString();
                                    typerate1[j] = ExternalFinanceTransactionWindow.FinalAccount3[a].Typerate.ToString();
                                    money1[j] = ExternalFinanceTransactionWindow.FinalAccount3[a].Money.ToString();
                                    rate1[j] = ExternalFinanceTransactionWindow.FinalAccount3[a].Rate.ToString();
                                    finalmoney1[j] = ExternalFinanceTransactionWindow.FinalAccount3[a].FinalMoney.ToString();
                                    deposit1[j] = ExternalFinanceTransactionWindow.FinalAccount3[a].Deposit.ToString();
                                    break;
                                }
                            }
                        }

                        DataContractJsonSerializer formatter1 = new DataContractJsonSerializer(typeof(List<FinalAccount>));
                        List<FinalAccount> FinalAccount1 = new List<FinalAccount>();
                        for (int j = 0; j < Count; j++)
                        {
                            FinalAccount FinalAccount2 = new FinalAccount(id1[j].ToString(), surname1[j].ToString(),
                                name1[j].ToString(), patronymic1[j].ToString(), typerate1[j].ToString(),
                                money1[j].ToString(), rate1[j].ToString(), finalmoney1[j].ToString(), deposit1[j].ToString());

                            FinalAccount1.Add(FinalAccount2);
                        }
                        using (FileStream fs = new FileStream("2.json", FileMode.Create))
                        {
                            formatter1.WriteObject(fs, FinalAccount1);
                        }
                        DG4.ItemsSource = FinalAccount1;
                    }
                    if (ExternalFinanceTransactionWindow.IsRecord == true)
                    {
                        RecordChanged?.Invoke(ExternalFinanceTransactionWindow.Record);
                        RecordChanged -= Handler;
                    }
                }
                else
                {
                    MessageBox.Show("Счет закрыт");
                }
            }
            else
            {
                MessageBox.Show("Выберите клиента");
            }
        }

        private void UpdateDataGrid4(object sender, System.ComponentModel.CancelEventArgs e)
        {
            List<FinalAccount> FinalAccManage = new List<FinalAccount>();
            DataContractJsonSerializer formatter2 = new DataContractJsonSerializer(typeof(List<FinalAccount>));
            using (FileStream fs = new FileStream("2.json", FileMode.OpenOrCreate))
            {
                FinalAccManage = (List<FinalAccount>)formatter2.ReadObject(fs);
            }
            DG4.ItemsSource = FinalAccManage;
        }

        // Открытие нового счета
        public void OpenNewAccount()
        {
            RecordChanged += Handler;
            OpenNewAccount OpenNewAccountWindow = new OpenNewAccount();
            OpenNewAccountWindow.Closing += UpdateDataGrid4;
            List<Consultant> ListClientsOpen = new List<Consultant>();
            DataContractJsonSerializer formatter1 = new DataContractJsonSerializer(typeof(List<Consultant>));
            using (FileStream fs = new FileStream("1.json", FileMode.OpenOrCreate))
            {
                ListClientsOpen = (List<Consultant>)formatter1.ReadObject(fs);
            }
            OpenNewAccountWindow.DG6.ItemsSource = ListClientsOpen;
            OpenNewAccountWindow.AccountsCount = Count;
            for (int i = 0; i < ListClientsOpen.Count; i++)
            {
                OpenNewAccountWindow.surname2[i] = ListClientsOpen[i].Surname;
                OpenNewAccountWindow.name2[i] = ListClientsOpen[i].Name;
                OpenNewAccountWindow.patronymic2[i] = ListClientsOpen[i].Patronymic;
                OpenNewAccountWindow.number_phone2[i] = ListClientsOpen[i].Number_phone;
                OpenNewAccountWindow.passport2[i] = ListClientsOpen[i].Passport;
                OpenNewAccountWindow.status2[i] = ListClientsOpen[i].Status;
                OpenNewAccountWindow.datetimechangenote2[i] = ListClientsOpen[i].Datetimechangenote;
                OpenNewAccountWindow.changedata2[i] = ListClientsOpen[i].Changedata;
                OpenNewAccountWindow.changetype2[i] = ListClientsOpen[i].Changetype;
                OpenNewAccountWindow.changeagent2[i] = ListClientsOpen[i].Changeagent;
            }
            OpenNewAccountWindow.ShowDialog();
            if (OpenNewAccountWindow.IsRecord == true) 
            { 
                RecordChanged?.Invoke(OpenNewAccountWindow.Record);
                RecordChanged -= Handler;
            }
        }

        // Пополнение счета
        public void AddMoneyAccount(DataGrid dataGrid)
        {
            RecordChanged += Handler;
            if (dataGrid.SelectedCells.Count != 0)
            {
                int index = dataGrid.SelectedIndex;
                if (!(id1[index].Contains("_Closed")))
                {
                    AddMoneyAccount AddMoneyAccountWindow = new AddMoneyAccount();
                    AddMoneyAccountWindow.Label1.Content = id1[index];
                    AddMoneyAccountWindow.Label2.Content = surname1[index].ToString();
                    AddMoneyAccountWindow.Label3.Content = name1[index].ToString();
                    AddMoneyAccountWindow.Label4.Content = patronymic1[index].ToString();
                    AddMoneyAccountWindow.Label5.Content = typerate1[index].ToString();
                    AddMoneyAccountWindow.Label6.Content = money1[index].ToString();
                    AddMoneyAccountWindow.Label7.Content = rate1[index].ToString();
                    AddMoneyAccountWindow.Label8.Content = finalmoney1[index].ToString();
                    AddMoneyAccountWindow.Label9.Content = deposit1[index].ToString();

                    if (AddMoneyAccountWindow.Label9.Content.ToString() == "Deposit")
                    {
                        AddMoneyAccountWindow.Label10.Content = "5%";
                    }
                    else
                    {
                        AddMoneyAccountWindow.Label10.Content = "0%";
                    }

                    AddMoneyAccountWindow.ShowDialog();

                    if (AddMoneyAccountWindow.Changedata == 1)
                    {
                        id1[index] = AddMoneyAccountWindow.Label1.Content.ToString();
                        surname1[index] = AddMoneyAccountWindow.Label2.Content.ToString();
                        name1[index] = AddMoneyAccountWindow.Label3.Content.ToString();
                        patronymic1[index] = AddMoneyAccountWindow.Label4.Content.ToString();
                        typerate1[index] = AddMoneyAccountWindow.Label5.Content.ToString();
                        money1[index] = AddMoneyAccountWindow.Money.ToString();
                        rate1[index] = AddMoneyAccountWindow.Label7.Content.ToString();
                        finalmoney1[index] = AddMoneyAccountWindow.FinalMoney.ToString();
                        deposit1[index] = AddMoneyAccountWindow.Label9.Content.ToString();

                        string text1, text2, text3, text4, text5, text6, text7, text8, text9;
                        List<FinalAccount> FinalAccount1 = new List<FinalAccount>();
                        DataContractJsonSerializer formatter1 = new DataContractJsonSerializer(typeof(List<FinalAccount>));

                        for (int i = 0; i < Count; i++)
                        {
                            text1 = id1[i].ToString();
                            text2 = surname1[i].ToString();
                            text3 = name1[i].ToString();
                            text4 = patronymic1[i].ToString();
                            text5 = typerate1[i].ToString();
                            text6 = money1[i].ToString();
                            text7 = rate1[i].ToString();
                            text8 = finalmoney1[i].ToString();
                            text9 = deposit1[i].ToString();

                            FinalAccount FinalAccount2 = new FinalAccount(text1, text2, text3, text4, text5, text6, text7, text8, text9);
                            FinalAccount1.Add(FinalAccount2);
                        }
                        using (FileStream fs = new FileStream("2.json", FileMode.Create))
                        {
                            formatter1.WriteObject(fs, FinalAccount1);
                        }
                        using (FileStream fs = new FileStream("2.json", FileMode.OpenOrCreate))
                        {
                            FinalAccount1 = (List<FinalAccount>)formatter1.ReadObject(fs);
                        }
                        DG4.ItemsSource = FinalAccount1;
                    }
                    if (AddMoneyAccountWindow.IsRecord == true)
                    {
                        RecordChanged?.Invoke(AddMoneyAccountWindow.Record);
                        RecordChanged -= Handler;
                    }
                }
                else
                {
                    MessageBox.Show("Счет закрыт");
                }
            }
            else
            {
                MessageBox.Show("Выберите клиента");
            }
        }

        // Закрытие счета
        public void CloseAccount(DataGrid dataGrid)
        {
            RecordChanged += Handler;
            if (dataGrid.SelectedCells.Count != 0)
            {
                int index = dataGrid.SelectedIndex;
                string id_ = id1[index];
                id1[index] = id1[index].ToString() + "_Closed";
                string text1, text2, text3, text4, text5, text6, text7, text8, text9;
                List<FinalAccount> FinalAccount1 = new List<FinalAccount>();
                DataContractJsonSerializer formatter1 = new DataContractJsonSerializer(typeof(List<FinalAccount>));

                for (int i = 0; i < Count; i++)
                {
                    text1 = id1[i].ToString();
                    text2 = surname1[i].ToString();
                    text3 = name1[i].ToString();
                    text4 = patronymic1[i].ToString();
                    text5 = typerate1[i].ToString();
                    text6 = money1[i].ToString();
                    text7 = rate1[i].ToString();
                    text8 = finalmoney1[i].ToString();
                    text9 = deposit1[i].ToString();

                    FinalAccount FinalAccount2 = new FinalAccount(text1, text2, text3, text4, text5, text6, text7, text8, text9);
                    FinalAccount1.Add(FinalAccount2);
                }
                using (FileStream fs = new FileStream("2.json", FileMode.Create))
                {
                    formatter1.WriteObject(fs, FinalAccount1);
                }
                using (FileStream fs = new FileStream("2.json", FileMode.OpenOrCreate))
                {
                    FinalAccount1 = (List<FinalAccount>)formatter1.ReadObject(fs);
                }
                DG4.ItemsSource = FinalAccount1;
                RecordChanged?.Invoke($"[Manager {DateTime.Now}] Клиент {surname1[index].ToString()} {name1[index].ToString()} {patronymic1[index].ToString()} закрыл счет c номером {id_}");
                RecordChanged -= Handler;
            }
            else
            {
                MessageBox.Show("Выберите клиента");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            EditAccountsList(DG4);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            InnerFinanceTransaction(DG4);
        }
        
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            OpenNewAccount();
            List<FinalAccount> FinalAccManage = new List<FinalAccount>();
            DataContractJsonSerializer formatter2 = new DataContractJsonSerializer(typeof(List<FinalAccount>));
            using (FileStream fs = new FileStream("2.json", FileMode.OpenOrCreate))
            {
                FinalAccManage = (List<FinalAccount>)formatter2.ReadObject(fs);
            }
            DG4.ItemsSource = FinalAccManage;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            AddMoneyAccount(DG4);
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            ExternalFinanceTransaction(DG4);
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            CloseAccount(DG4);
        }
    }
}
