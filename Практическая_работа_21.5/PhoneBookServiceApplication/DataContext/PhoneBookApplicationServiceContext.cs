using Microsoft.EntityFrameworkCore;

namespace PhoneBookServiceApplication.DataContext
{
    public class PhoneBookApplicationServiceContext : DbContext
    {
        public PhoneBookApplicationServiceContext(DbContextOptions<PhoneBookApplicationServiceContext> options)
            : base(options)
        {
        }

        public PhoneBookApplicationServiceContext()
            : base()
        {
        }

        public DbSet<PhoneBook> PhoneBook { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-26BRAK1\SQLEXPRESS;
                                        DataBase=DBServicePhoneBook;
                                        Trusted_Connection=True;");
        }
    }
}
