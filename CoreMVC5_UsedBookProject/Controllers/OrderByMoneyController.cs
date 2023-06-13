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
    public class OrderByMoneyController : Controller
    {
        private readonly ProductContext _context;

        public OrderByMoneyController(ProductContext context)
        {
            _context = context;
        }

        // GET: OrderByMoney
        public async Task<IActionResult> Index()
        {
            var ProductContext = _context.OrderByMoneys.Include(o => o.Product);
            return View(await ProductContext.ToListAsync());
        }

        // GET: OrderByMoney/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderByMoney = await _context.OrderByMoneys
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.OrderByMoneyId == id);
            if (orderByMoney == null)
            {
                return NotFound();
            }

            return View(orderByMoney);
        }

        // GET: OrderByMoney/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Set<Product>(), "ProductId", "ProductId");
            return View();
        }

        // POST: OrderByMoney/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderByMoneyId,UnitPrice,SellerId,BuyerId,DenyReason,ProductId,Status,CreateDate")] OrderByMoney orderByMoney)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderByMoney);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Set<Product>(), "ProductId", "ProductId", orderByMoney.ProductId);
            return View(orderByMoney);
        }

        // GET: OrderByMoney/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderByMoney = await _context.OrderByMoneys.FindAsync(id);
            if (orderByMoney == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Set<Product>(), "ProductId", "ProductId", orderByMoney.ProductId);
            return View(orderByMoney);
        }

        // POST: OrderByMoney/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("OrderByMoneyId,UnitPrice,SellerId,BuyerId,DenyReason,ProductId,Status,CreateDate")] OrderByMoney orderByMoney)
        {
            if (id != orderByMoney.OrderByMoneyId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderByMoney);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderByMoneyExists(orderByMoney.OrderByMoneyId))
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
            ViewData["ProductId"] = new SelectList(_context.Set<Product>(), "ProductId", "ProductId", orderByMoney.ProductId);
            return View(orderByMoney);
        }

        // GET: OrderByMoney/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderByMoney = await _context.OrderByMoneys
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.OrderByMoneyId == id);
            if (orderByMoney == null)
            {
                return NotFound();
            }

            return View(orderByMoney);
        }

        // POST: OrderByMoney/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var orderByMoney = await _context.OrderByMoneys.FindAsync(id);
            _context.OrderByMoneys.Remove(orderByMoney);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderByMoneyExists(string id)
        {
            return _context.OrderByMoneys.Any(e => e.OrderByMoneyId == id);
        }
    }
}
