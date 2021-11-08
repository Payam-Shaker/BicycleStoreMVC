using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BicycleStoreMVC.Models
{
    public class Brand
    {
        public Brand()
        {
                
        }

        public int BrandID { get; set; }
        public string BrandName { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
