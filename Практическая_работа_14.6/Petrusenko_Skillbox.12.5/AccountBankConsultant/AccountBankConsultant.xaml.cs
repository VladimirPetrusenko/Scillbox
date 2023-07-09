using System.Collections.Generic;
using System.Windows;
using System.Runtime.Serialization.Json;
using System.IO;
using ConsultantManagerLibrary;

namespace Petrusenko_Skillbox._11._6
{
    /// <summary>
    /// Логика взаимодействия для AccountBankConsultant.xaml
    /// </summary>
    public partial class AccountBankConsultant : Window
    {
        public AccountBankConsultant()
        {
            InitializeComponent();
            List<FinalAccount> Cons2 = new List<FinalAccount>();
            DataContractJsonSerializer formatter1 = new DataContractJsonSerializer(typeof(List<FinalAccount>));
            using (FileStream fs = new FileStream("2.json", FileMode.OpenOrCreate))
            {
                Cons2 = (List<FinalAccount>)formatter1.ReadObject(fs);
            }
            DG3.ItemsSource = Cons2;
        }
    }
}
