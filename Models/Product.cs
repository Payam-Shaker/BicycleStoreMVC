using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BicycleStoreMVC.Models
{
    public class Product
    {
        //public Product()
        //{

        //}
        //[Key]
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int ModelYear { get; set; }
        public decimal ListPrice { get; set; }
        public Category Category { get; set; }
        //[ForeignKey ("Category")]
        public int CategoryID { get; set; }
        public Brand Brand { get; set; }
        //[ForeignKey("Brand")]
        public int BrandID { get; set; }
        //public ICollection<OrderItem> OrderItems { get; set; }
        public Stock Stocks { get; set; }

    }
}
