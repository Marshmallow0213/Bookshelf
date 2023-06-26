using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Authorization;
using CoreMVC5_UsedBookProject.ViewModel;
using CoreMVC5_UsedBookProject.Services;
using System.Diagnostics;
using CoreMVC5_UsedBookProject.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace CoreMVC5_UsedBookProject.Controllers
{
    [Authorize(Roles = "Seller")]
    public class SellerController : Controller
    {
        private readonly SellerService _sellerService;

        public SellerController(SellerService sellerService)
        {
            _sellerService = sellerService;
        }
        public IActionResult Index()
        {
            var name = User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value;
            var count = _sellerService.OrdersAllCountList(name);
            return View(count);
        }
        public IActionResult MySales(String status, int now_page, string trade)
        {
            var name = User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value;
            MySalesViewModel mymodel = new();
            ViewBag.trade = trade;
            if (trade == "金錢")
            {
                mymodel = _sellerService.GetMoneyOrders(status, now_page, name);
                return View(mymodel);
            }
            else if (trade == "以物易物")
            {
                mymodel = _sellerService.GetBarterOrders(status, now_page, name);
                return View(mymodel);
            }
            else
            {
                return NotFound();
            }
        }
        public IActionResult MyProducts(String status, int now_page, string trade)
        {
            var name = User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value;
            MyProductsViewModel mymodel = new();
            ViewBag.trade = trade;
            if (trade == "金錢")
            {
                mymodel = _sellerService.GetProducts(status, now_page, name, trade);
                return View(mymodel);
            }
            else if (trade == "以物易物")
            {
                mymodel = _sellerService.GetProducts(status, now_page, name, trade);
                return View(mymodel);
            }
            else
            {
                return NotFound();
            }
        }
        public IActionResult Create()
        {
            var name = User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value;
            int[] checkLimit = _sellerService.ProductNewLimit(name);
            if (checkLimit[0] >= checkLimit[1])
            {
                return RedirectToAction("Index");
            }
            else
            {
                ProductCreateViewModel product = new() { ProductId = "Create_Product"};
                return View(product);
            }
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Create(ProductCreateViewModel productCreateViewModel)
        {
            if (ModelState.IsValid && (productCreateViewModel.Status == "已上架" || productCreateViewModel.Status == "未上架"))
            {
                var name = User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value;
                _sellerService.CreateProduct(productCreateViewModel, name);
                if (productCreateViewModel.Trade == "金錢")
                {
                    return RedirectToAction("MyProducts", new { trade = "金錢", status = productCreateViewModel.Status });
                }
                else
                {
                    return RedirectToAction("MyProducts", new { trade = "以物易物", status = productCreateViewModel.Status });
                }
            }
            else
            {
                return View(productCreateViewModel);
            }
        }

        

        public IActionResult Edit(string ProductId)
        {
            var name = User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value;
            if (ProductId == null)
            {
                return NotFound();
            }
            else
            {
                var product = _sellerService.GetProduct(ProductId, name);
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
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Edit(ProductEditViewModel productEditViewModel)
        {
            if (ModelState.IsValid)
            {
                var name = User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value;
                _sellerService.EditProduct(productEditViewModel, name);
                if (productEditViewModel.Trade == "金錢")
                {
                    return RedirectToAction("MyProducts", new { trade = "金錢", status = productEditViewModel.Status });
                }
                else
                {
                    return RedirectToAction("MyProducts", new { trade = "以物易物", status = productEditViewModel.Status });
                }
            }
            else
            {
                return View(productEditViewModel);
            }
        }
        
        public IActionResult Details(string ProductId)
        {
            var name = User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value;
            if (ProductId == null)
            {
                return NotFound();
            }
            else
            {
                var product = _sellerService.GetProduct(ProductId, name);
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
        public IActionResult Delete(string ProductId, string Trade)
        {
            var name = User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value;
            _sellerService.DeleteProduct(ProductId, name);
            if (Trade == "金錢")
            {
                return RedirectToAction("MyProducts", new { trade = "金錢", status = "刪除" });
            }
            else
            {
                return RedirectToAction("MyProducts", new { trade = "以物易物", status = "刪除" });
            }
        }
        public IActionResult PermanentDelete(string ProductId, string Trade)
        {
            var name = User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value;
            _sellerService.PermanentDeleteProduct(ProductId, name);
            if (Trade == "金錢")
            {
                return RedirectToAction("MyProducts", new { trade = "金錢", status = "刪除" });
            }
            else
            {
                return RedirectToAction("MyProducts", new { trade = "以物易物", status = "刪除" });
            }
        }
        public IActionResult Error()
        {
            return View();
        }
        
    }
}
