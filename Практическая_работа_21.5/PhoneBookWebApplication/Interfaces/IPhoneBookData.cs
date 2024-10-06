using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBookWebApplication.Interfaces
{
    interface IPhoneBookData
    {
        IEnumerable<PhoneBook> GetPhoneBookEntry();

        void AddPhoneBookEntry(PhoneBook phoneBook);

        void DeletePhoneBookEntry(PhoneBook phoneBook);

        void EditPhoneBookEntry(PhoneBook phoneBook);

        PhoneBook FindPhoneBookEntry(int phoneBookId);
    }
}
