using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BicycleStore.Domain.Models
{
    public class Stock
    {
        public Stock()
        {
                
        }

        public Store Store { get; set; }
        //[ForeignKey("Store")]
        public int StoreID { get; set; }
        public Product Product { get; set; }
        //[ForeignKey("Product")]
        public int ProductID { get; set; }
        public int Quantity { get; set; }
    }
}
