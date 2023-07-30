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
    [Authorize]
    public class SellerController : Controller
    {
        private readonly SellerService _sellerService;

        public SellerController(SellerService sellerService)
        {
            _sellerService = sellerService;
        }
        public IActionResult Index()
        {
            var name = User.Identity.Name;
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
            var name = User.Identity.Name;
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
            var name = User.Identity.Name;
            MyProductsViewModel mymodel = new();
            ViewBag.trade = trade;
            mymodel = _sellerService.GetProducts(status, now_page, name, trade);
            return View(mymodel);
        }
        public IActionResult Create()
        {
            var name = User.Identity.Name;
            int[] checkLimit = _sellerService.ProductNewLimit(name);
            if (checkLimit[0] >= checkLimit[1])
            {
                return RedirectToAction("Index");
            }
            else
            {
                ProductViewModel product = new() { ProductId = "Create_Product", Image1 = "無圖片,無圖片,無圖片,無圖片,無圖片,無圖片,無圖片,無圖片,無圖片", CreateBy = name};
                return View(product);
            }
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Create(ProductViewModel ProductViewModel)
        {
            if (ModelState.IsValid && (ProductViewModel.Status == "已上架" || ProductViewModel.Status == "未上架"))
            {
                if (ProductViewModel.Trade != "買賣" && ProductViewModel.Trade != "交換" && ProductViewModel.Trade != "買賣與交換")
                {
                    return View(ProductViewModel);
                }
                var name = User.Identity.Name;
                _sellerService.CreateProduct(ProductViewModel, name);
                if (ProductViewModel.Trade == "買賣" || ProductViewModel.Trade == "買賣與交換")
                {
                    return RedirectToAction("MyProducts", new { trade = "買賣", status = ProductViewModel.Status });
                }
                else
                {
                    return RedirectToAction("MyProducts", new { trade = "交換", status = ProductViewModel.Status });
                }
            }
            else
            {
                return View(ProductViewModel);
            }
        }

        public IActionResult Edit(string ProductId)
        {
            var name = User.Identity.Name;
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
                    if(product.Status == "已上架" ||  product.Status == "未上架")
                    {
                        return View(product);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Edit(ProductViewModel ProductViewModel)
        {
            if (ModelState.IsValid)
            {
                if (ProductViewModel.Trade != "買賣" && ProductViewModel.Trade != "交換" && ProductViewModel.Trade != "買賣與交換")
                {
                    return NotFound();
                }
                var name = User.Identity.Name;
                _sellerService.EditProduct(ProductViewModel, name);
                if (ProductViewModel.Trade == "買賣" || ProductViewModel.Trade == "買賣與交換")
                {
                    return RedirectToAction("MyProducts", new { trade = "買賣", status = ProductViewModel.Status });
                }
                else
                {
                    return RedirectToAction("MyProducts", new { trade = "交換", status = ProductViewModel.Status });
                }
            }
            else
            {
                return View(ProductViewModel);
            }
        }
        
        public IActionResult Details(string ProductId)
        {
            var name = User.Identity.Name;
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
            var name = User.Identity.Name;
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
            var name = User.Identity.Name;
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
        public bool Delete(string ProductId)
        {
            var name = User.Identity.Name;
            var product = _sellerService.DeleteProduct(ProductId, name);
            return product;
        }
        public IActionResult Error()
        {
            return View();
        }
    }
}
