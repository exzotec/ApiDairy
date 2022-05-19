using Microsoft.EntityFrameworkCore;

namespace ApiDairy.Models
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData
            (
                    new User { UserId = 1, Login = "Tom", Password = "" , Role = ""},
                    new User { UserId = 2, Login = "Tom", Password = "", Role = "" },
                    new User { UserId = 3, Login = "Tom", Password = "", Role = "" },
                    new User { UserId = 4, Login = "Tom", Password = "", Role = "" },
                    new User { UserId = 5, Login = "Tom", Password = "", Role = "" }
            );
        }
    }
}
