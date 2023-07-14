using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Authorization;
using CoreMVC5_UsedBookProject.ViewModels;
using CoreMVC5_UsedBookProject.Services;
using System.Diagnostics;
using CoreMVC5_UsedBookProject.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace CoreMVC5_UsedBookProject.Controllers
{
    [Authorize(Roles = "User")]
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
            var checkStatus = _sellerService.CheckOrdersStatus(status);
            if (!checkStatus)
            {
                return NotFound();
            }
            var checkTrade = _sellerService.CheckOrdersTrade(trade);
            if (!checkTrade)
            {
                return NotFound();
            }
            var name = User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value;
            MySalesViewModel mymodel = new();
            ViewBag.trade = trade;
            if(trade == "買賣")
            {
                mymodel = _sellerService.GetOrders(status, trade, now_page, name);
            }
            else if(trade == "交換")
            {
                mymodel = _sellerService.GetBarterOrders(status, trade, now_page, name);
            }
            return View(mymodel);
        }
        public IActionResult MyProducts(String status, int now_page, string trade)
        {
            var checkStatus = _sellerService.CheckProductsStatus(status);
            if (!checkStatus)
            {
                return NotFound();
            }
            var checkTrade = _sellerService.CheckProductsTrade(trade);
            if (!checkTrade)
            {
                return NotFound();
            }
            var name = User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value;
            MyProductsViewModel mymodel = new();
            ViewBag.trade = trade;
            mymodel = _sellerService.GetProducts(status, now_page, name, trade);
            return View(mymodel);
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
                if (productCreateViewModel.Trade != "買賣" && productCreateViewModel.Trade != "交換" && productCreateViewModel.Trade != "買賣與交換")
                {
                    return View(productCreateViewModel);
                }
                var name = User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value;
                _sellerService.CreateProduct(productCreateViewModel, name);
                if (productCreateViewModel.Trade == "買賣" || productCreateViewModel.Trade == "買賣與交換")
                {
                    return RedirectToAction("MyProducts", new { trade = "買賣", status = productCreateViewModel.Status });
                }
                else
                {
                    return RedirectToAction("MyProducts", new { trade = "交換", status = productCreateViewModel.Status });
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
                if (product == null || product.CreateBy != name)
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
                if (productEditViewModel.Trade != "買賣" && productEditViewModel.Trade != "交換" && productEditViewModel.Trade != "買賣與交換")
                {
                    return NotFound();
                }
                var name = User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value;
                _sellerService.EditProduct(productEditViewModel, name);
                if (productEditViewModel.Trade == "買賣" || productEditViewModel.Trade == "買賣與交換")
                {
                    return RedirectToAction("MyProducts", new { trade = "買賣", status = productEditViewModel.Status });
                }
                else
                {
                    return RedirectToAction("MyProducts", new { trade = "交換", status = productEditViewModel.Status });
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
                if (product == null || product.CreateBy != name)
                {
                    return NotFound();
                }
                else
                {
                    return View(product);
                }
            }
        }
        public IActionResult OrderDetails(string OrderId, string trade)
        {
            var name = User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value;
            OrderViewModel mymodel = new();
            ViewBag.trade = trade;
            if (trade != "買賣" && trade != "交換")
            {
                return NotFound();
            }
            mymodel = _sellerService.GetOrder(OrderId);
            if (mymodel == null || mymodel.SellerId != name)
            {
                return NotFound();
            }
            return View(mymodel);
        }
        [HttpPost]
        public IActionResult OrderDetails(string OrderId, string trade, string submit)
        {
            if (submit == "拒絕交易")
            {
                _sellerService.DenyOrder(OrderId);
                return RedirectToAction("MySales", new { status = "不成立", trade = "買賣" });
            }
            else if (submit == "取消訂單")
            {
                _sellerService.CancelOrder(OrderId);
                return RedirectToAction("MySales", new { status = "不成立", trade = "買賣" });
            }
            else if (submit == "接受交易")
            {
                _sellerService.AcceptOrder(OrderId);
                return RedirectToAction("MySales", new { status = "已成立", trade = "買賣" });
            }
            else
            {
                return NotFound();
            }
        }
        public IActionResult BarterOrderDetails(string OrderId, string trade)
        {
            var name = User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value;
            BarterOrderViewModel mymodel = new();
            ViewBag.trade = trade;
            if (trade != "買賣" && trade != "交換")
            {
                return NotFound();
            }
            mymodel = _sellerService.GetBarterOrder(OrderId);
            if (mymodel == null || mymodel.SellerId != name)
            {
                return NotFound();
            }
            return View(mymodel);
        }
        [HttpPost]
        public IActionResult BarterOrderDetails(string OrderId, string trade, string ProductId, string submit)
        {
            if(submit == "拒絕交換")
            {
                _sellerService.DenyBarterOrder(OrderId);
                return RedirectToAction("MySales", new { status = "不成立", trade = "交換" });
            }
            else if (submit == "取消訂單")
            {
                _sellerService.CancelBarterOrder(OrderId);
                return RedirectToAction("MySales", new { status = "不成立", trade = "交換" });
            }
            else if (submit == "接受與此書交換")
            {
                _sellerService.AcceptBarterOrder(OrderId, ProductId);
                return RedirectToAction("MySales", new { status = "已成立", trade = "交換" });
            }
            else
            {
                return NotFound();
            }
        }
        public IActionResult Delete(string ProductId, string Trade)
        {
            var name = User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value;
            var product = _sellerService.DeleteProduct(ProductId, name);
            if(product == false)
            {
                return RedirectToAction("Index", new {});
            }
            if (Trade.Contains("買賣"))
            {
                return RedirectToAction("MyProducts", new { trade = "買賣", status = "刪除" });
            }
            else
            {
                return RedirectToAction("MyProducts", new { trade = "交換", status = "刪除" });
            }
        }
        public IActionResult Error()
        {
            return View();
        }
        
    }
}
