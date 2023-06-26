using CoreMVC5_UsedBookProject.Data;
using CoreMVC5_UsedBookProject.Services;
using CoreMVC5_UsedBookProject.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Xml.Linq;

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
        [AllowAnonymous]
        public IActionResult Index(int now_page, string trade)
        {
            MyProductsViewModel mymodel = new();
            ViewBag.trade = trade;
            if (trade == "金錢")
            {
                mymodel = _buyerService.GetProducts(now_page, trade);
                return View(mymodel);
            }
            else if (trade == "以物易物")
            {
                mymodel = _buyerService.GetProducts(now_page, trade);
                return View(mymodel);
            }
            else
            {
                return NotFound();
            }
        }
        [AllowAnonymous]
        public IActionResult Details(string ProductId)
        {
            var name = User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value;
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
            string name = User.Identity.Name;
            MySalesViewModel mymodel = new();
            ViewBag.trade = trade;
            if (trade == "金錢")
            {
                mymodel = _buyerService.GetMoneyOrders(status, now_page, name);
                return View(mymodel);
            }
            else if (trade == "以物易物")
            {
                mymodel = _buyerService.GetBarterOrders(status, now_page, name);
                return View(mymodel);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
