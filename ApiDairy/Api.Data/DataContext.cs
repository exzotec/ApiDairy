using ApiDairy.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace ApiDairy.Data
{
    public class DataContext : DbContext
    {
        public DbSet<User> users { get; set; }
        public DbSet<Role> roles { get; set; }

        public DbSet<Class> Classes { get; set; }
        public DbSet<Office> Offices { get; set; }
        public DbSet<Timetable> Timetables { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Mark> Marks { get; set; }
        public DbSet<Hometask> Hometasks { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Api");
            base.OnModelCreating(modelBuilder);
        }
    }
}
