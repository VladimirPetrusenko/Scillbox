using System.Collections.Generic;

namespace PhoneBookServiceApplication.Interfaces
{
    public interface IPhoneBookData
    {
        IEnumerable<PhoneBook> GetPhoneBookEntry();

        void AddPhoneBookEntry(PhoneBook phoneBook);

        void DeletePhoneBookEntry(PhoneBook phoneBook);

        void EditPhoneBookEntry(PhoneBook phoneBook);

        PhoneBook FindPhoneBookEntry(int phoneBookId);
    }
}
