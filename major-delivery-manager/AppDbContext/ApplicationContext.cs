using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using major_delivery_manager.Models;
using Microsoft.EntityFrameworkCore;

namespace major_delivery_manager.AppDbContext
{
    public class ApplicationContext : DbContext
    {
        public DbSet<DeliveryRequestModel> Requests { get; set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=major-delivery-database-test.db");
        }
    }
}
