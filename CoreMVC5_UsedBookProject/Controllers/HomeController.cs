using CoreMVC5_UsedBookProject.Data;
using CoreMVC5_UsedBookProject.Models;
using CoreMVC5_UsedBookProject.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace CoreMVC5_UsedBookProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ProductContext _context;

        public HomeController(ILogger<HomeController> logger, ProductContext productContext)
        {
            _logger = logger;
            _context = productContext;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult SearchProductbyName()
        {
            string trade = "金錢";
            var product = (from p in _context.Products
                           where p.Status == "已上架" && p.Trade == $"{trade}"
                           orderby p.CreateDate descending
                           select new ProductViewModel { ProductId = p.ProductId, Title = p.Title, ISBN = p.ISBN, Author = p.Author, Publisher = p.Publisher, PublicationDate = p.PublicationDate, Degree = p.Degree, ContentText = p.ContentText, Image1 = p.Image1, Image2 = p.Image2, Status = p.Status, Trade = p.Trade, UnitPrice = p.UnitPrice, CreateDate = p.CreateDate, EditDate = p.EditDate, CreateBy = p.CreateBy }).ToList();
            
            MyProductsViewModel mymodel = new()
            {
                Products = product
            };
            return View(mymodel);
        }
        [HttpPost]
        public IActionResult SearchProductbyName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return Content("name不得為空字串!");
            }


            var products = (from p in _context.Products
                            where p.Status == "已上架" && p.Title.Contains(name)
                            orderby p.CreateDate descending
                            select new ProductViewModel { ProductId = p.ProductId, Title = p.Title, ISBN = p.ISBN, Author = p.Author, Publisher = p.Publisher, PublicationDate = p.PublicationDate, Degree = p.Degree, ContentText = p.ContentText, Image1 = p.Image1, Image2 = p.Image2, Status = p.Status, Trade = p.Trade, UnitPrice = p.UnitPrice, CreateDate = p.CreateDate, EditDate = p.EditDate, CreateBy = p.CreateBy }).ToList();

            //判斷集合是否有資料
            if (products.Count == 0)
            {
                return Content($"找不到任何的{name}資料記錄");
            }
            MyProductsViewModel mymodel = new()
            {
                Products = products
            };
            //指派使用ListTable.cshtml
            return View(mymodel);
        }
        public IActionResult Privacy()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
