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
    public class ChangeOrderController : Controller
    {
        private readonly OrderContext _context;

        public ChangeOrderController(OrderContext context)
        {
            _context = context;
        }

        // GET: ChangeOrder
        public async Task<IActionResult> Index()
        {
            return View(await _context.ChangeOrder.ToListAsync());
        }

        // GET: ChangeOrder/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var changeOrder = await _context.ChangeOrder
                .FirstOrDefaultAsync(m => m.ChangeOrderId == id);
            if (changeOrder == null)
            {
                return NotFound();
            }

            return View(changeOrder);
        }

        // GET: ChangeOrder/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ChangeOrder/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ChangeOrderId,ProductId,Buyer,Seller,denyreason")] ChangeOrder changeOrder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(changeOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(changeOrder);
        }

        // GET: ChangeOrder/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var changeOrder = await _context.ChangeOrder.FindAsync(id);
            if (changeOrder == null)
            {
                return NotFound();
            }
            return View(changeOrder);
        }

        // POST: ChangeOrder/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ChangeOrderId,ProductId,Buyer,Seller,denyreason")] ChangeOrder changeOrder)
        {
            if (id != changeOrder.ChangeOrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(changeOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChangeOrderExists(changeOrder.ChangeOrderId))
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
            return View(changeOrder);
        }

        // GET: ChangeOrder/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var changeOrder = await _context.ChangeOrder
                .FirstOrDefaultAsync(m => m.ChangeOrderId == id);
            if (changeOrder == null)
            {
                return NotFound();
            }

            return View(changeOrder);
        }

        // POST: ChangeOrder/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var changeOrder = await _context.ChangeOrder.FindAsync(id);
            _context.ChangeOrder.Remove(changeOrder);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChangeOrderExists(string id)
        {
            return _context.ChangeOrder.Any(e => e.ChangeOrderId == id);
        }
    }
}
