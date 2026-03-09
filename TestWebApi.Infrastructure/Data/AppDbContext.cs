using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestWebApi.Domain.Entities;
using TestWebApi.Application.DTOs;
namespace TestWebApi.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Product> Products => Set<Product>();
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<CustomerListDto> CustomerList { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Branch> Branches => Set<Branch>();
        protected override void OnModelCreating(ModelBuilder builder)
        {
           
            base.OnModelCreating(builder); // optional, but safe
            builder.Entity<CustomerListDto>().HasNoKey();

            // You can still customize specific entities if needed
            builder.Entity<Product>(entity =>
            {
                entity.Property(p => p.Price).HasColumnType("numeric(18,2)");
            });

            // Master - Detail Relationship
            builder.Entity<Order>()
            .HasMany(o => o.Items)
            .WithOne(i => i.Order)
            .HasForeignKey(i => i.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

            // Optional: configure table and column names explicitly (already mapped via attributes)
            builder.Entity<Order>().ToTable("orders");
            builder.Entity<OrderItem>().ToTable("order_items");
        }
    }
}
