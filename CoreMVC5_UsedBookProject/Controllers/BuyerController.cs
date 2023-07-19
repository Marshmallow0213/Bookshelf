using CoreMVC5_UsedBookProject.Data;
using CoreMVC5_UsedBookProject.Models;
using CoreMVC5_UsedBookProject.Services;
using CoreMVC5_UsedBookProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVC5_UsedBookProject.Controllers
{
    [Authorize(Roles = "User")]
    public class BuyerController : Controller
    {
        private readonly BuyerService _buyerService;
        private readonly ProductContext _context;

        public BuyerController(BuyerService buyerService, ProductContext productContext)
        {
            _buyerService = buyerService;
            _context = productContext;
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
        public IActionResult MySales(String status, int now_page, string trade)
        {
            var checkStatus = _buyerService.CheckOrdersStatus(status);
            if (!checkStatus)
            {
                return NotFound();
            }
            var checkTrade = _buyerService.CheckOrdersTrade(trade);
            if (!checkTrade)
            {
                return NotFound();
            }
            var name = User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value;
            MySalesViewModel mymodel = new();
            ViewBag.trade = trade;
            if (trade == "買賣")
            {
                mymodel = _buyerService.GetOrders(trade, status, now_page, name);
            }
            else if (trade == "交換")
            {
                mymodel = _buyerService.GetBarterOrders(trade, status, now_page, name);
            }
            return View(mymodel);
        }
        public IActionResult CreateOrder(string ProductId, string Sellername, string trade, string Order)
        {
            if(Order != "買賣" && Order != "交換")
            {
                return RedirectToAction("Details", "Home", new { ProductId = ProductId });
            }
            var buyername = User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value;
            var product = _context.Products.Where(w => w.ProductId == ProductId).FirstOrDefault();
            var checkselfproducts = _context.Products.Where(w => w.CreateBy == buyername && w.Status == "已上架" && w.Trade.Contains("交換")).FirstOrDefault();
            if (buyername == product.CreateBy)
            {
                TempData["Message"] = "你不能購買自己的商品!";
                return RedirectToAction("Details", "Home", new { ProductId = ProductId });
            }
            if (checkselfproducts == null && Order == "交換")
            {
                TempData["Message"] = "你需要先上架自己的交換商品!";
                return RedirectToAction("Details", "Home", new { ProductId = ProductId });
            }
            if (ProductId != null && Sellername != null)
            {
                if (Order == "買賣")
                {
                    _buyerService.CreateOrder(trade, Sellername, buyername, ProductId);
                }
                else if (Order == "交換")
                {
                    _buyerService.CreateBarterOrder(trade, Sellername, buyername, ProductId);
                }
            }
            return RedirectToAction("MySales", new { Trade = Order });
        }
        private List<ProductViewModel> GetCartItems(string name)
        {
            var cartItems = (from s in _context.Shoppingcarts
                             from p in _context.Products
                             where s.ProductId == p.ProductId && p.Status == "已上架" && s.Id == name
                             select new ProductViewModel
                             {
                                 ProductId = p.ProductId,
                                 Title = p.Title,
                                 ISBN = p.ISBN,
                                 Author = p.Author,
                                 Publisher = p.Publisher,
                                 Degree = p.Degree,
                                 ContentText = p.ContentText,
                                 Image2 = p.ImageList,
                                 Status = p.Status,
                                 Trade = p.Trade,
                                 UnitPrice = p.UnitPrice,
                                 CreateDate = p.CreateDate,
                                 EditDate = p.EditDate,
                                 CreateBy = p.CreateBy
                             }).ToList();
            return cartItems;
        }
        public IActionResult Shoppingcart()
        {
            var name = User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value;
            List<ProductViewModel> cartItems = GetCartItems(name);

            MyProductsViewModel mymodel = new()
            {
                Products = cartItems
            };
            return View(mymodel);
        }
        public string AddToShoppingcart(string id)
        {
            var name = User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value;
            var exist = _context.Shoppingcarts.Where(w=>w.ProductId == id && w.Id == name).FirstOrDefault();
            var product = _context.Products.Where(w => w.ProductId == id).FirstOrDefault();
            if (name == product.CreateBy)
            {
                return "自己的商品";
            }
            if (exist == null)
            {
                Shoppingcart shoppingcart = new()
                {
                    ProductId = id,
                    Id = name
                };
                _context.Shoppingcarts.Add(shoppingcart);
                _context.SaveChanges();
                return "成功";
            }
            return "失敗";
        }
        public IActionResult DeleteFromShoppingcart(string ProductId)
        {
            var name = User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value;
            var exist = _context.Shoppingcarts.Where(w => w.ProductId == ProductId && w.Id == name).FirstOrDefault();
            _context.Shoppingcarts.Remove(exist);
            _context.SaveChanges();
            return RedirectToAction("Shoppingcart", new {});
        }
        public IActionResult Wish(int now_page)
        {
            var name = User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value;
            now_page = now_page == 0 ? 1 : now_page;
            var countWish = _context.Wishes.Where(w => w.Id == name).Select(g => new { g.WishId }).ToList();
            int all_pages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(countWish.Count) / 30));
            var wishlist = (from w in _context.Wishes from u in _context.Users where w.Id == u.Id select new WishViewModel { Title = w.Title, ISBN = w.ISBN, WishId = w.WishId, UserName = u.Name }).Skip((now_page - 1) * 30).Take(30).ToList();
            List<string> ISBNproducts = new();
            ISBNproducts = _context.Products
                    .Where(p => p.Status == "已上架")
                    .OrderByDescending(p => p.CreateDate)
                    .Select(p => p.ISBN
                    )
                    .ToList();
            List<string> Titleproducts = new();
            Titleproducts = _context.Products
                .Where(p => p.Status == "已上架")
                .OrderByDescending(p => p.CreateDate)
                .Select(p => p.Title
                )
                .ToList();
            WishsViewModel wishs = new()
            {
                Wishs = wishlist,
                ISBNproducts = ISBNproducts,
                Titleproducts = Titleproducts,
                PagesCount = new int[] { now_page, all_pages }
            };
            return View(wishs);
        }
       
        [HttpPost]
        public IActionResult Wish(WishsViewModel book)
        {
            if (ModelState.IsValid)
            {
                var name = User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value;
                Wish bookWish = new Wish { Title = book.Title, ISBN = book.ISBN, Id = name };
                _context.Wishes.Add(bookWish);
                _context.SaveChanges();
                return RedirectToAction("Wish", new {});
            }
            else {
                return RedirectToAction("Wish", new {});
            }
        }

        public IActionResult DeleteBook(int Id)
        {
            var name = User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value;
            var wish = _context.Wishes.Where(w => w.WishId == Id && w.Id == name).FirstOrDefault();
            _context.Wishes.Remove(wish);
            _context.SaveChanges();
            return RedirectToAction("Wish", new { });
        }
        public IActionResult OrderDetails(string OrderId, string trade)
        {
            var name = User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value;
            OrderViewModel mymodel = new();
            ViewBag.trade = trade;
            mymodel = _buyerService.GetOrder(OrderId);
            if(mymodel == null || mymodel.BuyerId != name)
            {
                return NotFound();
            }
            return View(mymodel);
        }
        [HttpPost]
        public IActionResult OrderDetails(OrderViewModel orderViewModel, string submit)
        {
            if (submit == "完成訂單")
            {
                _buyerService.FinishOrder(orderViewModel.OrderId);
                return RedirectToAction("MySales", new { status = "已完成", trade = "買賣" });
            }
            if (ModelState.IsValid && submit == "取消訂單")
            {
                _buyerService.CancelOrder(orderViewModel.OrderId, orderViewModel.DenyReason);
                return RedirectToAction("MySales", new { status = "待取消", trade = "買賣" });
            }
            var name = User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value;
            OrderViewModel mymodel = new();
            ViewBag.trade = orderViewModel.Trade;
            mymodel = _buyerService.GetOrder(orderViewModel.OrderId);
            return View(mymodel);
        }
        public IActionResult BarterOrderDetails(string OrderId, string trade)
        {
            var name = User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value;
            BarterOrderViewModel mymodel = new();
            ViewBag.trade = trade;
            mymodel = _buyerService.GetBarterOrder(OrderId);
            if (mymodel == null || mymodel.BuyerId != name)
            {
                return NotFound();
            }
            return View(mymodel);
        }
        [HttpPost]
        public IActionResult BarterOrderDetails(OrderViewModel orderViewModel, string submit)
        {
            if(submit == "完成訂單")
            {
                _buyerService.FinishBarterOrder(orderViewModel.OrderId);
                return RedirectToAction("MySales", new { status = "已完成", trade = "交換" });
            }
            if (ModelState.IsValid && submit == "取消訂單")
            {
                _buyerService.CancelBarterOrder(orderViewModel.OrderId, orderViewModel.DenyReason);
                return RedirectToAction("MySales", new { status = "待取消", trade = "交換" });
            }
            var name = User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value;
            BarterOrderViewModel mymodel = new();
            ViewBag.trade = orderViewModel.Trade;
            mymodel = _buyerService.GetBarterOrder(orderViewModel.OrderId);
            return View(mymodel);
        }
    }
}
