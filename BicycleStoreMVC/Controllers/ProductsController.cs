﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BicycleStoreMVC.Data;
using BicycleStore.Domain.Models;
using BicycleStoreMVC.Repositories;

namespace BicycleStoreMVC.Controllers
{
    /// <summary>
    /// This controller manages view and CRUD actions on products
    /// </summary>
    public class ProductsController : Controller
    {
        private readonly BicycleStoreDbContext _context;
        private readonly ICrud<Product> _proRepo;
        private readonly ICrud<Brand> _brandRepo;
        public ProductsController(BicycleStoreDbContext context, ICrud<Product> proRepo, ICrud<Brand> brandRepo)
        {
            _context = context;
            _proRepo = proRepo;
            _brandRepo = brandRepo;
        }

        // GET: Products
        public IActionResult Index()
        {
            var products = _proRepo.GetAll().Include(p => p.Brand).Include(p => p.Category);
            return View(products);
        }

        // GET: Products/Details/{id}
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = _proRepo.GetById(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["BrandName"] = new SelectList(_context.Brands , "BrandID", "BrandName");
            ViewData["CategoryName"] = new SelectList(_context.Categories, "CategoryID", "CategoryName");
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductID,ProductName,ModelYear,ListPrice,CategoryID,BrandID")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/{id}
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["BrandName"] = new SelectList(_context.Brands, "BrandID", "BrandName", product.BrandID);
            ViewData["CategoryName"] = new SelectList(_context.Categories, "CategoryID", "CategoryName", product.CategoryID);
            return View(product);
        }

        // POST: Products/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductID,ProductName,ModelYear,ListPrice,CategoryID,BrandID")] Product product)
        {
            if (id != product.ProductID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrandID"] = new SelectList(_context.Brands, "BrandID", "BrandID", product.BrandID);
            ViewData["CategoryID"] = new SelectList(_context.Categories, "CategoryID", "CategoryID", product.CategoryID);
            return View(product);
        }

        // GET: Products/Delete/{id}
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductID == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductID == id);
        }
    }
}
