using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BicycleStoreMVC.Models
{
    public class Order
    {
        public Order()
        {
                
        }
        [Key]
        public int OrderID { get; set; }
        public Customer Customer { get; set; }
        [ForeignKey ("Customer")]
        public int CustomerID { get; set; }
        public bool OrderStatus { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ShippingDate { get; set; }
        public Store Store { get; set; }
        [ForeignKey("Store")]
        public int StoreID { get; set; }
        public Staff Staff { get; set; }
        [ForeignKey("Staff")]
        public int StaffID { get; set; }
    }
}
