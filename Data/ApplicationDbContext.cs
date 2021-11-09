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
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        //public DbSet<OrderItem> orderItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Store> Stores { get; set; }

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

            modelBuilder.Entity<Customer>(b =>
            {
                b.HasKey("CustomerID");

                b.Property<string>("FirstName")
                    .HasColumnType("nvarchar(50)");
                b.Property<string>("LastName")
                    .HasColumnType("nvarchar(50)");
                b.Property<string>("Phon")
                    .HasColumnType("nvarchar(30)");
                b.Property<string>("Email")
                    .HasColumnType("nvarchar(30)");
                b.Property<string>("Street")
                    .HasColumnType("nvarchar(100)");
                b.Property<string>("City")
                    .HasColumnType("nvarchar(30)");
                b.Property<string>("ZipCode")
                    .HasColumnType("nvarchar(20)");

            });
            modelBuilder.Entity<Order>(b =>
            {
                b.HasKey("OrderID");

                b.Property<int>("CustomerID")
                .HasColumnType("int");
                b.Property<int>("OrderStatus")
                .HasColumnType("int");
                b.Property<DateTime>("OrderDate")
                .HasColumnType("DateTime");
                b.Property<DateTime>("ShippingDate")
                .HasColumnType("DateTime");
                b.Property<int>("StoreID")
                .HasColumnType("int");

                b.HasOne(or => or.Customer)
                .WithMany(d => d.Orders)
                .HasForeignKey(or => or.CustomerID)
                .OnDelete(DeleteBehavior.Cascade);

                b.HasOne(or => or.Store)
               .WithMany(d => d.Orders)
               .HasForeignKey(or => or.StoreID)
               .OnDelete(DeleteBehavior.Cascade);

                b.HasOne(or => or.Staff)
               .WithMany(d => d.Orders)
               .HasForeignKey(or => or.StaffID)
               .OnDelete(DeleteBehavior.Cascade);



            });

            modelBuilder.Entity<Stock>(b =>
            {
                b.HasKey("ProductID");

                //b.Property<int>("StoreID")
                //.HasColumnType("int");
                //b.Property<int>("ProductID")
                //    .HasColumnType("int");

                //b.HasOne(sto => sto.Product)
                //.WithOne(d => d.Stocks)
                //.HasForeignKey(sto => sto.ProductID)
                //.OnDelete(DeleteBehavior.Cascade);
            });

        }
    }
}
