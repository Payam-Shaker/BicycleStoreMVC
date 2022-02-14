using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BicycleStore.Domain.Models
{
    public class Brand
    {
        public Brand()
        {
                
        }
        //[Key]
        public int BrandID { get; set; }
        public string BrandName { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
