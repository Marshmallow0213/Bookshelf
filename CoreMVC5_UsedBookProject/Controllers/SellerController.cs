using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Authorization;
using CoreMVC5_UsedBookProject.ViewModel;
using CoreMVC5_UsedBookProject.Services;
using System.Diagnostics;

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
            string name = User.Identity.Name;
            var count = _sellerService.OrdersAllCountList(name);
            ViewOrder mymodel = new()
            {
                OrdersCount = count
            };
            return View(mymodel);
        }
        public IActionResult MySales(String status, int now_page, string trade)
        {
            string name = User.Identity.Name;
            ViewOrder mymodel = new();
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
            string name = User.Identity.Name;
            ViewProduct mymodel = new();
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
            string name = User.Identity.Name;
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
                var name = User.Identity.Name;
                _sellerService.CreateProduct(productCreateViewModel, name);
                if (productCreateViewModel.Trade == "金錢")
                {
                    return RedirectToAction("MyProducts", new { trade = "金錢" });
                }
                else
                {
                    return RedirectToAction("MyProducts", new { trade = "以物易物" });
                }
            }
            else
            {
                return View(productCreateViewModel);
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
                var name = User.Identity.Name;
                _sellerService.EditProduct(productEditViewModel, name);
                if (productEditViewModel.Trade == "金錢")
                {
                    return RedirectToAction("MyProducts", new { trade = "金錢" });
                }
                else
                {
                    return RedirectToAction("MyProducts", new { trade = "以物易物" });
                }
            }
            else
            {
                return View(productEditViewModel);
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
        public IActionResult Delete(string ProductId)
        {
            var name = User.Identity.Name;
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
        public IActionResult Delete(ProductEditViewModel productEditViewModel)
        {
            var name = User.Identity.Name;
            _sellerService.DeleteProduct(productEditViewModel, name);
            if (productEditViewModel.Trade == "金錢")
            {
                return RedirectToAction("MyProducts", new { trade = "金錢" });
            }
            else
            {
                return RedirectToAction("MyProducts", new { trade = "以物易物" });
            }
        }
        public IActionResult Error()
        {
            return View();
        }
        
    }
}
