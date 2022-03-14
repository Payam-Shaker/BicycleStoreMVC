﻿using BicycleStore.Domain.Models;
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

namespace BicycleStoreMVC.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICrud<Order> _orderRepo;
        private readonly ICrud<Product> _proRepo;

        public List<OrderItem> orderItem { get; set; }
        public decimal? Total { get; set; }


        public OrderController(ApplicationDbContext context, ICrud<Order> orderRepo, ICrud<Product> proRepo)
        {
            _context = context;
            _orderRepo = orderRepo;
            _proRepo = proRepo;

        }
        public IActionResult AddToCart(int ProductID)
        {
            var cart = new List<OrderItem>();
            var product = _proRepo.GetById(ProductID);
            if (product != null)
            {
                cart.Add(new OrderItem()
                {
                    Product = product,
                    Quantity = 1
                });
                HttpContext.Session.SetObjectAsJson("cart", cart);
            }


            return View();
        }
    }
}