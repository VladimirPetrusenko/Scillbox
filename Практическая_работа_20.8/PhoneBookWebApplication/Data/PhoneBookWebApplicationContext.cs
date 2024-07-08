using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PhoneBookWebApplication;

namespace PhoneBookWebApplication.Data
{
    public class PhoneBookWebApplicationContext : DbContext
    {
        public PhoneBookWebApplicationContext (DbContextOptions<PhoneBookWebApplicationContext> options)
            : base(options)
        {
        }
        public PhoneBookWebApplicationContext()
            : base()
        {
        }

        public DbSet<PhoneBookWebApplication.PhoneBook> PhoneBook { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-26BRAK1\SQLEXPRESS;
                                        DataBase=PhoneBookDataBase;
                                        Trusted_Connection=True;");
        }

    }
}
