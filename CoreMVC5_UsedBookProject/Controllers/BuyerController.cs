using CoreMVC5_UsedBookProject.Data;
using CoreMVC5_UsedBookProject.Services;
using CoreMVC5_UsedBookProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace CoreMVC5_UsedBookProject.Controllers
{
    [Authorize(Roles = "Buyer")]
    public class BuyerController : Controller
    {
        private readonly BuyerService _buyerService;
        private readonly ProductContext _context;

        public BuyerController(BuyerService buyerService, ProductContext productContext)
        {
            _buyerService = buyerService;
            _context = productContext;
        }
        public IActionResult Details(string ProductId)
        {
            if (ProductId == null)
            {
                return NotFound();
            }
            else
            {
                var product = _buyerService.GetProduct(ProductId);
                if (product == null)
                {
                    return NotFound();
                }
                else
                {
                    return View(product);
                }
            }
        }
        public IActionResult MySales(String status, int now_page, string trade)
        {
            var name = User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value;
            MySalesViewModel mymodel = new();
            ViewBag.trade = trade;
            mymodel = _buyerService.GetOrders(trade, status, now_page, name);
            return View(mymodel);
        }
        public IActionResult CreateOrder(string ProductId, string Sellername, string trade)
        {
            if (ProductId != null && Sellername != null)
            {
                var buyername = User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value;
                _buyerService.CreateOrder(trade, Sellername, buyername, ProductId);
            }
            return RedirectToAction("MySales", new { Trade = trade });
        }
    }
}
