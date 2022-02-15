using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BicycleStore.Domain.Models
{
    public class OrderItem
    {
        public OrderItem()
        {
                
        }
        [Key]
        public int ItemID { get; set; }
        public int OrderID { get; set; }

        public Product Product { get; set; }
        //[ForeignKey("Product")]
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public decimal ListPrice { get; set; }
        public decimal Discount { get; set; }
        public Order Order { get; set; }
    }
}
