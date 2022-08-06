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

namespace BicycleStoreMVC.Controllers
{
    /// <summary>
    /// add later a new method for giving star to the User as a test 
    /// </summary>
    public class UserController : Controller
    {
        private readonly BicycleStoreDbContext _context;
        public UserController(BicycleStoreDbContext context)
        {
            _context = context;
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

                    //create password salt passwordhash
                    byte[] passwordHash, passwordSalt;
                    CreatePasswordHash(customerDto.Password, out passwordHash, out passwordSalt);
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
        //public async Task<bool> UserExists(string username)
        //{
        //    if (await _context.Customers.AnyAsync(x => x.FirstName.ToLower() == username.ToLower()))
        //    {
        //        return true;
        //    }
        //    return false;
        //}

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
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
            if (!VerifyPasswordHash(customerLoginDto.Password, user.PasswordHash, user.PasswordSalt))
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
        public bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }
}