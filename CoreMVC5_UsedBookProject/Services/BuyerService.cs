using CoreMVC5_UsedBookProject.ViewModels;
using System.Collections.Generic;
using System;
using CoreMVC5_UsedBookProject.Data;
using CoreMVC5_UsedBookProject.Interfaces;
using CoreMVC5_UsedBookProject.Repositories;
using System.Linq;
using CoreMVC5_UsedBookProject.Models;

namespace CoreMVC5_UsedBookProject.Services
{
    public class BuyerService
    {
        private readonly ProductContext _context;
        private readonly SellerRepository _sellerRepository;
        private readonly IHashService _hashService;

        public BuyerService(ProductContext productContext, SellerRepository sellerRepository, IHashService hashService)
        {
            _context = productContext;
            _sellerRepository = sellerRepository;
            _hashService = hashService;
        }
        public ProductEditViewModel GetProduct(string id)
        {
            var product = _context.Products
            .Where(p => p.Status == "已上架"&&p.ProductId == id)
            .OrderByDescending(p => p.CreateDate)
            .Select(p => new Product
            {  ProductId=p.ProductId,
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
           .FirstOrDefault();
            var dm = _sellerRepository.DMToVM(product);
            return dm;
        }
        public MyProductsViewModel GetProducts(int now_page, string trade)
        {
            Dictionary<string, int> count = ProductsCountList(trade);
            now_page = now_page == 0 ? 1 : now_page;
            int all_pages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(count["已上架"]) / 10));
            var products = _context.Products
            .Where(p => p.Status == "已上架" && p.Trade == trade)
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
           .Skip((now_page - 1) * 10).Take(10).ToList();
            MyProductsViewModel mymodel = new()
            {
                Products = products,
                PagesCount = new int[] { now_page, all_pages }
            };
            return mymodel;
        }
        public Dictionary<string, int> ProductsCountList(string trade)
        {
            Dictionary<string, int> countList = new();
            countList = _context.Products.Where(w => w.Trade == $"{trade}").GroupBy(p => p.Status).Select(g => new { Status = g.Key, count = g.Count() }).ToDictionary(d => d.Status, d => d.count);
            Dictionary<string, int> count = new()
            {
                { "全部", 0 },
                { "未上架", 0 },
                { "待審核", 0 },
                { "已上架", 0 },
                { "已售完", 0 },
                { "封禁", 0 },
                { "刪除", 0 }
            };
            foreach (var item in countList.Keys)
            {
                count[item] = countList[item];
            }
            int all = 0;
            foreach (var item in count.Values)
            {
                all += item;
            }
            count["全部"] = all;
            return count;
        }
        public MySalesViewModel GetOrders(string trade, string status, int now_page, string name)
        {
            Dictionary<string, int> count = OrdersCountList("金錢", name);
            status ??= "待確認";
            now_page = now_page == 0 ? 1 : now_page;
            int all_pages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(count[status]) / 10));
            List<OrderViewModel> orders = new();
            orders = (from o in _context.Orders
                      from p in _context.Products
                      where o.ProductId == p.ProductId && o.Status == (status == "全部" ? o.Status : $"{status}") && o.BuyerId == $"{name}" && o.Trade == trade
                      orderby o.CreateDate descending
                      select new OrderViewModel { OrderId = o.OrderId, UnitPrice = o.UnitPrice, SellerId = o.SellerId, BuyerId = o.BuyerId, DenyReason = o.DenyReason, Status = o.Status, ProductId = p.ProductId, Title = p.Title, Image1 = p.Image1 }).Skip((now_page - 1) * 10).Take(10).ToList();
            MySalesViewModel mymodel = new()
            {
                Orders = orders,
                PagesCount = new int[] { now_page, all_pages },
                OrdersCount = count,
                StatusPage = status
            };
            return mymodel;
        }
        public Dictionary<string, int> OrdersCountList(string trade, string name)
        {
            Dictionary<string, int> countList = new();
            countList = _context.Orders.Where(w => w.BuyerId == name && w.Trade == trade).GroupBy(p => p.Status).Select(g => new { Status = g.Key, count = g.Count() }).ToDictionary(product => product.Status, product => product.count);
            Dictionary<string, int> count = new()
            {
                { "全部", 0 },
                { "待確認", 0 },
                { "已成立", 0 },
                { "不成立", 0 },
                { "待取消", 0 }
            };
            foreach (var item in countList.Keys)
            {
                count[item] = countList[item];
            }
            int all = 0;
            foreach (var item in count.Values)
            {
                all += item;
            }
            count["全部"] = all;
            return count;
        }
        public void CreateOrder(string trade, string sellername, string buyername, string ProductId)
        {
            string Id = $"{_hashService.RandomString(16)}";
            var checkOrderExist = (from p in _context.Orders
                                     where p.OrderId == $"{Id}"
                                     orderby p.CreateDate descending
                                     select new { p.OrderId }).FirstOrDefault();
            while (checkOrderExist != null)
            {
                Id = $"{_hashService.RandomString(16)}";
                checkOrderExist = (from p in _context.Orders
                                     where p.OrderId == $"{Id}"
                                     orderby p.CreateDate descending
                                     select new { p.OrderId }).FirstOrDefault();
            };
            Order order = new()
            {
                OrderId = Id,
                UnitPrice = 0,
                SellerId = sellername,
                BuyerId = buyername,
                DenyReason = "",
                ProductId = ProductId,
                Status = "待確認",
                Trade = trade,
                CreateDate = DateTime.Now
            };
            _context.Add(order);
            _context.SaveChanges();
        }
    }
}
