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
    public class MoneyOrderController : Controller
    {
        private readonly OrderContext _context;

        public MoneyOrderController(OrderContext context)
        {
            _context = context;
        }

        // GET: MoneyOrder
        public async Task<IActionResult> Index()
        {
            return View(await _context.MoneyOrder.ToListAsync());
        }

        // GET: MoneyOrder/Details/5
        public async Task<IActionResult> MoneyOrderResult(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moneyOrder = await _context.MoneyOrder
                .FirstOrDefaultAsync(m => m.MoneyOrderId == id);
            if (moneyOrder == null)
            {
                return NotFound();
            }

            return View(moneyOrder);
        }

        // GET: MoneyOrder/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MoneyOrder/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MoneyOrderId,ProductId,UnitPrice,denyreason,Buyer,Seller")] MoneyOrder moneyOrder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(moneyOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(moneyOrder);
        }

        // GET: MoneyOrder/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            id = "M1";
            if (id == null)
            {
                return NotFound();
            }

            var moneyOrder = await _context.MoneyOrder.FindAsync(id);
            if (moneyOrder == null)
            {
                return NotFound();
            }
            return View(moneyOrder);
        }

        // POST: MoneyOrder/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MoneyOrderId,ProductId,UnitPrice,denyreason,Buyer,Seller")] MoneyOrder moneyOrder)
        {
            if (id != moneyOrder.MoneyOrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(moneyOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MoneyOrderExists(moneyOrder.MoneyOrderId))
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
            return View(moneyOrder);
        }

        // GET: MoneyOrder/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moneyOrder = await _context.MoneyOrder
                .FirstOrDefaultAsync(m => m.MoneyOrderId == id);
            if (moneyOrder == null)
            {
                return NotFound();
            }

            return View(moneyOrder);
        }

        // POST: MoneyOrder/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var moneyOrder = await _context.MoneyOrder.FindAsync(id);
            _context.MoneyOrder.Remove(moneyOrder);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MoneyOrderExists(string id)
        {
            return _context.MoneyOrder.Any(e => e.MoneyOrderId == id);
        }
    }
}
