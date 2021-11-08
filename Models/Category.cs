using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BicycleStoreMVC.Models
{
    public class Category
    {
        //public Category()
        //{

        //}
        [Key]
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
