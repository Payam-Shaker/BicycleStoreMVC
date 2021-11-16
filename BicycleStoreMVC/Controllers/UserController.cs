using BicycleStoreMVC.Data;
using BicycleStoreMVC.DTOs;
using BicycleStoreMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Security.Claims;

namespace BicycleStoreMVC.Controllers
{
    /// <summary>
    /// add later a new method for giving star to the User as a test 
    /// </summary>
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        public UserController(ApplicationDbContext context)
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
                        CreatedDate = DateTime.Now,
                        Email = customerDto.Email,
                        Street = customerDto.Street,
                        City = customerDto.City,
                        ZipCode = customerDto.ZipCode
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

                    throw new Exception(ex.Message);
                }



            }
            else
            {
                return View("Index");

            }
        }

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
            //check for username 
            var user = FindByEmail(customerLoginDto.UserName);

            if (user == null)
            {
                return null;
            }
            //check whether password is correct...
            if (!VerifyPasswordHash(customerLoginDto.Password, user.PasswordHash, user.PasswordSalt))
                return null;

            //authentication is successful
            return PartialView("_LoginSuccessfull");
        }

       // [HttpPost]
        //public IActionResult Authenticate(CustomerLoginDto customerLoginDto)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View("Index");
        //    }
        //    else
        //    {
        //        try
        //        {
        //            var user = Login(customerLoginDto.UserName);

        //            if (user == null)
        //            {
        //                ModelState.AddModelError(" ", "Username or Password is wrong!");
        //                return View("Login");
        //            }
        //            var claims = new List<Claim>()
        //            {
        //                new Claim(ClaimTypes.NameIdentifier, Convert.ToString(user.UserName)),
        //                new Claim(ClaimTypes.Name, user.)
        //            };
        //        }
        //        catch (Exception ex)
        //        {

        //            throw new Exception($"Login failed!", ex);
        //        }
        //    }
           
        //}

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