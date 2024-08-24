using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using major_delivery_manager.Models;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace major_delivery_manager.AppDbContext
{
    internal class ApplicationContext : DbContext
    {
        public DbSet<RequestModel> Requests { get; set; } = null!;
        public DbSet<CourierModel> Couriers { get; set; } = null!;
        public DbSet<RequestCancellationModel> Cancellations { get; set; } = null!;

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            optionsBuilder.UseSqlite(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RequestModel>()
                .HasOne(m => m.CancellationModel)
                .WithOne(r => r.Request)
                .HasForeignKey<RequestCancellationModel>(r => r.RequestModelId);

            modelBuilder.Entity<CourierModel>()
                .HasMany(m => m.RequestModels)
                .WithOne(r => r.CourierModel)
                .HasForeignKey(r => r.ResponsibleId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
