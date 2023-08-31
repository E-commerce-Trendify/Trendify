using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Trendify.Data;
using Trendify.DTOs;
using Trendify.Interface;
using Trendify.Models;

namespace Trendify.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategory _context;

        public CategoriesController(ICategory context)
        {
            _context = context;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            var categoty = await _context.GetAllCategories();

            return View(categoty);
                       
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int id)
        {

            var categoty = await _context.GetCategoryById(id);

            return View(categoty);
        }

        public async Task<IActionResult> AllCategoryProduct(int id)
        {

            var categoty = await _context.GetCategoryById(id);

            return View(categoty);
        }
        
        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }
        

        
        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        public async Task<IActionResult> Create(CategoryDTO category)
        {


            if (!ModelState.IsValid)
            {
                return View(category);
            }
            
            await _context.Create(category);
            return RedirectToAction("Index");
        }

        
        
        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Edit(int id, CategoryDTO category)
        {
            


            if (!ModelState.IsValid)
            {
                return View(category);
            }
            await _context.Update(id, category);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var prodcut = await _context.GetCategoryById(id);
            var Product = new CategoryDTO()
            {
               
                Name = prodcut.Name,
                Description = prodcut.Description,
                
            };
            return View(Product);
        }
       
        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _context.GetCategoryById(id);
           
            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost]
        
        public async Task<IActionResult> Delete(int id,string category)
        {
            await _context.Delete(id);
            return RedirectToAction("Index");
            
        }

        
    }
}
