using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using PhoneBookWebApplication.AuthIdentityPhoneBookWebApplication;


namespace PhoneBookWebApplication.DataContext
{
    public class PhoneBookWebApplicationContext : IdentityDbContext<User>
    {
        public PhoneBookWebApplicationContext(DbContextOptions<PhoneBookWebApplicationContext> options)
           : base(options)
        {
        }
        public PhoneBookWebApplicationContext()
            : base()
        {
        }

        //public DbSet<IdentityPhoneBookWebApplication.PhoneBook> PhoneBook { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-26BRAK1\SQLEXPRESS;
                                        DataBase=DBWebPhoneBook;
                                        Trusted_Connection=True;");
        }
    }
}
