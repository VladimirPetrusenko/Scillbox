using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Runtime.Serialization.Json;
using System.IO;

namespace ConsultantManagerLibrary
{
    [Serializable]
    public class Consultant
    {
        protected string surname;
        protected string name;
        protected string patronymic;
        protected string number_phone;
        protected string passport;
        protected string datetimechangenote;
        protected string changedata;
        protected string changetype;
        protected string changeagent;
        protected string status;
        protected int n = 1;

        Random rnd = new Random();

        protected string[] array = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", 
            "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"};

        public Consultant()
        {

        }
        public Consultant(string surname_, string name_, string patronymic_, string number_phone_,
            string passport_, string status_, string datetimechangenote_, string changedata_, string changetype_,
            string changeagent_)
        {
            surname = surname_;
            name = name_;
            patronymic = patronymic_;
            number_phone = number_phone_;
            passport = passport_;
            status = status_;
            datetimechangenote = datetimechangenote_;
            changedata = changedata_;
            changetype = changetype_;
            changeagent = changeagent_;
        }

        List<Consultant> Consultants = new List<Consultant>();

        public string Surname
        {
            get { return surname; }
        }

        public string Name
        {
            get { return name; }
        }

        public string Patronymic
        {
            get { return patronymic; }
        }

        public string Number_phone
        {
            get { return number_phone; }
            set { if (value != null) number_phone = value; else number_phone = "000-000"; }
        }

        public string Passport
        {
            get { return passport; }
            set { passport = value; }
        }

        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        public string Datetimechangenote
        {
            get { if (datetimechangenote == "") return "-"; else return datetimechangenote; }
        }

        public string Changedata
        {
            get { if (changedata == "") return "-"; else return changedata; }
        }

        public string Changetype
        {
            get { if (changetype == "") return "-"; else return changetype; }
        }

        public string Changeagent
        {
            get { if (changeagent == "") return "-"; else return changeagent; }
        }

        

        [NonSerialized()]

        public string passport_add;

        public class SortByName: IComparer<Consultant>
        {
            public int Compare(Consultant x, Consultant y)
            {
                Consultant X = (Consultant)x;
                Consultant Y = (Consultant)y;

                return String.Compare(X.Name, Y.Name);
            }
        }

        List<FinalAccount> FinalAccounts = new List<FinalAccount>();

        public virtual void ClientBaseCreate()
        {
            if (n <= 20)
            {
                //cписок клиентов

                int index = rnd.Next(26);
                string surname1 = $"Фамилия{n}";
                string name1 = array[index] + array[index] + array[index];
                string patronymic1 = $"Отчество{n}";
                string number_phone1 = $"{n}-000";
                string passport1 = $"{n}-11 {n}-00";
                string datetimechangenote1 = "";
                string changedata1 = "";
                string changetype1 = "";
                string changeagent1 = "";
                string status1;

                if ((n % 2) == 0) { status1 = "standard"; } else { status1 = "premium"; }
                Consultants.Add(new Consultant(surname = surname1, name = name1,
                patronymic = patronymic1, number_phone = number_phone1, passport = passport1, status = status1,
                datetimechangenote = datetimechangenote1, changedata = changedata1, changetype = changetype1,
                changeagent = changeagent1));
                
                int money1 = 100000;
                int rate1 = 5;
                double money2 = 100000.5;
                double rate2 = 7.5;
                string id_account1 = $"{n}-1";
                string id_account2 = $"{n}-2";

                if ((status1 == "standard"))
                { 
                    BankAccount<int, int> BankAccount1 = new BankAccount<int, int>("Capital", money1, rate1);
                    FinalAccounts.Add(new FinalAccount(id_account1, surname1, name1, patronymic1, BankAccount1.Typerate, BankAccount1.Money.ToString(), 
                    BankAccount1.Rate.ToString(), BankAccount1.FinalYearMoney(BankAccount1.Typerate, BankAccount1.Money, BankAccount1.Rate), "Deposit"));

                    BankAccount<int, int> BankAccount2 = new BankAccount<int, int>("noCapital", money1, rate1);
                    FinalAccounts.Add(new FinalAccount(id_account2, surname1, name1, patronymic1, BankAccount2.Typerate, BankAccount2.Money.ToString(),
                    BankAccount1.Rate.ToString(), BankAccount1.FinalYearMoney(BankAccount2.Typerate, BankAccount2.Money, BankAccount2.Rate), "noDeposit"));
                }

                if ((status1 == "premium"))
                { 
                    BankAccount<double, double> BankAccount1 = new BankAccount<double, double>("Capital", money2, rate2);
                    FinalAccounts.Add(new FinalAccount(id_account1, surname1, name1, patronymic1, BankAccount1.Typerate, BankAccount1.Money.ToString(),
                    BankAccount1.Rate.ToString(), BankAccount1.FinalYearMoney(BankAccount1.Typerate, BankAccount1.Money, BankAccount1.Rate), "Deposit"));

                    BankAccount<double, double> BankAccount2 = new BankAccount<double, double>("noCapital", money2, rate2);
                    FinalAccounts.Add(new FinalAccount(id_account2, surname1, name1, patronymic1, BankAccount2.Typerate, BankAccount2.Money.ToString(),
                    BankAccount2.Rate.ToString(), BankAccount2.FinalYearMoney(BankAccount2.Typerate, BankAccount2.Money, BankAccount2.Rate), "noDeposit"));
                }

                n++;
                ClientBaseCreate();
            }
            if (n > 20)
            {
                DataContractJsonSerializer formatter1 = new DataContractJsonSerializer(typeof(List<Consultant>));
                using (FileStream fs = new FileStream(@"1.json", FileMode.Create))
                {
                    formatter1.WriteObject(fs, Consultants);
                }
            }
            if (n > 20)
            {
                DataContractJsonSerializer formatter1 = new DataContractJsonSerializer(typeof(List<FinalAccount>));
                using (FileStream fs = new FileStream(@"2.json", FileMode.Create))
                {
                    formatter1.WriteObject(fs, FinalAccounts);
                }
            }
        }
    }
}
