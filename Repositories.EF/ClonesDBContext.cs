using Data.EF.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.EF
{
    public class ClonesDbContext : DbContext
    {
        private readonly string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\projects\\MyClonesData\\MyClones.mdf;Integrated Security=True;Connect Timeout=30";
        //private readonly string connectionString = "Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=MyClones;Integrated Security=True;";


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(connectionString);
        }

        public DbSet<FieldsData> Fields { get; set; }
    }
}
