using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Xamarin.Forms;

namespace MobileSample.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string databaseFileName = "GestionDesEmployee.sqlite";
            string databaseFilePath = null;
            if (Device.RuntimePlatform == "Android")
            {
                databaseFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), databaseFileName);
            }
            else if (Device.RuntimePlatform == "iOS")
            {
                databaseFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "..", "Library", databaseFileName);
            }
            else
            {
                throw new NotImplementedException(Device.RuntimePlatform);
            }
            if (string.IsNullOrEmpty(databaseFilePath)) throw new NotImplementedException();

            optionsBuilder.UseSqlite($"Filename={databaseFilePath}");
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Optional: Add specific configurations for the User entity
            modelBuilder.Entity<User>().ToTable("Users");
        }
    }
}
