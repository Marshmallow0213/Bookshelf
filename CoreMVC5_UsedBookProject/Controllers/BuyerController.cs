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

namespace CoreMVC5_UsedBookProject.Controllers
{
    [Authorize(Roles = "Buyer")]
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
            var name = User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value;
            MySalesViewModel mymodel = new();
            ViewBag.trade = trade;
            mymodel = _buyerService.GetOrders(trade, status, now_page, name);
            return View(mymodel);
        }
        public IActionResult CreateOrder(string ProductId, string Sellername, string trade)
        {
            var buyername = User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value;
            var product = _context.Products.Where(w => w.ProductId == ProductId).FirstOrDefault();
            if (buyername == product.CreateBy)
            {
                return RedirectToAction("Details", "Home", new { ProductId = ProductId });
            }
            if (ProductId != null && Sellername != null)
            {
                _buyerService.CreateOrder(trade, Sellername, buyername, ProductId);
            }
            return RedirectToAction("MySales", new { Trade = trade });
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
        public IActionResult Wish()

        {
            var wishlist = (from w in _context.Wishes select new WishViewModel { Title = w.Title, ISBN = w.ISBN ,WishId=w.WishId,Id=w.Id}).ToList();
               
            return View(wishlist);
        }
        [HttpPost]
        public IActionResult AddBook(WishViewModel book)
        {
            var name = User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value;
            Wish bookWish = new Wish { Title=book.Title,ISBN=book.ISBN,Id=name};


            _context.Wishes.Add(bookWish);
            _context.SaveChanges();
            return RedirectToAction("Wish", new { });
        }
        public IActionResult DeleteBook(int Id)
        {
            var name = User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value;
            var wish = _context.Wishes.Where(w => w.WishId == Id).FirstOrDefault();
            

            _context.Wishes.Remove(wish);
            _context.SaveChanges();
            return RedirectToAction("Wish", new { });
        }
        public IActionResult OrderDetails(string OrderId, string trade)
        {
            var name = User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value;
            OrderViewModel mymodel = new();
            ViewBag.trade = trade;
            if (trade != "金錢" && trade != "以物易物")
            {
                return NotFound();
            }
            mymodel = _buyerService.GetOrder(OrderId, trade);
            return View(mymodel);
        }
        public IActionResult FinishOrder(string orderId, string trade)
        {
            _buyerService.FinishOrder(orderId);
            return RedirectToAction("MySales", new { status = "已完成", trade = trade });
        }
    }
}
