using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BicycleStoreMVC.Models
{
    public class Order
    {
        public Order()
        {
                
        }

        public int OrderID { get; set; }
        public Customer Customer { get; set; }
        public int CustomerID { get; set; }
        public bool OrderStatus { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ShippingDate { get; set; }
        public Store Store { get; set; }
        public int StoreID { get; set; }
        public Staff Staff { get; set; }
        public int StaffID { get; set; }
    }
}
