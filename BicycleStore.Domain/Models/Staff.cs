using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BicycleStore.Domain.Models
{
    public class Staff
    {
        public Staff()
        {
                
        }
        [Key]
        public int StaffID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        //[ForeignKey("Store_Id")]
        //public virtual Store Store { get; set; }
        //[ForeignKey("Store")]
        //public int StoreID { get; set; }
        //public int? ManagerID { get; set; }
        public ICollection<Order> Orders { get; set; }

    }
}
