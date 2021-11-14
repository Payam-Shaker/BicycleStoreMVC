using BicycleStoreMVC.Data;
using BicycleStoreMVC.DTOs;
using BicycleStoreMVC.Models;
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

        public IActionResult Login(CustomerDto customerDto)
        {

        }
    }
}
