using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBookWebApplication
{
    public class PhoneBook
    {
        public int ID { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string NumberPhone { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }

        public PhoneBook(int ID, string Surname, string Name, string Patronymic, string NumberPhone, string Address, string Description)
        {
            this.ID = ID;
            this.Surname = Surname;
            this.Name = Name;
            this.Patronymic = Patronymic;
            this.NumberPhone = NumberPhone;
            this.Address = Address;
            this.Description = Description;
        }

        public PhoneBook(/*int ID,*/ string Surname, string Name, string Patronymic, string NumberPhone, string Address, string Description)
        {
            //this.ID = ID;
            this.Surname = Surname;
            this.Name = Name;
            this.Patronymic = Patronymic;
            this.NumberPhone = NumberPhone;
            this.Address = Address;
            this.Description = Description;
        }
    }
}
