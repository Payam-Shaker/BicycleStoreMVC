using BicycleStoreMVC.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BicycleStoreMVC.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        //public DbSet<Customer> Customers { get; set; }
        //public DbSet<Order> Orders { get; set; }
        //public DbSet<OrderItem> orderItems { get; set; }
        public DbSet<Product> Products { get; set; }
        //public DbSet<Staff> Staffs { get; set; }
        //public DbSet<Stock> Stocks { get; set; }
        //public DbSet<Store> Stores { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(b =>
            {
                b.HasKey("ProductID");
                b.Property<string>("ProductName")
                    .HasColumnType("nvarchar(50)");

                b.Property<int>("ModelYear")
                    //.IsConcurrencyToken()
                    .HasColumnType("int");

                b.Property<decimal>("ListPrice")
                    .HasColumnType("DECIMAL(20,2)");
                //.HasMaxLength(256);

                b.Property<int>("CategoryID")
                    .HasColumnType("int");
                //.HasMaxLength(256);
                b.Property<int>("BrandID")
                    .HasColumnType("int");

                //b.ToTable("AspNetRoles");
               // modelBuilder.Entity<Post>()
                b.HasOne(prod => prod.Category)
                .WithMany(d => d.Products)
                .HasForeignKey(prod => prod.CategoryID)
                .OnDelete(DeleteBehavior.Cascade);

                b.HasOne(prod => prod.Brand)
                .WithMany(d => d.Products)
                .HasForeignKey(prod => prod.BrandID)
                .OnDelete(DeleteBehavior.Cascade);



            });

        }
    }
}
