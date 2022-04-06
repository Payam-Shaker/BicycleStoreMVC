using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BicycleStore.Domain.Models
{
    public class Product
    {
        public Product()
        {
            //Brand = new Brand();
        }
        //[Key]
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int ModelYear { get; set; }
        public decimal ListPrice { get; set; }
        //[ForeignKey("Category_Id")]
        public Category Category { get; set; }
        //[ForeignKey ("Category")]
        public int CategoryID { get; set; }
        [ForeignKey("Brand_Id")]
        public virtual Brand Brand { get; set; }
        public int BrandID { get; set; }
        public Stock Stocks { get; set; }
        public OrderItem OrderItems { get; set; }
        public string Image { get; set; }

    }
}
