namespace PhoneBookServiceApplication
{
    public class PhoneBook
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string NumberPhone { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }

        public PhoneBook(int Id, string Surname, string Name, string Patronymic, string NumberPhone, string Address, string Description)
        {
            this.Id = Id;
            this.Surname = Surname;
            this.Name = Name;
            this.Patronymic = Patronymic;
            this.NumberPhone = NumberPhone;
            this.Address = Address;
            this.Description = Description;
        }

        /*public PhoneBook(int Id, string Surname, string Name, string Patronymic, string NumberPhone, string Address, string Description)
        {
            this.Id = Id;
            this.Surname = Surname;
            this.Name = Name;
            this.Patronymic = Patronymic;
            this.NumberPhone = NumberPhone;
            this.Address = Address;
            this.Description = Description;
        }*/

        public PhoneBook()
        {

        }
    }
}
