using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Trendify.Data;
using Trendify.DTOs;
using Trendify.Interface;
using Trendify.Models;

namespace Trendify.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProducts _context;
        private readonly ICategory categories;

        public ProductsController(IProducts context, ICategory categorie)
        {
            categories = categorie;
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var ecommerceDbContext = await _context.GetAllProducts();
            return View(ecommerceDbContext);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int id)
        {

            var product =await _context.GetProductById(id);
            return View(product);
        }

        // GET: Products/Create
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            var categoriees = await categories.GetAllCategories();
            ViewBag.AllCategories = new SelectList(categoriees, "CategoryID", "Name");
            ProductsDto productsDto = new ProductsDto();
            return View(productsDto);
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create( ProductsDto product , IFormFile file)
        {
            var categoriees = await categories.GetAllCategories();
            ViewBag.AllCategories = new SelectList(categoriees, "CategoryID", "Name");
            var imagesURl = await _context.UploadFile(file);
            ModelState.Remove("file");

            if (!ModelState.IsValid)
            {
                return View(product);
            }
            await _context.Create(product,imagesURl);
            return RedirectToAction("Index");


        }

        // GET: Products/Edit/5
        [Authorize(Roles = "Editor")]
        public async Task<IActionResult> Edit(int id)
        {
            var categoriees = await categories.GetAllCategories();
            ViewBag.AllCategories = new SelectList(categoriees, "CategoryID", "Name");
            var prodcut = await _context.GetProductById(id);
            var Product = new ProductsDto()
            {
                ProductId = prodcut.ProductID,
                CategoryID = prodcut.CategoryID,
                StockQuantity = prodcut.StockQuantity,
                Name = prodcut.Name,
                Description = prodcut.Description,  
                Price  = prodcut.Price,
                ImageURL=prodcut.ImageUrl
            };

            return View(Product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Edit(int id, ProductsDto product,IFormFile file)
        {
            var categoriees = await categories.GetAllCategories();
            ViewBag.AllCategories = new SelectList(categoriees, "CategoryID", "Name");
            var imagesURl = await _context.UploadFile(file);
            ModelState.Remove("file");

            if (!ModelState.IsValid)
            {
                return View(product);
            }

            await _context.Update(product, id,imagesURl);
            return RedirectToAction("Index");

        }

        // GET: Categories/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _context.GetProductById(id);
            return View(category);
        }
        // POST: Categories/Delete/5
        [HttpPost]
        public async Task<IActionResult> Delete(int id, string category)
        {
            await _context.Delete(id);
            return RedirectToAction("Index");
        }

        //    // GET: Products/Delete/5
        //    public async Task<IActionResult> Delete(int? id)
        //    {
        //        if (id == null || _context.Products == null)
        //        {
        //            return NotFound();
        //        }

        //        var product = await _context.Products
        //            .Include(p => p.Category)
        //            .FirstOrDefaultAsync(m => m.ProductID == id);
        //        if (product == null)
        //        {
        //            return NotFound();
        //        }

        //        return View(product);
        //    }

        //    // POST: Products/Delete/5
        //    [HttpPost, ActionName("Delete")]
        //    [ValidateAntiForgeryToken]
        //    public async Task<IActionResult> DeleteConfirmed(int id)
        //    {
        //        if (_context.Products == null)
        //        {
        //            return Problem("Entity set 'EcommerceDbContext.Products'  is null.");
        //        }
        //        var product = await _context.Products.FindAsync(id);
        //        if (product != null)
        //        {
        //            _context.Products.Remove(product);
        //        }

        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }

        //    private bool ProductExists(int id)
        //    {
        //      return (_context.Products?.Any(e => e.ProductID == id)).GetValueOrDefault();
        //    }
        //}
    }
}
