using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BicycleStoreMVC.Models
{
    public class Staff
    {
        public Staff()
        {
                
        }

        public int StaffID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Store Store { get; set; }
        public int StoreID { get; set; }
        public ICollection<Staff> Staffs { get; set; }
        public int? ManagerID { get; set; }
        public ICollection<Order> Orders { get; set; }

    }
}
