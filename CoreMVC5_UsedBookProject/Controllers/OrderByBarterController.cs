using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoreMVC5_UsedBookProject.Data;
using CoreMVC5_UsedBookProject.Models;

namespace CoreMVC5_UsedBookProject.Controllers
{
    public class OrderByBarterController : Controller
    {
        private readonly ProductContext _context;

        public OrderByBarterController(ProductContext context)
        {
            _context = context;
        }

        // GET: OrderByBarter
        public async Task<IActionResult> Index()
        {
            var ProductContext = _context.OrderByBarters.Include(o => o.Product);
            return View(await ProductContext.ToListAsync());
        }

        // GET: OrderByBarter/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderByBarter = await _context.OrderByBarters
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.OrderByBarterId == id);
            if (orderByBarter == null)
            {
                return NotFound();
            }

            return View(orderByBarter);
        }

        // GET: OrderByBarter/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Set<Product>(), "ProductId", "ProductId");
            return View();
        }

        // POST: OrderByBarter/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderByBarterId,SellerId,BuyerId,DenyReason,ProductId,Status,CreateDate")] OrderByBarter orderByBarter)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderByBarter);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Set<Product>(), "ProductId", "ProductId", orderByBarter.ProductId);
            return View(orderByBarter);
        }

        // GET: OrderByBarter/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderByBarter = await _context.OrderByBarters.FindAsync(id);
            if (orderByBarter == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Set<Product>(), "ProductId", "ProductId", orderByBarter.ProductId);
            return View(orderByBarter);
        }

        // POST: OrderByBarter/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("OrderByBarterId,SellerId,BuyerId,DenyReason,ProductId,Status,CreateDate")] OrderByBarter orderByBarter)
        {
            if (id != orderByBarter.OrderByBarterId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderByBarter);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderByBarterExists(orderByBarter.OrderByBarterId))
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
            ViewData["ProductId"] = new SelectList(_context.Set<Product>(), "ProductId", "ProductId", orderByBarter.ProductId);
            return View(orderByBarter);
        }

        // GET: OrderByBarter/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderByBarter = await _context.OrderByBarters
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.OrderByBarterId == id);
            if (orderByBarter == null)
            {
                return NotFound();
            }

            return View(orderByBarter);
        }

        // POST: OrderByBarter/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var orderByBarter = await _context.OrderByBarters.FindAsync(id);
            _context.OrderByBarters.Remove(orderByBarter);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderByBarterExists(string id)
        {
            return _context.OrderByBarters.Any(e => e.OrderByBarterId == id);
        }
    }
}
