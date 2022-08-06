using BicycleStore.Domain.Models;
using BicycleStoreMVC.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace BicycleStore.Data
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            
            using (var context = new BicycleStoreDbContext(
            serviceProvider.GetRequiredService<
            DbContextOptions<BicycleStoreDbContext>>()))
            {
                context.Database.EnsureCreated();
                //if database is already seeded
                if (context.Products.Any())
                {
                    return; 
                }

                var category1 = new Category { CategoryName = "Race" };
                var category2 = new Category { CategoryName = "City" };
                var category3 = new Category { CategoryName = "Hybrid" };
                if(!context.Categories.Any())
                    context.Categories.AddRange(category1, category2, category3);

                var brand1 = new Brand { BrandName = "Nishiki" };
                var brand2 = new Brand { BrandName = "Trek" };
                var brand3 = new Brand { BrandName = "Monark" };
                var brand4 = new Brand { BrandName = "Bianchi" };
                var brand5 = new Brand { BrandName = "Moderna" };

                if (!context.Brands.Any())
                    context.Brands.AddRange(brand1, brand2, brand3, brand4, brand5);

                var product1 = new Product
                {
                    ProductName = "T120",
                    ModelYear = 2019,
                    ListPrice = 5000.00m,
                    Category = category2,
                    Brand = brand3,

                };

                var product2 = new Product
                {
                    ProductName = "KARL",
                    ModelYear = 2021,
                    ListPrice = 35000.00m,
                    Category = category1,
                    Brand = brand5,

                };

                var product3 = new Product
                {
                    ProductName = "Touring Master",
                    ModelYear = 2009,
                    ListPrice = 8000.00m,
                    Category = category3,
                    Brand = brand5,

                };
                if (!context.Products.Any())
                    context.Products.AddRange(product1, product2, product3);

                context.SaveChanges();

            }
        }
    }
}
