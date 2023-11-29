using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework16.Models
{
    internal class SQLiteContext:DbContext
    {
        public DbSet<Purchase> Purchases { get; set; } = null!;
        //public ApplicationContext() => Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=purchasesDb.db");
        }
    }
}
