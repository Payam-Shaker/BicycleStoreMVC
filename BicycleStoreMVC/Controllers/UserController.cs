using BicycleStoreMVC.Data;
using BicycleStore.Domain.DTOs;
using BicycleStoreMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Security.Claims;
using BicycleStore.Domain.Models;
using Microsoft.EntityFrameworkCore;
using BicycleStoreMVC.Services;

namespace BicycleStoreMVC.Controllers
{
    /// <summary>
    /// add later a new method for giving star to the User as a test 
    /// </summary>
    public class UserController : Controller
    {
        private readonly BicycleStoreDbContext _context;
        private readonly IUserService _userService;
        public UserController(BicycleStoreDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register(CustomerDto customerDto)
        {
            //check model validity
            if (ModelState.IsValid)
            {
                try
                {
                    //TODO: FIx UserAlreadyExists()
                    if (UserExists(customerDto.Email))
                    {
                        throw new Exception();
                    }
                    //create password salt passwordhash
                    byte[] passwordHash, passwordSalt;
                    _userService.CreatePasswordHash(customerDto.Password, out passwordHash, out passwordSalt);
                    //convert customerDto to customer 
                    var obj = new Customer
                    {
                        FirstName = customerDto.FirstName,
                        LastName = customerDto.LastName,
                        PasswordHash = passwordHash,
                        PasswordSalt = passwordSalt,
                        Created = DateTime.Now,
                        Email = customerDto.Email,
                        Street = customerDto.Street,
                        City = customerDto.City,
                        ZipCode = customerDto.ZipCode,
                        Role = 2 //1 is reserved for admin role which can be modfied manually. All new users are non-admins then.
                    };
                    //save to database
                    _context.Add(obj);
                    _context.SaveChanges();
                    //to clear the form
                    ModelState.Clear();
                    //success msg
                    return PartialView("_RegisterSuccessful");
                }
                catch (Exception ex)
                {
                    //TODO> fix the reg. form view to show pop-up error message
                    throw new Exception(ex.Message);
                }
            }
            else
            {
                return View("Index");
            }
        }
        public bool UserExists(string username)
        {
            if (_context.Customers.Any(x => x.Email.ToLower() == username.ToLower()))
            {
                return true;
            }
            return false;
        }

        public IActionResult LoginView()
        {
            return View("Login");
        }

        public IActionResult Login(CustomerLoginDto customerLoginDto)
        {
            if (string.IsNullOrEmpty(customerLoginDto.UserName) || string.IsNullOrEmpty(customerLoginDto.Password))
                return null;
            //TODO: Find user by email first
            //check whether password is correct...
            var user = _context.Customers.Where(c => c.Email.ToLower() == customerLoginDto.UserName.ToLower()).FirstOrDefault();
            if (!_userService.VerifyPasswordHash(customerLoginDto.Password, user.PasswordHash, user.PasswordSalt))
                return null;
            TempData["currentUser"] = user.FirstName;

            if (user.Role == 1)
            {
                return RedirectToAction("Index", "Products");
            }

            //authentication is successful - return to home page with user name displayed on the navbar
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Logout()
        {
            TempData["currentUser"] = null;
            return RedirectToAction("Index", "Home");
        }

        public Customer FindByEmail(string userName)
        {
            var user = _context.Customers.FirstOrDefault(p => p.Email == userName);
            if (user != null)
            {
                return user;
            }
            else
            {
                throw new Exception($"User not found!");
            }
        }
    }
}