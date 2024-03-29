﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BicycleStore.Domain.Models
{
    public class Store
    {
        public Store()
        {
            Orders = new HashSet<Order>();
            Staffs = new HashSet<Staff>();


        }
        [Key]
        public int StoreID { get; set; }
        public string StoreName { get; set; }
        public string StorePhone { get; set; }
        public string StoreEmail { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Staff> Staffs { get; set; }

        public Stock Stocks { get; set; }



    }
}
