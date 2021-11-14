using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BicycleStoreMVC.DTOs
{
    public class CustomerLoginDto
    {
        [Required(ErrorMessage = "Please enter your user name!")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Please enter your password!")]
        public string Password { get; set; }
    }
}
