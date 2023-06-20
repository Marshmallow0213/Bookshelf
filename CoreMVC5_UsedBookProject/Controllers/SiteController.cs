using Microsoft.AspNetCore.Mvc;
using CoreMVC5_UsedBookProject.Models;
using CoreMVC5_UsedBookProject.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using CoreMVC5_UsedBookProject.Interfaces;
using CoreMVC5_UsedBookProject.Services;
using CoreMVC5_UsedBookProject.ViewModel;
using System.Drawing;
using System.Diagnostics;

namespace CoreMVC5_UsedBookProject.Controllers
{
    public class SiteController : Controller


    {
        private readonly ProductContext _ctx;
       
        public SiteController(ProductContext ctx)
        {
            _ctx = ctx;
           
        }




        [HttpPost]
        public IActionResult SearchProductbyName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return Content("name不得為空字串!");
            }

        
            var products = (from p in _ctx.Products
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
    }
}