using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BicycleStoreMVC.DTOs
{
    public class CustomerDto
    {
        //public int CustomerID { get; set; }
        [Required(ErrorMessage ="You cannot leave first name empty!")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "You cannot leave last name empty!")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "You cannot leave email adress empty!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "You cannot leave password empty!")]
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }



    }
}
