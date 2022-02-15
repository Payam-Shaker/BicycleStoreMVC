using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BicycleStore.Domain.Models
{
    //[Table("Brand", Schema = "Production")]

    public class Brand
    {
        public Brand()
        {
            Products = new HashSet<Product>();   
        }
        [Key]
        public int BrandID { get; set; }
        public string BrandName { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        //public virtual Product Products { get; set; }


    }
}
