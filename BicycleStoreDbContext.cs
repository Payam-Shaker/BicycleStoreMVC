using BicycleStoreMVC.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BicycleStoreMVC
{
    public class BicycleStoreDbContext : DbContext
    {
        public BicycleStoreDbContext(DbContextOptions<BicycleStoreDbContext> options) : base(options)
        {
                
        }

        public DbSet<Brand> Brands  { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> orderItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Store> Stores { get; set; }

    }
}
