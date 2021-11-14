using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BicycleStoreMVC.Controllers
{
    /// <summary>
    /// add later a new method for giving star to the User as a test 
    /// </summary>
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
