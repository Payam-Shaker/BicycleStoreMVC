using BicycleStore.Domain.Models;
using BicycleStoreMVC.Data;
using BicycleStoreMVC.Models;
using BicycleStoreMVC.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BicycleStoreMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly BicycleStoreDbContext _context;
        private readonly ICrud<Product> _proRepo;
        private readonly ICrud<Brand> _brandRepo;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, BicycleStoreDbContext context, ICrud<Product> proRepo, ICrud<Brand> brandRepo)
        {
            _logger = logger;
            _context = context;
            _proRepo = proRepo;
            _brandRepo = brandRepo;

        }


        public IActionResult Index(string sortOrder, string searchString)
        {
            //IEnumerable<Brand> result = new List<Brand>();
            //var products = _crud.GetAll();
            ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.PriceSortParm = string.IsNullOrEmpty(sortOrder) ? "price_desc" : "";

            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            var products = _proRepo.GetAll().Include(p => p.Brand).Include(p=>p.Category).AsQueryable();
            if (!string.IsNullOrEmpty(searchString))
            {
                 products = products.Where(s => s.ProductName.Contains(searchString));
                                       //|| s.FirstMidName.Contains(searchString))                   
            }
            switch (sortOrder)
            {
                case "name_desc":
                    products = products.OrderByDescending(s => s.ProductName);
                    break;
                case "price_desc":
                    products = products.OrderByDescending(s => s.ListPrice);
                    break;
                //case "Date":
                //    products = products.OrderBy(s => s.);
                //    break;
                //case "date_desc":
                //    products = products.OrderByDescending(s => s.EnrollmentDate);
                //    break;
                default:
                    products = products.OrderBy(s => s.ProductName);
                    break;
            }
            return View(products);
        }
        //public PartialViewResult Products()
        //{
        //    return PartialView("_searchProducts");
        //}

        [Authorize]
        public IActionResult ConfidentialData()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
