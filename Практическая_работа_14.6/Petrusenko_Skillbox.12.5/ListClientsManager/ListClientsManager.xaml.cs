using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;
using ConsultantManagerLibrary;

namespace Petrusenko_Skillbox._11._6
{
    /// <summary>
    /// Логика взаимодействия для ListClientsManager.xaml
    /// </summary>
    /// 

    public static class ClientsManagerExtension
    {
        public static double ClientMoneyCalculate (this Manager client)
        {
            List<FinalAccount> FinalAccManage = new List<FinalAccount>();
            DataContractJsonSerializer formatter1 = new DataContractJsonSerializer(typeof(List<FinalAccount>));
            using (FileStream fs = new FileStream("2.json", FileMode.OpenOrCreate))
            {
                FinalAccManage = (List<FinalAccount>)formatter1.ReadObject(fs);
            }
            FinalAccount finalAccManageTemp = new FinalAccount(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty,
              "0", string.Empty, string.Empty, string.Empty);
            double moneyClient = 0;
            foreach (FinalAccount finalAccManage in FinalAccManage)
            {
                if (finalAccManage.Surname == client.Surname && finalAccManage.Name == client.Name && finalAccManage.Patronymic == client.Patronymic)
                {
                    finalAccManageTemp = finalAccManageTemp + finalAccManage;
                }
            }
            moneyClient = Convert.ToDouble(finalAccManageTemp.Money);
            return moneyClient;
        }
    }

    public partial class ListClientsManager : Window, IEditManager
    {
        string[] surname1 = new string[50];
        string[] name1 = new string[50];
        string[] patronimyc1 = new string[50];
        string[] number_phone1 = new string[50];
        string[] passport1 = new string[50];
        string[] status1 = new string[50];
        string[] datetimechangenote1 = new string[50];
        string[] changedata1 = new string[50];
        string[] changetype1 = new string[50];
        string[] changeagent1 = new string[50];
        string number = "";
        int count;

        public int Count
        {
            get { return count; }
            set { count = value; }
        }

        public string Number
        {
            set { number = value; }
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

        public ListClientsManager()
        {
            InitializeComponent();
            List<Manager> Manage = new List<Manager>();

            DataContractJsonSerializer formatter = new DataContractJsonSerializer(typeof(List<Manager>));

            using (FileStream fs = new FileStream("1.json", FileMode.OpenOrCreate))
            {
                Manage = (List<Manager>)formatter.ReadObject(fs);
            }
            DG2.ItemsSource = Manage;
            Count = Manage.Count;
            for (int i = 0; i < Manage.Count; i++)
            {
                surname1[i] = Manage[i].Surname;
                name1[i] = Manage[i].Name;
                patronimyc1[i] = Manage[i].Patronymic;
                number_phone1[i] = Manage[i].Number_phone;
                passport1[i] = Manage[i].Passport;
                status1[i] = Manage[i].Status;
                datetimechangenote1[i] = Manage[i].Datetimechangenote;
                changedata1[i] = Manage[i].Changedata;
                changetype1[i] = Manage[i].Changetype;
                changeagent1[i] = Manage[i].Changeagent;
            }
        }

        // Редактирование информации по клиенту
        public void EditClientsList(DataGrid dataGrid)
        {
            RecordChanged += Handler;
            if (dataGrid.SelectedCells.Count != 0)
            {
                EditClientListManager EditClientListManagerWindow = new EditClientListManager();
                int index = dataGrid.SelectedIndex;
                EditClientListManagerWindow.TextBox1.Text = surname1[index].ToString();
                EditClientListManagerWindow.surname2 = surname1[index].ToString();
                EditClientListManagerWindow.TextBox2.Text = name1[index].ToString();
                EditClientListManagerWindow.name2 = name1[index].ToString();
                EditClientListManagerWindow.TextBox3.Text = patronimyc1[index].ToString();
                EditClientListManagerWindow.patronimyc2 = patronimyc1[index].ToString();
                EditClientListManagerWindow.TextBox4.Text = number_phone1[index].ToString();
                EditClientListManagerWindow.number_phone2 = number_phone1[index].ToString();
                EditClientListManagerWindow.TextBox5.Text = passport1[index].ToString();
                EditClientListManagerWindow.passport2 = passport1[index].ToString();
                EditClientListManagerWindow.ComboBox1.Text = status1[index].ToString();
                EditClientListManagerWindow.status2 = status1[index].ToString();

                string text11, text12, text13, text14, text15, text16;
                text11 = surname1[index].ToString();
                text12 = name1[index].ToString();
                text13 = patronimyc1[index].ToString();
                text14 = number_phone1[index].ToString();
                text15 = passport1[index].ToString();
                text16 = status1[index].ToString();

                EditClientListManagerWindow.ShowDialog();

                if (EditClientListManagerWindow.TextBox1.Text.ToString() != surname1[index].ToString() ||
                    EditClientListManagerWindow.TextBox2.Text.ToString() != name1[index].ToString() ||
                    EditClientListManagerWindow.TextBox3.Text.ToString() != patronimyc1[index].ToString() ||
                    EditClientListManagerWindow.TextBox4.Text.ToString() != number_phone1[index].ToString() ||
                    EditClientListManagerWindow.TextBox5.Text.ToString() != passport1[index].ToString() ||
                    EditClientListManagerWindow.ComboBox1.Text.ToString() != status1[index].ToString())
                {
                    MessageBox.Show("Ok");
                    surname1[index] = EditClientListManagerWindow.TextBox1.Text.ToString();
                    name1[index] = EditClientListManagerWindow.TextBox2.Text.ToString();
                    patronimyc1[index] = EditClientListManagerWindow.TextBox3.Text.ToString();
                    number_phone1[index] = EditClientListManagerWindow.TextBox4.Text.ToString();
                    passport1[index] = EditClientListManagerWindow.TextBox5.Text.ToString();
                    status1[index] = EditClientListManagerWindow.ComboBox1.Text.ToString();
                    MessageBox.Show(surname1[index].ToString());
                    datetimechangenote1[index] = DateTime.Now.ToString();
                    changedata1[index] = "";
                    if (EditClientListManagerWindow.TextBox1.Text.ToString() != EditClientListManagerWindow.surname2) { changedata1[index] = changedata1[index] + "Фамилия"; }
                    if (EditClientListManagerWindow.TextBox2.Text.ToString() != EditClientListManagerWindow.name2) { changedata1[index] = changedata1[index] + "Имя"; }
                    if (EditClientListManagerWindow.TextBox3.Text.ToString() != EditClientListManagerWindow.patronimyc2) { changedata1[index] = changedata1[index] + "Отчество"; }
                    if (EditClientListManagerWindow.TextBox4.Text.ToString() != EditClientListManagerWindow.number_phone2) { changedata1[index] = changedata1[index] + "Номер телефона"; }
                    if (EditClientListManagerWindow.TextBox5.Text.ToString() != EditClientListManagerWindow.passport2) { changedata1[index] = changedata1[index] + "Паспорт"; }
                    if (EditClientListManagerWindow.ComboBox1.Text.ToString() != EditClientListManagerWindow.status2) { changedata1[index] = changedata1[index] + "Статус"; }
                    changetype1[index] = "Корр.";
                    changeagent1[index] = "Менеджер";
                    List<Manager> Manage1 = new List<Manager>();
                    DataContractJsonSerializer formatter1 = new DataContractJsonSerializer(typeof(List<Manager>));
                    string text1, text2, text3, text4, text5, text6, text7, text8, text9, text10;
                    for (int i = 0; i < Count; i++)
                    {
                        text1 = surname1[i].ToString();
                        text2 = name1[i].ToString();
                        text3 = patronimyc1[i].ToString();
                        text4 = number_phone1[i].ToString();
                        text5 = passport1[i].ToString();
                        text6 = status1[i].ToString();
                        text7 = datetimechangenote1[i].ToString();
                        text8 = changedata1[i].ToString();
                        text9 = changetype1[i].ToString();
                        text10 = changeagent1[i].ToString();
                        Manager Cons2 = new Manager(text1, text2, text3, text4, text5, text6, text7, text8, text9, text10);
                        Manage1.Add(Cons2);
                    }
                    using (FileStream fs = new FileStream("1.json", FileMode.Create))
                    {
                        formatter1.WriteObject(fs, Manage1);
                    }
                    using (FileStream fs = new FileStream("1.json", FileMode.OpenOrCreate))
                    {
                        Manage1 = (List<Manager>)formatter1.ReadObject(fs);
                    }
                    DG2.ItemsSource = Manage1;
                    RecordChanged?.Invoke($"[Manager {DateTime.Now}] " +
                        $"Скорректирована информация по клиенту: {text11} {text12} {text13} {text14} {text15} {text16}" +
                        $"---> {surname1[index].ToString()} {name1[index].ToString()} {patronimyc1[index].ToString()} {number_phone1[index].ToString()} {passport1[index].ToString()} {status1[index].ToString()}");
                    RecordChanged -= Handler;
                }
            }
            else
            {
                MessageBox.Show("Выберите клиента");
            }
        }

        // Добавление нового клиента
        public void AddClientsList(DataGrid dataGrid)
        {
            List<Manager> Manage1 = new List<Manager>();
            EditClientListManager AddClientListManagerWindow = new EditClientListManager();
            AddClientListManagerWindow.TextBox1.Text = "";
            AddClientListManagerWindow.TextBox2.Text = "";
            AddClientListManagerWindow.TextBox3.Text = "";
            AddClientListManagerWindow.TextBox4.Text = "";
            AddClientListManagerWindow.TextBox5.Text = "";
            AddClientListManagerWindow.ComboBox1.Text = "standard";
            AddClientListManagerWindow.IsAddNewClient = true;
            AddClientListManagerWindow.ShowDialog();
            if (AddClientListManagerWindow.TextBox1.Text != "" || AddClientListManagerWindow.TextBox2.Text != "" 
                || AddClientListManagerWindow.TextBox3.Text != "" || AddClientListManagerWindow.TextBox4.Text != "" 
                || AddClientListManagerWindow.TextBox5.Text != "" || AddClientListManagerWindow.ComboBox1.Text != "")
            {
                surname1[Count] = AddClientListManagerWindow.TextBox1.Text.ToString();
                name1[Count] = AddClientListManagerWindow.TextBox2.Text.ToString();
                patronimyc1[Count] = AddClientListManagerWindow.TextBox3.Text.ToString();
                number_phone1[Count] = AddClientListManagerWindow.TextBox4.Text.ToString();
                passport1[Count] = AddClientListManagerWindow.TextBox5.Text.ToString();
                status1[Count] = AddClientListManagerWindow.ComboBox1.Text.ToString();
                datetimechangenote1[Count] = "";
                changedata1[Count] = "";
                changetype1[Count] = "";
                changeagent1[Count] = "";
                Count++;
                string text1, text2, text3, text4, text5, text6, text7, text8, text9, text10;
                DataContractJsonSerializer formatter1 = new DataContractJsonSerializer(typeof(List<Manager>));
                for (int i = 0; i < Count; i++)
                {
                    text1 = surname1[i].ToString();
                    text2 = name1[i].ToString();
                    text3 = patronimyc1[i].ToString();
                    text4 = number_phone1[i].ToString();
                    text5 = passport1[i].ToString();
                    text6 = status1[i].ToString();
                    text7 = datetimechangenote1[i].ToString();
                    text8 = changedata1[i].ToString();
                    text9 = changetype1[i].ToString();
                    text10 = changeagent1[i].ToString();
                    Manager Cons2 = new Manager(text1, text2, text3, text4, text5, text6, text7, text8, text9, text10);
                    Manage1.Add(Cons2);
                }
                using (FileStream fs = new FileStream("1.json", FileMode.Create))
                {
                    formatter1.WriteObject(fs, Manage1);
                }
                using (FileStream fs = new FileStream("1.json", FileMode.OpenOrCreate))
                {
                    Manage1 = (List<Manager>)formatter1.ReadObject(fs);
                }
                DG2.ItemsSource = Manage1;
            }
        }

        // Удаление клиента
        public void DeleteClientsList(DataGrid dataGrid)
        {
            if (dataGrid.SelectedCells.Count != 0)
            {
                int index = dataGrid.SelectedIndex;
                List<Manager> Cons1 = new List<Manager>();
                DataContractJsonSerializer formatter1 = new DataContractJsonSerializer(typeof(List<Manager>));
                using (FileStream fs = new FileStream("1.json", FileMode.OpenOrCreate))
                {
                    Cons1 = (List<Manager>)formatter1.ReadObject(fs);
                }
                Cons1.RemoveAt(index);
                DG2.ItemsSource = Cons1;
                using (FileStream fs = new FileStream("1.json", FileMode.Create))
                {
                    formatter1.WriteObject(fs, Cons1);
                }
            }
            else
            {
                MessageBox.Show("Выберите клиента");
            }
        }

        //Средства клиента
        public void MoneySelectedClient(DataGrid dataGrid)
        {
            if (dataGrid.SelectedCells.Count != 0)
           {
               int index = dataGrid.SelectedIndex;
               List<Manager> Cons1 = new List<Manager>();
               DataContractJsonSerializer formatter1 = new DataContractJsonSerializer(typeof(List<Manager>));
               using (FileStream fs = new FileStream("1.json", FileMode.OpenOrCreate))
               {
                   Cons1 = (List<Manager>)formatter1.ReadObject(fs);
               }
               double moneyClient;
               moneyClient = Cons1[index].ClientMoneyCalculate();
               MessageBox.Show(Convert.ToString(moneyClient));
           }
           else
           {
               MessageBox.Show("Выберите клиента");
           }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            EditClientsList(DG2);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AddClientsList(DG2);
        }

        // Сортировка
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            List<Manager> Cons1 = new List<Manager>();
            DataContractJsonSerializer formatter1 = new DataContractJsonSerializer(typeof(List<Manager>));
            using (FileStream fs = new FileStream("1.json", FileMode.OpenOrCreate))
            {
                Cons1 = (List<Manager>)formatter1.ReadObject(fs);
            }
            Cons1.Sort(new Manager.SortByName());
            DG2.ItemsSource = Cons1;
            using (FileStream fs = new FileStream("1.json", FileMode.Create))
            {
                formatter1.WriteObject(fs, Cons1);
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            DeleteClientsList(DG2);
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            AccountBankManager AccountBankManagerWindow = new AccountBankManager();
            AccountBankManagerWindow.ShowDialog();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            MoneySelectedClient(DG2);
        } 
    }
}
