using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;

namespace Petrusenko_Skillbox._11._6
{
    /// <summary>
    /// Логика взаимодействия для ListClientsConsultant.xaml
    /// </summary>
    public partial class ListClientsConsultant : Window, IEditConsultant
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

        List<Consultant> Cons_visible = new List<Consultant>();

        public ListClientsConsultant()
        {
            InitializeComponent();
            List<Consultant> Cons = new List<Consultant>();
            List<Consultant> Cons_visible = new List<Consultant>();

            DataContractJsonSerializer formatter = new DataContractJsonSerializer(typeof(List<Consultant>));

            using (FileStream fs = new FileStream("1.json", FileMode.OpenOrCreate))
            {
                Cons = (List<Consultant>)formatter.ReadObject(fs);
            }
            for (int j = 0; j < Cons.Count; j++)
            {
                Consultant Cons2 = new Consultant(Cons[j].Surname, Cons[j].Name, Cons[j].Patronymic, Cons[j].Number_phone,
                "***", Cons[j].Status, Cons[j].Datetimechangenote, Cons[j].Changedata, Cons[j].Changetype, Cons[j].Changeagent);
                Cons_visible.Add(Cons2);
            }
            DG1.ItemsSource = Cons_visible;
            Count = Cons.Count;
            for (int i = 0; i < Cons.Count; i++)
            {
                surname1[i] = Cons[i].Surname;
                name1[i] = Cons[i].Name;
                patronimyc1[i] = Cons[i].Patronymic;
                number_phone1[i] = Cons[i].Number_phone;
                passport1[i] = Cons[i].Passport;
                status1[i] = Cons[i].Status;
                datetimechangenote1[i] = Cons[i].Datetimechangenote;
                changedata1[i] = Cons[i].Changedata;
                changetype1[i] = Cons[i].Changetype;
                changeagent1[i] = Cons[i].Changeagent;
            }
        }
        // Редактирование информации по клиенту 
        public void EditClientsList(DataGrid dataGrid)
        {
            RecordChanged += Handler;
            if (dataGrid.SelectedCells.Count != 0)
            {
                EditClientListConsultant EditClientListConsultantWindow = new EditClientListConsultant();
                int index = dataGrid.SelectedIndex;
                EditClientListConsultantWindow.TextBox1.Text = number_phone1[index].ToString();
                EditClientListConsultantWindow.ShowDialog();
                if (EditClientListConsultantWindow.Number_phone_change != 0)
                {
                    MessageBox.Show("Ok");
                    number_phone1[index] = EditClientListConsultantWindow.textbox;
                    MessageBox.Show(number_phone1[index].ToString());
                    datetimechangenote1[index] = DateTime.Now.ToString();
                    changedata1[index] = "Ном.тел.";
                    changetype1[index] = "Корр.";
                    changeagent1[index] = "Консультант";
                    List<Consultant> Cons1 = new List<Consultant>();
                    DataContractJsonSerializer formatter1 = new DataContractJsonSerializer(typeof(List<Consultant>));
                    string text1, text2, text3, text4, text5, text6, text7, text8, text9, text10;
                    for (int i = 0; i < Count; i++)
                    {
                        text1 = surname1[i];
                        text2 = name1[i];
                        text3 = patronimyc1[i];
                        text4 = number_phone1[i];
                        text5 = passport1[i];
                        text6 = status1[i];
                        text7 = datetimechangenote1[i];
                        text8 = changedata1[i];
                        text9 = changetype1[i];
                        text10 = changeagent1[i];
                        Consultant Cons2 = new Consultant(text1, text2, text3, text4, text5, text6, text7, text8, text9, text10);
                        Cons1.Add(Cons2);
                    }
                    using (FileStream fs = new FileStream("1.json", FileMode.Create))
                    {
                        formatter1.WriteObject(fs, Cons1);
                    }
                    using (FileStream fs = new FileStream("1.json", FileMode.OpenOrCreate))
                    {
                        Cons1 = (List<Consultant>)formatter1.ReadObject(fs);
                    }
                    for (int j = 0; j < Cons1.Count; j++)
                    {
                        Consultant Cons2 = new Consultant(Cons1[j].Surname, Cons1[j].Name, Cons1[j].Patronymic, Cons1[j].Number_phone, 
                            "***", Cons1[j].Status, Cons1[j].Datetimechangenote, Cons1[j].Changedata, Cons1[j].Changetype, Cons1[j].Changeagent);
                        Cons_visible.Add(Cons2);
                    }
                    DG1.ItemsSource = Cons_visible;
                }
                if (EditClientListConsultantWindow.IsRecord == true)
                {
                    //Record = $"[Consultant {DateTime.Now}] По клиенту {surname1[index].ToString()} {name1[index].ToString()} {patronimyc1[index].ToString().ToString()} скорректирована информация";
                    RecordChanged?.Invoke($"[Consultant {DateTime.Now}] По клиенту {surname1[index].ToString()} {name1[index].ToString()} {patronimyc1[index].ToString().ToString()} скорректирована информация");
                    RecordChanged -= Handler;
                }
            }
            else
            {
                MessageBox.Show("Выберите клиента");
            }
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            EditClientsList(DG1);
        }
        // Сортировка клиентов
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            List<Consultant> Cons1 = new List<Consultant>();
            DataContractJsonSerializer formatter1 = new DataContractJsonSerializer(typeof(List<Consultant>));
            using (FileStream fs = new FileStream("1.json", FileMode.OpenOrCreate))
            {
                Cons1 = (List<Consultant>)formatter1.ReadObject(fs);
            }
            Cons1.Sort(new Consultant.SortByName());
            DG1.ItemsSource = Cons1;
            using (FileStream fs = new FileStream("1.json", FileMode.Create))
            {
                formatter1.WriteObject(fs, Cons1);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            AccountBankConsultant AccountBankConsultantWindow = new AccountBankConsultant();
            AccountBankConsultantWindow.ShowDialog();
        }
    }
}
