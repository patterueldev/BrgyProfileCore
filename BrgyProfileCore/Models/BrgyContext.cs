using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.IO;

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
        public DbSet<Sitio> SitioList { get; set; }
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
    }
}
