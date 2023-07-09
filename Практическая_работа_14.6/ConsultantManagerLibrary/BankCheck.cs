using System;
using System.Windows;

namespace ConsultantManagerLibrary
{
    // Классы для реализации ковариантности и контрвариантности в проекте 
    public class NoDeposit
    {
        public double Money { get; set; }

        public NoDeposit(double money) => Money = money;
    }

    public class Deposit: NoDeposit
    {
        public Deposit(double money): base(money) => Money = money*0.95;
    }

    public class AddMoneyDeposit: IAccountAddMoney<Deposit>
    {
        public Deposit AccountAddMoney(double money)
        {
            return new Deposit(money);
        }
    }

    public class AddMoneyNoDeposit: IAccountAddMoney<NoDeposit>
    {
        public NoDeposit AccountAddMoney(double money)
        {
            return new NoDeposit(money);
        }
    }

    public class Transaction : ITransaction<NoDeposit>
    {
        public void Transact(NoDeposit noDeposit)
        {
            MessageBox.Show($"Сумма перевода с учетом комиссии: {noDeposit.Money}");
        }
    }

    // Класс обобщенный 
    public class BankAccount<T, K>
    {
        private string typerate;
        public string Typerate
        {
            get { return typerate; }
            set { typerate = value; }
        }
        private T money;
        public T Money
        {
            get { return money; }
            set { money = value; }
        }
        private K rate;
        public K Rate
        {
            get { return rate; }
            set { rate = value; }
        }
        public BankAccount(string typerate_, T money_, K rate_)
        {
            Typerate = typerate_;
            Money = money_;
            Rate = rate_;
        }
        public string FinalYearMoney(string typerate_, T money_, K rate_)
        {
            Type t_money = money_.GetType();
            Type t_rate = rate_.GetType();
            if (typerate_ == "Capital" && (t_money.Equals(typeof(int)) || t_money.Equals(typeof(double)))
                && (t_rate.Equals(typeof(int)) || t_rate.Equals(typeof(double))))
            { return (Convert.ToDouble(money_.ToString()) * Convert.ToDouble(rate_.ToString()) * 2).ToString(); }
            else if (typerate_ == "noCapital" && (t_money.Equals(typeof(int)) || t_money.Equals(typeof(double)))
                && (t_rate.Equals(typeof(int)) || t_rate.Equals(typeof(double))))
            { return (Convert.ToDouble(Convert.ToDouble(money_.ToString()) * Convert.ToDouble(rate_.ToString())).ToString()); }
            else
            { return "Неверно"; }
        }
    }

    [Serializable]
    public class FinalAccount
    {
        public static FinalAccount operator+ (FinalAccount x, FinalAccount y)
        {
            double a = (Convert.ToDouble(x.Money) + Convert.ToDouble(y.Money));
            return new FinalAccount(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, 
               a.ToString(), string.Empty, string.Empty, string.Empty);
        }

        private string idchek;
        public string IDChek
        {
            get { return idchek; }
            set { idchek = value; }
        }
        private string surnamename;
        public string Surname
        {
            get { return surnamename; }
            set { surnamename = value; }
        }
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private string patronymic;
        public string Patronymic
        {
            get { return patronymic; }
            set { patronymic = value; }
        }
        private string typerate;
        public string Typerate
        {
            get { return typerate; }
            set { typerate = value; }
        }
        private string money;
        public string Money
        {
            get { return money; }
            set { money = value; }
        }
        private string rate;
        public string Rate
        {
            get { return rate; }
            set { rate = value; }
        }
        private string finalmoney;
        public string FinalMoney
        {
            get { return finalmoney; }
            set { finalmoney = value; }
        }

        private string deposit;
        public string Deposit
        {
            get { return deposit; }
            set { deposit = value; }
        }

        public FinalAccount(string idchek_, string surname_, string name_, string patronymic_, string typerate_, 
            string money_, string rate_, string finalmoney_, string deposit_)
        {
            IDChek = idchek_;
            Surname = surname_;
            Name = name_;
            Patronymic = patronymic_;
            Typerate = typerate_;
            Money = money_;
            Rate = rate_;
            FinalMoney = finalmoney_;
            Deposit = deposit_;
        }
    }
}
