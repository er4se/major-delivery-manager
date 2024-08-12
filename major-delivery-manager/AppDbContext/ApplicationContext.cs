using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using major_delivery_manager.Models;
using Microsoft.EntityFrameworkCore;

namespace major_delivery_manager.AppDbContext
{
    internal class ApplicationContext : DbContext
    {
        public DbSet<RequestModel> Requests { get; set; } = null!;
        public DbSet<CourierModel> Couriers { get; set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=major-delivery-database-test.db");
        }
    }
}
