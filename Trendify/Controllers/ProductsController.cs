using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Trendify.Data;
using Trendify.Interface;
using Trendify.Models;

namespace Trendify.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProducts _context;

        public ProductsController(IProducts context)
        {
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


            //        var product = await _context.Products
            //            .Include(p => p.Category)
            //            .FirstOrDefaultAsync(m => m.ProductID == id);
            //        if (product == null)
            //        {
            //            return NotFound();
            //        }
            var product =await _context.GetProductById(id);
            return View(product);
        }

        //    // GET: Products/Create
        //    public IActionResult Create()
        //    {
        //        ViewData["CategoryID"] = new SelectList(_context.Categories, "CategoryID", "CategoryID");
        //        return View();
        //    }

        //    // POST: Products/Create
        //    // To protect from overposting attacks, enable the specific properties you want to bind to.
        //    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //    [HttpPost]
        //    [ValidateAntiForgeryToken]
        //    public async Task<IActionResult> Create([Bind("ProductID,Name,Description,Price,StockQuantity,CategoryID")] Product product)
        //    {

        //            _context.Add(product);
        //            await _context.SaveChangesAsync();
        //            return RedirectToAction(nameof(Index));

        //        ViewData["CategoryID"] = new SelectList(_context.Categories, "CategoryID", "CategoryID", product.CategoryID);
        //        return View(product);
        //    }

        //    // GET: Products/Edit/5
        //    public async Task<IActionResult> Edit(int? id)
        //    {
        //        if (id == null || _context.Products == null)
        //        {
        //            return NotFound();
        //        }

        //        var product = await _context.Products.FindAsync(id);
        //        if (product == null)
        //        {
        //            return NotFound();
        //        }
        //        ViewData["CategoryID"] = new SelectList(_context.Categories, "CategoryID", "CategoryID", product.CategoryID);
        //        return View(product);
        //    }

        //    // POST: Products/Edit/5
        //    // To protect from overposting attacks, enable the specific properties you want to bind to.
        //    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //    [HttpPost]
        //    [ValidateAntiForgeryToken]
        //    public async Task<IActionResult> Edit(int id, [Bind("ProductID,Name,Description,Price,StockQuantity,CategoryID")] Product product)
        //    {
        //        if (id != product.ProductID)
        //        {
        //            return NotFound();
        //        }



        //                _context.Update(product);
        //                await _context.SaveChangesAsync();


        //                if (!ProductExists(product.ProductID))
        //                {
        //                    return NotFound();
        //                }


        //            return RedirectToAction(nameof(Index));

        //        ViewData["CategoryID"] = new SelectList(_context.Categories, "CategoryID", "CategoryID", product.CategoryID);
        //        return View(product);
        //    }

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
