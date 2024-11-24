using Microsoft.EntityFrameworkCore;

namespace SkillProfiServiceApplication.DataContext
{
    public class RequestServiceContext : DbContext
    {
        public RequestServiceContext(DbContextOptions<RequestServiceContext> options)
            : base(options)
        {
        }

        public RequestServiceContext()
            : base()
        {
        }

        public DbSet<Request> Requests { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-26BRAK1\SQLEXPRESS;
                                        DataBase=DBRequestService;
                                        Trusted_Connection=True;");
        }
    }
}