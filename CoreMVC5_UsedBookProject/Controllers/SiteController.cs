using Microsoft.AspNetCore.Mvc;
using CoreMVC5_UsedBookProject.Models;
using CoreMVC5_UsedBookProject.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using CoreMVC5_UsedBookProject.Interfaces;
using CoreMVC5_UsedBookProject.Services;

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

         
            var products =_ctx.Products.Where(x => x.Title.Contains(name)).ToList();
         

            //判斷集合是否有資料
            if (products.Count == 0)
            {
                return Content($"找不到任何的{name}資料記錄");
            }

            //指派使用ListTable.cshtml
            return View();
        }
    }
}