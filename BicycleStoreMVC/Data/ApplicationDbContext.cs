using BicycleStoreMVC.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using BicycleStore.Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;

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
        public DbSet<OrderItem> orderItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Store> Stores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(e =>
            {
                e.HasKey(e => e.ProductID)
                .HasName ("Product_Id");

                e.ToTable("Product", "Production");

                e.Property(e => e.ProductName)
                .IsRequired (true)
                .HasColumnName("Product_Name")
                .HasColumnType("VARCHAR(50)");

                e.Property(e => e.ModelYear)
                .IsRequired(true)
                .HasColumnName("Product_Year")
                .HasColumnType("SMALLINT");

                e.Property(e => e.ListPrice)
                .IsRequired(true)
                .HasColumnName("Product_Price")
                .HasColumnType("DECIMAL(10,2)");

                e.Property(e => e.Image)
                .HasColumnName("Image")
                .HasColumnType("varchar(500)");

                e.Property(e => e.CategoryID)
                .IsRequired(true)
                .HasColumnName("Category_Id")
                .HasColumnType("INT");

                e.Property(e => e.BrandID)
                .IsRequired(true)
                .HasColumnName("Brand_Id")
                .HasColumnType("INT");
                
                e.HasOne(prod => prod.Category)
                .WithMany(d => d.Products)
                .HasForeignKey(prod => prod.CategoryID)
                .OnDelete(DeleteBehavior.Cascade);

                e.HasOne(prod => prod.Brand)
                .WithMany(d => d.Products)
                .HasForeignKey(prod => prod.BrandID)
                .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Brand>(e =>
            {
                e.ToTable("Brand", "Production");

                e.HasKey(e => e.BrandID)
                .HasName("Brand_Id");


                e.Property(e => e.BrandName)
                .IsRequired(true)
                .HasColumnName("Brand_Name")
                .HasColumnType("VARCHAR(50)");
            });

            modelBuilder.Entity<Category>(e =>
            {
                e.HasKey(e => e.CategoryID)
                .HasName("Category_Id");

                e.ToTable("Category", "Production");

                e.Property(e => e.CategoryName)
                .IsRequired(true)
                .HasColumnName("Category_Name")
                .HasColumnType("VARCHAR(50)");
            });


            modelBuilder.Entity<Customer>(e =>
            {
                e.HasKey(e => e.CustomerID)
                .HasName("Customer_Id");

                e.ToTable("Customer", "Sales");

                e.Property(e => e.FirstName)
                .IsRequired(true)
                .HasColumnName("Customer_FirstName")
                .HasColumnType("varchar(255)");

                e.Property(e => e.LastName)
                .IsRequired(true)
                .HasColumnName("Customer_LastName")
                .HasColumnType("varchar(255)");

                e.Property(e => e.Email)
                .IsRequired(true)
                .HasColumnName("Customer_Email")
                .HasColumnType("varchar(255)");

                e.Property(e => e.Phone)
                .HasColumnName("Customer_Phone")
                .HasColumnType("varchar(50)");

                e.Property(e => e.Street)
                .HasColumnName("Customer_Street")
                .HasColumnType("varchar(255)");

                e.Property(e => e.City)
                .HasColumnName("Customer_City")
                .HasColumnType("varchar(255)");


                e.Property(e => e.ZipCode)
                .HasColumnName("Customer_ZipCode")
                .HasColumnType("varchar(50)");

                e.Property(e => e.Role)
                .HasColumnName("Role")
                .HasColumnType("int");
            });

            modelBuilder.Entity<Order>(e =>
            {
                e.HasKey(e => e.OrderID)
                .HasName("Order_Id");

                e.ToTable("Order", "Sales");

                e.Property(e => e.OrderStatus)
                .IsRequired(true)
                .HasColumnName("Order_Status")
                .HasColumnType("SMALLINT");

                e.Property(e => e.OrderDate)
                .IsRequired(true)
                .HasColumnName("Order_Date")
                .HasColumnType("DATE");

                e.Property(e => e.OrderRequiredDate)
                .IsRequired(true)
                .HasColumnName("Order_Required_Date")
                .HasColumnType("DATE");

                e.Property(e => e.ShippingDate)
                .HasColumnName("Order_Shipped_Date")
                .HasColumnType("DATE");

                e.Property(e => e.CustomerID)
                .HasColumnName("Customer_Id")
                .HasColumnType("INT");

                e.Property(e => e.StoreID)
                .IsRequired(true)
                .HasColumnName("Store_Id")
                .HasColumnType("INT");

                e.Property(e => e.StaffID)
                .IsRequired(true)
                .HasColumnName("Staff_Id")
                .HasColumnType("INT");


                e.HasOne(or => or.Customer)
                .WithMany(d => d.Orders)
                .HasForeignKey(or => or.CustomerID)
                .HasConstraintName("Customer_Id")
                .OnDelete(DeleteBehavior.Cascade);

                e.HasOne(or => or.Store)
               .WithMany(d => d.Orders)
               .HasForeignKey(or => or.StoreID)
                .HasConstraintName("Store_Id")
               .OnDelete(DeleteBehavior.Cascade);

                e.HasOne(or => or.Staff)
               .WithMany(d => d.Orders)
               .HasForeignKey(or => or.StaffID)
               .HasConstraintName("Staff_Id")
               .OnDelete(DeleteBehavior.Cascade);



            });

            modelBuilder.Entity<Stock>(e =>
            {
                //e.HasNoKey();
                //e.HasAlternateKey(e => e.StoreID);
                e.ToTable("Stock", "Production");

                e.Property(e => e.StoreID)
                .HasColumnName("Store_Id")
                .HasColumnType("INT");

                e.Property(e => e.ProductID)
                .HasColumnName("Product_Id")
                .HasColumnType("INT");


                e.Property(e => e.Quantity)
                .HasColumnName("Stock_Quantity")
                .HasColumnType("INT");


                e.HasOne(sto => sto.Product)
                .WithOne(d => d.Stocks)
                //.HasConstraintName("FK_Product_Id")
                //.HasForeignKey("FK__Stock__Product_I__3A4CA8FD")
                .OnDelete(DeleteBehavior.Cascade);

                e.HasOne(sto => sto.Store)
                .WithOne(d => d.Stocks)
                //.HasForeignKey("FK__Stock__Store_Id__395884C4")
                .OnDelete(DeleteBehavior.Cascade);

            });

            modelBuilder.Entity<OrderItem>(e =>
            {
                e.HasKey(e => new { e.OrderID, e.ItemID });

                e.ToTable("Order_Item", "Sales");

                e.Property(e => e.ProductID)
                .IsRequired(true)
                .HasColumnName("Product_Id")
                .HasColumnType("INT");

                e.Property(e => e.Quantity)
                .IsRequired(true)
                .HasColumnName("Item_Quantity")
                .HasColumnType("INT");

                e.Property(e => e.ListPrice)
                .IsRequired(true)
                .HasColumnName("List_Price")
                .HasColumnType("DECIMAL (10,2)");

                e.Property(e => e.Discount)
                .IsRequired(true)
                .HasColumnName("Discount")
                .HasColumnType("DECIMAL (4,2)");

                e.HasOne(or => or.Order)
                .WithOne(d => d.OrderItems)
                //.HasConstraintName("FK_Order_Id")
                //.HasForeignKey("FK__Order_Ite__Order__367C1819")
                .OnDelete(DeleteBehavior.Cascade);

                e.HasOne(or => or.Product)
                .WithOne(d => d.OrderItems)
               //.HasConstraintName("FK_Product_Id")
                //.HasForeignKey("FK__Order_Ite__Produ__37703C52")
                .OnDelete(DeleteBehavior.Cascade);

            });
            modelBuilder.Entity<Store>(e =>
            {
                e.HasKey(e => e.StoreID)
                .HasName("Store_Id");

                e.ToTable("Store", "Sales");

                e.Property(e => e.StoreName)
                .HasColumnName("Store_Name")
                .HasColumnType("VARCHAR(255)");

                e.Property(e => e.StorePhone)
                .HasColumnName("Store_Phon")
                .HasColumnType("VARCHAR(50)");

                e.Property(e => e.StoreEmail)
                .HasColumnName("Store_Email")
                .HasColumnType("VARCHAR(255)");

                e.Property(e => e.Street)
                .HasColumnName("Store_Street")
                .HasColumnType("VARCHAR(255)");

                e.Property(e => e.City)
                .HasColumnName("Store_City")
                .HasColumnType("VARCHAR(255)");

                e.Property(e => e.ZipCode)
                .HasColumnName("Store_ZipCode")
                .HasColumnType("VARCHAR(50)");


            });

            modelBuilder.Entity<Staff>(e =>
            {
                e.HasKey(e => e.StaffID)
                .HasName("Staff_Id");

                e.ToTable("Staff", "Sales");

                e.Property(e => e.FirstName)
                .IsRequired(true)
                .HasColumnName("Staff_FirstName")
                .HasColumnType("VARCHAR(255)");

                e.Property(e => e.LastName)
                .IsRequired(true)
                .HasColumnName("Staff_LastName")
                .HasColumnType("VARCHAR(255)");

                e.Property(e => e.Email)
                .IsRequired(true)
                .HasColumnName("Staff_Email")
                .HasColumnType("VARCHAR(255)");

                e.Property(e => e.Phone)
                .IsRequired(true)
                .HasColumnName("Staff_Phone")
                .HasColumnType("VARCHAR(50)");

                //e.Property(e => e.StoreID)
                //.IsRequired(true)
                //.HasColumnName("Store_Id")
                //.HasColumnType("INT");

                //e.HasOne(stf => stf.Store)
                //.WithMany(d => d.Staffs)
                //.HasForeignKey(stf => stf.StoreID)
                //.HasConstraintName("Store_Id")
                //.OnDelete(DeleteBehavior.Cascade);

            });
        } 
    }
}
