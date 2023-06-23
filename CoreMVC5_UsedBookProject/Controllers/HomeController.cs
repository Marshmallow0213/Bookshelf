using CoreMVC5_UsedBookProject.Data;
using CoreMVC5_UsedBookProject.Models;
using CoreMVC5_UsedBookProject.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public HomeController(ILogger<HomeController> logger, ProductContext productContext)
        {
            _logger = logger;
            _context = productContext;
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
            var NickName = HttpContext.Request.Cookies["NickName"];
            ViewBag.NickName = NickName;
            var UserIcon = HttpContext.Request.Cookies["UserIcon"];
            ViewBag.UserIcon = UserIcon;
        }
        public IActionResult Index()
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
        //public IActionResult SearchProductbyName()
        //{
        //    string trade = "金錢";
        //    var product = (from p in _context.Products
        //                   where p.Status == "已上架" && p.Trade == $"{trade}"
        //                   orderby p.CreateDate descending
        //                   select new ProductViewModel { ProductId = p.ProductId, Title = p.Title, ISBN = p.ISBN, Author = p.Author, Publisher = p.Publisher, PublicationDate = p.PublicationDate, Degree = p.Degree, ContentText = p.ContentText, Image1 = p.Image1, Image2 = p.Image2, Status = p.Status, Trade = p.Trade, UnitPrice = p.UnitPrice, CreateDate = p.CreateDate, EditDate = p.EditDate, CreateBy = p.CreateBy }).ToList();

        //    MyProductsViewModel mymodel = new()
        //    {
        //        Products = product
        //    };
        //    return View(mymodel);
        //}
        private bool FuzzyMatch(string source, string target)
        {
            int sourceLength = source.Length;
            int targetLength = target.Length;
            int[,] distanceMatrix = new int[sourceLength + 1, targetLength + 1];

            // 初始化距离矩阵
            for (int i = 0; i <= sourceLength; i++)
            {
                distanceMatrix[i, 0] = i;
            }

            for (int j = 0; j <= targetLength; j++)
            {
                distanceMatrix[0, j] = j;
            }

            // 计算编辑距离
            for (int i = 1; i <= sourceLength; i++)
            {
                for (int j = 1; j <= targetLength; j++)
                {
                    int substitutionCost = (source[i - 1] == target[j - 1]) ? 0 : 1;
                    distanceMatrix[i, j] = Math.Min(
                        Math.Min(distanceMatrix[i - 1, j] + 1, distanceMatrix[i, j - 1] + 1),
                        distanceMatrix[i - 1, j - 1] + substitutionCost);
                }
            }

            // 判断是否匹配成功
            int maxEditDistance = Math.Max(sourceLength, targetLength);
            int normalizedEditDistance = distanceMatrix[sourceLength, targetLength] * 100 / maxEditDistance;
            double similarityThreshold = 0.8; // 设置相似度阈值
            return normalizedEditDistance <= (1 - similarityThreshold) * 100;
        }
        [HttpGet]
        //public IActionResult SearchProductbyName(string name)
        //{
        //    if (string.IsNullOrEmpty(name))
        //    {
        //        return Content("name不得為空字串!");
        //    }


        //    var products = (from p in _context.Products
        //                    where p.Status == "已上架" && p.Title.Contains(name)
        //                    orderby p.CreateDate descending
        //                    select new ProductViewModel { ProductId = p.ProductId, Title = p.Title, ISBN = p.ISBN, Author = p.Author, Publisher = p.Publisher, PublicationDate = p.PublicationDate, Degree = p.Degree, ContentText = p.ContentText, Image1 = p.Image1, Image2 = p.Image2, Status = p.Status, Trade = p.Trade, UnitPrice = p.UnitPrice, CreateDate = p.CreateDate, EditDate = p.EditDate, CreateBy = p.CreateBy }).ToList();

        //    //判斷集合是否有資料
        //    if (products.Count == 0)
        //    {
        //        return Content($"找不到任何的{name}資料記錄");
        //    }

        //    if (products.Count == 0)
        //    {
        //        products = _context.Products.Where(p => FuzzyMatch(p.Title, name)).ToList();
        //    }
        //    MyProductsViewModel mymodel = new()
        //    {
        //        Products = products
        //    };
        //    //指派使用ListTable.cshtml
        //    return View(mymodel);
        //}
        public IActionResult SearchProductbyName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return Content("name不得為空字串!");
            }

            var products = _context.Products
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
            if (products.Count == 0)
            {
                return Content($"找不到任何的{name}資料記錄");
            }

            // 判断集合是否有数据
            if (products.Count == 0)
            {
                // 进行模糊匹配
                var _products = _context.Products
                    .Where(p => FuzzyMatch(p.Title, name))
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

                MyProductsViewModel mysearchproductmodel = new MyProductsViewModel
                {
                    Products = _products
                };

                // 指派使用ListTable.cshtml
                return View(mysearchproductmodel);
            }

            MyProductsViewModel mymodel = new MyProductsViewModel
            {
                Products = products
            };
            return View(mymodel);
        }
         
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
       
    }
}
