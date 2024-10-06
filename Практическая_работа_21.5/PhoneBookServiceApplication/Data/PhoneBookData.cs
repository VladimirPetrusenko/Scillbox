using System.Collections.Generic;
using System.Linq;
using PhoneBookServiceApplication.Interfaces;
using PhoneBookServiceApplication.DataContext;
using Microsoft.EntityFrameworkCore;


namespace PhoneBookServiceApplication.Data
{
    public class PhoneBookData : IPhoneBookData
    {
        public static DbContextOptions<PhoneBookApplicationServiceContext> options = new DbContextOptions<PhoneBookApplicationServiceContext>();

        PhoneBookApplicationServiceContext db = new PhoneBookApplicationServiceContext(options);

        public IEnumerable<PhoneBook> GetPhoneBookEntry()
        {
            IEnumerable<PhoneBook> phoneBooks = db.PhoneBook.ToList();
            
            return phoneBooks;
        }

        public void AddPhoneBookEntry(PhoneBook phoneBook)
        {
            db.PhoneBook.Add(phoneBook);

            db.SaveChanges();
        }

        public void DeletePhoneBookEntry(PhoneBook phoneBook)
        {
            db.PhoneBook.Remove(phoneBook);

            db.SaveChanges();
        }

        public void EditPhoneBookEntry(PhoneBook phoneBook)
        {
            db.PhoneBook.Update(phoneBook);

            db.SaveChanges();
        }

        public PhoneBook FindPhoneBookEntry(int id)
        {
            PhoneBook phoneBook = db.PhoneBook.Find(id);
            
            return phoneBook;
        }
    }
}
