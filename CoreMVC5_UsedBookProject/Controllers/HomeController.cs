﻿using CoreMVC5_UsedBookProject.Data;
using CoreMVC5_UsedBookProject.Models;
using CoreMVC5_UsedBookProject.Services;
using CoreMVC5_UsedBookProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
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

        public HomeController(ILogger<HomeController> logger, ProductContext productContext, BuyerService buyerService)
        {
            _logger = logger;
            _context = productContext;
            _buyerService = buyerService;
        }
        public IActionResult Index()
        {  
            var product = (from p in _context.Products
                           where p.Status == "已上架" 
                           orderby p.CreateDate descending
                           select new ProductViewModel { ProductId = p.ProductId, Title = p.Title, ISBN = p.ISBN, Author = p.Author, Publisher = p.Publisher, PublicationDate = p.PublicationDate, Degree = p.Degree, ContentText = p.ContentText, Image1 = p.Image1, Image2 = p.Image2, Status = p.Status, Trade = p.Trade, UnitPrice = p.UnitPrice, CreateDate = p.CreateDate, EditDate = p.EditDate, CreateBy = p.CreateBy }).Take(30).ToList();

          
            MyProductsViewModel mymodel = new()
            {
                Products = product
            };
            return View(mymodel);
        }
        private bool FuzzyMatch(string source, string target)
        {
            int sourceLength = source.Length;
            int targetLength = target.Length;
            int[,] distanceMatrix = new int[sourceLength + 1, targetLength + 1];

            
            for (int i = 0; i <= sourceLength; i++)
            {
                distanceMatrix[i, 0] = i;
            }

            for (int j = 0; j <= targetLength; j++)
            {
                distanceMatrix[0, j] = j;
            }

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

            int maxEditDistance = Math.Max(sourceLength, targetLength);
            int normalizedEditDistance = distanceMatrix[sourceLength, targetLength] * 100 / maxEditDistance;
            double similarityThreshold = 0.8; 
            return normalizedEditDistance <= (1 - similarityThreshold) * 100;
        }
        public IActionResult SearchProductbyName(string name)
        {
            List<ProductViewModel> products = new();
            if (!string.IsNullOrEmpty(name))
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
                if (products.Count == 0)
                {
                    products = _context.Products
                    .Where(p => p.Status == "已上架" && p.ISBN.Contains(name))
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



            ViewBag.Count = $"find {products.Count} products";
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
        public IActionResult ProductsList(int now_page, string trade)
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
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
       
    }
}
