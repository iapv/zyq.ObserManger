using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zyq.ObserManger.Models;

namespace zyq.ObserManger.Data
{
    public class AppDbContext : DbContext
    {


        public DbSet<ProductionHourly> ProductionHourlies { get; set; }
        public DbSet<QualityHistory> QualityHistories { get; set; }


        public AppDbContext()
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=production.db");
            }
        }
    }


}
