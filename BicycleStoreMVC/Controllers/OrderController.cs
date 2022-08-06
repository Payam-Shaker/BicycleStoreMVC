using BicycleStore.Domain.Models;
using BicycleStoreMVC.Data;
using BicycleStoreMVC.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using BicycleStoreMVC.Helpers;
using Microsoft.EntityFrameworkCore;

namespace BicycleStoreMVC.Controllers
{
    public class OrderController : Controller
    {
        private readonly BicycleStoreDbContext _context;
        private readonly ICrud<Order> _orderRepo;
        private readonly ICrud<Product> _proRepo;

        public List<OrderItem> orderItem { get; set; }
        public decimal? Total { get; set; }


        public OrderController(BicycleStoreDbContext context, ICrud<Order> orderRepo, ICrud<Product> proRepo)
        {
            _context = context;
            _orderRepo = orderRepo;
            _proRepo = proRepo;

        }
        public IActionResult AddToCart(int ProductID)
        {
            var order = new List<OrderItem>();
            var product = _proRepo.GetById(ProductID);
            if (product != null)
            {
                order.Add(new OrderItem()
                {
                    Product = product,
                    Quantity = 1
                });
                HttpContext.Session.SetObjectAsJson("order", order);
            }


            return View();
        }
        // GET: Order
        public IActionResult Index()
        {
            orderItem = HttpContext.Session.GetObjectFromJson<List<OrderItem>>("order");
            Total = orderItem.Sum(i => i.Product.ListPrice * i.Quantity);
            return View(orderItem);
        }
        // GET: Order/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.OrderID == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Cart/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cart/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderID")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: Cart/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            orderItem = HttpContext.Session.GetObjectFromJson<List<OrderItem>>("order");
            var itemToBeEdited = orderItem.Where(ci => ci.Product.ProductID == id).FirstOrDefault();
            if (!orderItem.Any())
            {
                return NotFound();
            }
            return View(itemToBeEdited);
        }

        // POST: Cart/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, int quantity)
        {
            orderItem = HttpContext.Session.GetObjectFromJson<List<OrderItem>>("order");
            var itemToBeEdited = orderItem.Where(ci => ci.Product.ProductID == id).FirstOrDefault();

            if (id != itemToBeEdited.Product.ProductID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    itemToBeEdited.Quantity = quantity;
                    if (quantity <= 0)
                    {
                        //redirect to delet endpoint
                    }
                    HttpContext.Session.SetObjectAsJson("order", orderItem);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return View("Edit");
        }

        // GET: Cart/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            orderItem = HttpContext.Session.GetObjectFromJson<List<OrderItem>>("order");
            var itemToBeDeleted = orderItem.Where(ci => ci.Product.ProductID == id).FirstOrDefault();
            if (!orderItem.Any())
            {
                return NotFound();
            }
            //ViewBag.DeleteSuccess = null;

            return View(itemToBeDeleted);

        }

        // POST: Cart/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            orderItem = HttpContext.Session.GetObjectFromJson<List<OrderItem>>("order");
            var itemToBeDeleted = orderItem.Where(ci => ci.Product.ProductID == id).FirstOrDefault();
            var itemName = itemToBeDeleted.Product.ProductName;
            orderItem.Remove(itemToBeDeleted);
            HttpContext.Session.SetObjectAsJson("order", orderItem);
            return View("Delete");
        }

        private bool CartExists(int id)
        {
            return _context.Orders.Any(e => e.OrderID == id);
        }
    }

}
