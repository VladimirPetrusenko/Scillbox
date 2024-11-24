using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SkillProfiWebApplication.Data;
using SkillProfiWebApplication.IdentitySkillProfi;

namespace SkillProfiWebApplication.DataContext
{
    public class SkillProfiContext: IdentityDbContext<User>
    {
        public SkillProfiContext(DbContextOptions<SkillProfiContext> options)
           : base(options)
        {
        }
        public SkillProfiContext()
            : base()
        {
        }

        public DbSet<AppElement> AppElements { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<Service> Services { get; set; }

        public DbSet<Post> Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-26BRAK1\SQLEXPRESS;
                                        DataBase=DB_WebSkillProfi;
                                        Trusted_Connection=True;");
        }
    }
}
