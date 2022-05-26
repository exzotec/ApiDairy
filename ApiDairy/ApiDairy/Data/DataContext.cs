using ApiDairy.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace ApiDairy.Data
{
    public class DataContext : DbContext
    {
        public DataContext()
        {

        }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Office> Offices { get; set; }
        public DbSet<Timetable> Timetables { get; set; }
        public DbSet<Subject> Subjects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                    new User { UserId = Guid.NewGuid().ToString(), Login = "", Password = "", Role = "admin" },
                    new User { UserId = Guid.NewGuid().ToString(), Login = "", Password = "", Role = "teacher" },
                    new User { UserId = Guid.NewGuid().ToString(), Login = "", Password = "", Role = "headteacher" },
                    new User { UserId = Guid.NewGuid().ToString(), Login = "", Password = "", Role = "student" },
                    new User { UserId = Guid.NewGuid().ToString(), Login = "", Password = "", Role = "parent" });

            modelBuilder.Entity<Class>().HasData(
                    new Class { ClassId = Guid.NewGuid().ToString(), Number = 1, Letter = "А" });

            modelBuilder.Entity<Office>().HasData(
                    new Office { OfficeId = Guid.NewGuid().ToString(), Number = 1 },
                    new Office { OfficeId = Guid.NewGuid().ToString(), Number = 2 });

            modelBuilder.Entity<Subject>().HasData(
                    new Subject { SubjectId = Guid.NewGuid().ToString(), Name = "Математика" },
                    new Subject { SubjectId = Guid.NewGuid().ToString(), Name = "Русский язык" });
        }
    }
}
