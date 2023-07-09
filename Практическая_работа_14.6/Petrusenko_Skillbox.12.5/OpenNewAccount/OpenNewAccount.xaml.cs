using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Runtime.Serialization.Json;
using System.IO;
using System;

namespace Petrusenko_Skillbox._11._6
{
    /// <summary>
    /// Логика взаимодействия для OpenNewAccount.xaml
    /// </summary>
    public partial class OpenNewAccount : Window
    {
        public int AccountsCount;

        public string[] surname2 = new string[50];
        public string[] name2 = new string[50];
        public string[] patronymic2 = new string[50];
        public string[] number_phone2 = new string[50];
        public string[] passport2 = new string[50];
        public string[] status2 = new string[50];
        public string[] datetimechangenote2 = new string[50];
        public string[] changedata2 = new string[50];
        public string[] changetype2 = new string[50];
        public string[] changeagent2 = new string[50];

        public bool IsOpenNewAccount = false;

        public OpenNewAccount()
        {
            InitializeComponent();
        }

        public void ThisClose(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Close();
        }

        public string Record = string.Empty;
        public bool IsRecord = false;

        public void SelectFromTheListClients(DataGrid dataGrid)
        {
            if (dataGrid.SelectedCells.Count != 0)
            {
                SelectFromTheListClients SelectFromTheListClientsWindow = new SelectFromTheListClients();
                SelectFromTheListClientsWindow.Closing += ThisClose;
                int index = dataGrid.SelectedIndex;
                double rate;
                SelectFromTheListClientsWindow.Label1.Content = surname2[index];
                SelectFromTheListClientsWindow.Label2.Content = name2[index];
                SelectFromTheListClientsWindow.Label3.Content = patronymic2[index];
                SelectFromTheListClientsWindow.Label4.Content = status2[index];

                switch (SelectFromTheListClientsWindow.Label4.Content)
                {
                    case "standard":
                        SelectFromTheListClientsWindow.Label5.Content = "5";
                        break;
                    case "premium":
                        SelectFromTheListClientsWindow.Label5.Content = "7,5";
                        break;
                }
                SelectFromTheListClientsWindow.ShowDialog();
                if (SelectFromTheListClientsWindow.IsRecord == true) { Record = SelectFromTheListClientsWindow.Record; IsRecord = true; }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SelectFromTheListClients(DG6);
        }
    }
}
