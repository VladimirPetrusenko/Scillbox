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

        public DbSet<PhoneBookWebApplication.PhoneBook> PhoneBook { get; set; }
    }
}
