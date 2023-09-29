using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Trendify.Data;
using Trendify.Models;

namespace Trendify.Controllers
{
    public class OrdersController : Controller
    {
        private readonly EcommerceDbContext _context;

        public OrdersController(EcommerceDbContext context)
        {
            _context = context;
        }

        // GET: Orders
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Index()
        {
            var orders = await _context.Orders.Include(o=>o.user).Include(u=>u.ShoppingCarts).ToListAsync();
            return View(orders);
        }

        // GET: Orders/Details/5


        // GET: Orders/Create


        // GET: Orders/Edit/5

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.


        // GET: Orders/Delete/5

    }
}
