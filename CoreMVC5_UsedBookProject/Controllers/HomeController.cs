using CoreMVC5_UsedBookProject.Data;
using CoreMVC5_UsedBookProject.Models;
using CoreMVC5_UsedBookProject.Services;
using CoreMVC5_UsedBookProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;


namespace CoreMVC5_UsedBookProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ProductContext _context;
        private readonly BuyerService _buyerService;
        private readonly AdminAccountContext _ctx;

        public HomeController(ILogger<HomeController> logger, ProductContext productContext, BuyerService buyerService, AdminAccountContext ctx)
        {
            _logger = logger;
            _context = productContext;
            _buyerService = buyerService;
            _ctx = ctx;
        }
        public async Task<IActionResult> Index()
        {
            var textbox = await _ctx.TextValue.FirstOrDefaultAsync();
            var model = await _ctx.TextValue.Select(g => new TextboxViewModel { Image1 = g.Image1, Image2 = g.Image2, Image3 = g.Image3 }).FirstOrDefaultAsync();
            string[] imgs = { model.Image1, model.Image2, model.Image3 };
            ViewBag.Imgs = imgs;
            return View(textbox);
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult SearchProducts(string name)
        {
            List<ProductViewModel> products = new();
            if (!string.IsNullOrEmpty(name))
            {
                products = _context.Products
                    .Where(p => p.Status == "已上架" && p.ISBN == name)
                    .OrderByDescending(p => p.CreateDate)
                    .Select(p => new ProductViewModel
                    {
                        ProductId = p.ProductId,
                        Title = p.Title,
                        ISBN = p.ISBN,
                        Author = p.Author,
                        Publisher = p.Publisher,
                        PublicationDate = p.PublicationDate,
                        Degree = p.Degree,
                        ContentText = p.ContentText,
                        Image1 = p.Image1,
                        Image2 = p.Image2,
                        Status = p.Status,
                        Trade = p.Trade,
                        UnitPrice = p.UnitPrice,
                        CreateDate = p.CreateDate,
                        EditDate = p.EditDate,
                        CreateBy = p.CreateBy
                    })
                    .ToList();
                if (products.Count == 0)
                {
                    products = _context.Products
                .Where(p => p.Status == "已上架" && p.Title.Contains(name))
                .OrderByDescending(p => p.CreateDate)
                .Select(p => new ProductViewModel
                {
                    ProductId = p.ProductId,
                    Title = p.Title,
                    ISBN = p.ISBN,
                    Author = p.Author,
                    Publisher = p.Publisher,
                    PublicationDate = p.PublicationDate,
                    Degree = p.Degree,
                    ContentText = p.ContentText,
                    Image1 = p.Image1,
                    Image2 = p.Image2,
                    Status = p.Status,
                    Trade = p.Trade,
                    UnitPrice = p.UnitPrice,
                    CreateDate = p.CreateDate,
                    EditDate = p.EditDate,
                    CreateBy = p.CreateBy
                })
                .ToList();
                }
            }
            ViewBag.Count = $"找到 {products.Count} 項商品";
            MyProductsViewModel mymodel = new MyProductsViewModel
            {
                Products = products
            };
            return View(mymodel);
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
                if (product == null || product.Status != "已上架")
                {
                    return NotFound();
                }
                else
                {
                    return View(product);
                }
            }
        }
        public IActionResult ProductsList(int now_page, string trade)
        {
            MyProductsViewModel mymodel = new();
            ViewBag.trade = trade;
            if (trade == "買賣")
            {
                mymodel = _buyerService.GetProducts(now_page, trade);
                return View(mymodel);
            }
            else if (trade == "交換")
            {
                mymodel = _buyerService.GetProducts(now_page, trade);
                return View(mymodel);
            }
            else
            {
                return NotFound();
            }
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        private List<string> GetPredictionsFromData(string searchText)
        {
            var predictions = _context.Products
                .Where(p => p.Title.Contains(searchText) && p.Status == "已上架")
                .Select(p => p.Title)
                .Take(15).ToList();
            if (predictions.Count == 0)
            {
                predictions = _context.Products.Where(p => p.ISBN.Contains(searchText))
               .Select(p => p.ISBN)
               .ToList();

            }
            return predictions;
        }

        public IActionResult GetPredictions(string searchText)
        {
            var predictions = GetPredictionsFromData(searchText);
            return PartialView("GetPredictions", predictions);
        }
        public IActionResult Wish(int now_page)
        {
            now_page = now_page == 0 ? 1 : now_page;
            var countWish = _context.Wishes.Select(g => new { g.WishId }).ToList();
            int all_pages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(countWish.Count) / 30));
            var wishlist = (from w in _context.Wishes from u in _context.Users where w.Id == u.Id select new WishViewModel { Title = w.Title, ISBN = w.ISBN, WishId = w.WishId, UserName = u.Name }).Skip((now_page - 1) * 30).Take(30).ToList();
            WishsViewModel wishs = new()
            {
                Wishs = wishlist,
                PagesCount = new int[] { now_page, all_pages }
            };
            return View(wishs);
        }
    }
}
