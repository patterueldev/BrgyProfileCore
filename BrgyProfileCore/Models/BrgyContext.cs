using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.IO;
using PasswordHashing;

namespace BrgyProfileCore
{
    struct DBDataSource
    {
        public string DBPath;
        public string GetConnectionString()
        {
            return "Data Source=" + DBPath;
        }
    }
    public class BrgyContext : DbContext
    {
        public DbSet<Resident> Residents { get; set; }
        public DbSet<Household> Households { get; set; }
        public DbSet<Sitio> Sitio { get; set; }
        public DbSet<User> Users { get; set; }
        private DBDataSource dataSource
        {
            get
            {
                string Fname = "BrgyProfileCore\\app.db";
                string folderPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                string dbPath = Path.Combine(folderPath, Fname);
                return new DBDataSource
                {
                    DBPath = dbPath
                };
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(dataSource.GetConnectionString());
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Func<string> hashedPassword = () =>
            {
                var password = "password";

                //Use Blake2b algorithm with saltsize of 20
                var passwordHasher = PasswordHasherInstance.Create(HashAlgorithm.Blake2b, 20);
                string hashedPassword = passwordHasher.Hash(password); //AED9BF19B9D5DEB3A...

                return hashedPassword;
            };
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    Username = "admin",
                    Password = hashedPassword(),
                    Role = "Administrator",
                    Name = "Admin"
                }
            );
        }
    }
}
