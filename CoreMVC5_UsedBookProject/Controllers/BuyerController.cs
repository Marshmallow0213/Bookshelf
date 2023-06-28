﻿using CoreMVC5_UsedBookProject.Data;
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
            if (ProductId != null && Sellername != null)
            {
                var buyername = User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value;
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
        public bool AddToShoppingcart(string id)
        {
            var name = User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value;
            var exist = _context.Shoppingcarts.Where(w=>w.ProductId == id && w.Id == name).FirstOrDefault();
            if (exist == null)
            {
                Shoppingcart shoppingcart = new()
                {
                    ProductId = id,
                    Id = name
                };
                _context.Shoppingcarts.Add(shoppingcart);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        public IActionResult DeleteFromShoppingcart(string ProductId)
        {
            var name = User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value;
            var exist = _context.Shoppingcarts.Where(w => w.ProductId == ProductId && w.Id == name).FirstOrDefault();
            _context.Shoppingcarts.Remove(exist);
            _context.SaveChanges();
            return RedirectToAction("Shoppingcart", new {});
        }
    }
}
