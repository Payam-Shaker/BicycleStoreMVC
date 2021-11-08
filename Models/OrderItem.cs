using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BicycleStoreMVC.Models
{
    public class OrderItem
    {
        public OrderItem()
        {
                
        }

        public int ItemID { get; set; }
        public Product Product { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public decimal ListPrice { get; set; }
        public decimal Discount { get; set; }
        public Order Order { get; set; }
        public int OrderID { get; set; }
    }
}
