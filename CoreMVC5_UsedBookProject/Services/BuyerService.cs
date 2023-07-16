using CoreMVC5_UsedBookProject.ViewModels;
using System.Collections.Generic;
using System;
using CoreMVC5_UsedBookProject.Data;
using CoreMVC5_UsedBookProject.Interfaces;
using CoreMVC5_UsedBookProject.Repositories;
using System.Linq;
using CoreMVC5_UsedBookProject.Models;
using Microsoft.EntityFrameworkCore;

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
        public bool CheckOrdersStatus(string status)
        {
            switch (status)
            {
                case "全部":
                    return true;
                case "待確認":
                    return true;
                case "已成立":
                    return true;
                case "不成立":
                    return true;
                case "已完成":
                    return true;
                case "待取消":
                    return true;
                case null:
                    return true;
                default:
                    return false;
            }
        }
        public bool CheckOrdersTrade(string trade)
        {
            switch (trade)
            {
                case "買賣":
                    return true;
                case "交換":
                    return true;
                default:
                    return false;
            }
        }
        public ProductEditViewModel GetProduct(string id)
        {
            var product = _context.Products
            .Where(p=>p.ProductId == id)
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
                TradingPlaceAndTime = p.TradingPlaceAndTime,
                CreateBy = p.CreateBy
            })
           .FirstOrDefault();
            var vm = _sellerRepository.DMToVM(product);
            var username = _context.Users.Where(w => w.Id == vm.CreateBy).Select(s => s.Name).FirstOrDefault();
            vm.CreateByName = username;
            return vm;
        }
        public MyProductsViewModel GetProducts(int now_page, string trade)
        {
            Dictionary<string, int> count = ProductsCountList(trade);
            now_page = now_page == 0 ? 1 : now_page;
            int all_pages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(count["已上架"]) / 10));
            var products = _context.Products
            .Where(p => p.Status == "已上架" && p.Trade.Contains(trade))
            .OrderByDescending(p => p.CreateDate)
            .Select(p => new ProductViewModel
            {
                ProductId = p.ProductId,
                Title = p.Title,
                ISBN = p.ISBN,
                Author = p.Author,
                ContentText = p.ContentText,
                Image1 = p.Image1,
                Trade = p.Trade,
                UnitPrice = p.UnitPrice
            })
           .Skip((now_page - 1) * 30).Take(30).ToList();
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
            countList = _context.Products.Where(w => w.Trade.Contains(trade)).GroupBy(p => p.Status).Select(g => new { Status = g.Key, count = g.Count() }).ToDictionary(d => d.Status, d => d.count);
            Dictionary<string, int> count = new()
            {
                { "全部", 0 },
                { "未上架", 0 },
                { "已上架", 0 },
                { "待確認", 0 },
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
            Dictionary<string, int> count = OrdersCountList(trade, name);
            status ??= "待確認";
            now_page = now_page == 0 ? 1 : now_page;
            int all_pages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(count[status]) / 10));
            List<OrderViewModel> orders = new();
            var sellername = (from o in _context.Orders from u in _context.Users where o.SellerId == u.Id select u.Name).FirstOrDefault();
            var buyername = (from o in _context.Orders from u in _context.Users where o.BuyerId == u.Id select u.Name).FirstOrDefault();
            orders = (from o in _context.Orders
                      from p in _context.Products
                      where o.ProductId == p.ProductId && o.Status == (status == "全部" ? o.Status : $"{status}") && o.BuyerId == $"{name}" && o.Trade.Contains(trade)
                      orderby o.CreateDate descending
                      select new OrderViewModel { OrderId = o.OrderId, SellerUnitPrice = o.UnitPrice, SellerId = o.SellerId, BuyerId = o.BuyerId, SellerName = sellername, BuyerName = buyername, DenyReason = o.DenyReason, Status = o.Status, Trade = o.Trade, SellerProductId = p.ProductId, SellerTitle = p.Title, SellerImage1 = p.Image1 }).Skip((now_page - 1) * 30).Take(30).ToList();
            MySalesViewModel mymodel = new()
            {
                Orders = orders,
                BarterOrders = new List<BarterOrderViewModel>() { },
                PagesCount = new int[] { now_page, all_pages },
                OrdersCount = count,
                StatusPage = status
            };
            return mymodel;
        }
        public MySalesViewModel GetBarterOrders(string trade, string status, int now_page, string name)
        {
            Dictionary<string, int> count = BarterOrdersCountList(trade, name);
            status ??= "待確認";
            now_page = now_page == 0 ? 1 : now_page;
            int all_pages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(count[status]) / 10));
            List<BarterOrderViewModel> barterorders = new();
            var sellername = (from o in _context.BarterOrders from u in _context.Users where o.SellerId == u.Id select u.Name).FirstOrDefault();
            var buyername = (from o in _context.BarterOrders from u in _context.Users where o.BuyerId == u.Id select u.Name).FirstOrDefault();
            barterorders = (from o in _context.BarterOrders
                      from p in _context.Products
                      where o.SellerProductId == p.ProductId && o.Status == (status == "全部" ? o.Status : $"{status}") && o.BuyerId == $"{name}" && o.Trade.Contains(trade)
                      orderby o.CreateDate descending
                            select new BarterOrderViewModel { OrderId = o.OrderId, SellerId = o.SellerId, BuyerId = o.BuyerId, SellerName = sellername, BuyerName = buyername, DenyReason = o.DenyReason, Status = o.Status, Trade = o.Trade, SellerProductId = o.SellerProductId, SellerTitle = p.Title, SellerISBN = p.ISBN, SellerAuthor = p.Author, SellerImage1 = p.Image1, BuyerProductId = o.BuyerProductId }).Skip((now_page - 1) * 30).Take(30).ToList();
            MySalesViewModel mymodel = new()
            {
                Orders = new List<OrderViewModel>() { },
                BarterOrders = barterorders,
                PagesCount = new int[] { now_page, all_pages },
                OrdersCount = count,
                StatusPage = status
            };
            return mymodel;
        }
        public Dictionary<string, int> OrdersCountList(string trade, string name)
        {
            Dictionary<string, int> countList = new();
            countList = _context.Orders.Where(w => w.BuyerId == name && w.Trade.Contains(trade)).GroupBy(p => p.Status).Select(g => new { Status = g.Key, count = g.Count() }).ToDictionary(product => product.Status, product => product.count);
            Dictionary<string, int> count = new()
            {
                { "全部", 0 },
                { "待確認", 0 },
                { "已成立", 0 },
                { "不成立", 0 },
                { "已完成", 0 },
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
        public Dictionary<string, int> BarterOrdersCountList(string trade, string name)
        {
            Dictionary<string, int> countList = new();
            countList = _context.BarterOrders.Where(w => w.BuyerId == name && w.Trade.Contains(trade)).GroupBy(p => p.Status).Select(g => new { Status = g.Key, count = g.Count() }).ToDictionary(product => product.Status, product => product.count);
            Dictionary<string, int> count = new()
            {
                { "全部", 0 },
                { "待確認", 0 },
                { "已成立", 0 },
                { "不成立", 0 },
                { "已完成", 0 },
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
            var product = _context.Products.Where(w => w.ProductId == ProductId).FirstOrDefault();
            _context.Entry(product).State = EntityState.Modified;
            product.Status = "待確認";
            Order order = new()
            {
                OrderId = Id,
                UnitPrice = product.UnitPrice,
                SellerId = sellername,
                BuyerId = buyername,
                DenyReason = "",
                ProductId = ProductId,
                Status = "待確認",
                Trade = trade,
                CreateDate = DateTime.Now
            };
            _context.Add(order);
            var exist = _context.Shoppingcarts.Where(w => w.ProductId == ProductId).ToList();
            foreach(var i in exist)
            {
                _context.Shoppingcarts.Remove(i);
            }
            _context.SaveChanges();
        }
        public void CreateBarterOrder(string trade, string sellername, string buyername, string ProductId)
        {
            string Id = $"{_hashService.RandomString(16)}";
            var checkOrderExist = (from p in _context.BarterOrders
                                   where p.OrderId == $"{Id}"
                                   orderby p.CreateDate descending
                                   select new { p.OrderId }).FirstOrDefault();
            while (checkOrderExist != null)
            {
                Id = $"{_hashService.RandomString(16)}";
                checkOrderExist = (from p in _context.BarterOrders
                                   where p.OrderId == $"{Id}"
                                   orderby p.CreateDate descending
                                   select new { p.OrderId }).FirstOrDefault();
            };
            var product = _context.Products.Where(w => w.ProductId == ProductId).FirstOrDefault();
            _context.Entry(product).State = EntityState.Modified;
            product.Status = "待確認";
            BarterOrder barterOrder = new()
            {
                OrderId = Id,
                SellerId = sellername,
                BuyerId = buyername,
                DenyReason = "",
                SellerProductId = ProductId,
                BuyerProductId = ProductId,
                Status = "待確認",
                Trade = trade,
                CreateDate = DateTime.Now
            };
            _context.Add(barterOrder);
            var exist = _context.Shoppingcarts.Where(w => w.ProductId == ProductId).ToList();
            foreach (var i in exist)
            {
                _context.Shoppingcarts.Remove(i);
            }
            _context.SaveChanges();
        }
        public OrderViewModel GetOrder(string OrderId)
        {
            OrderViewModel order = new();
            var sellername = (from o in _context.Orders from u in _context.Users where o.SellerId == u.Id select u.Name).FirstOrDefault();
            var buyername = (from o in _context.Orders from u in _context.Users where o.BuyerId == u.Id select u.Name).FirstOrDefault();
            order = (from o in _context.Orders
                     from p in _context.Products
                     where o.ProductId == p.ProductId && o.OrderId == OrderId
                     orderby o.CreateDate descending
                     select new OrderViewModel { OrderId = o.OrderId, SellerId = o.SellerId, BuyerId = o.BuyerId, SellerName = sellername, BuyerName = buyername, DenyReason = o.DenyReason, Status = o.Status, Trade = o.Trade, SellerUnitPrice = o.UnitPrice, SellerProductId = p.ProductId, SellerTitle = p.Title, SellerImage1 = p.Image1, SellerISBN = p.ISBN, SellerAuthor = p.Author }).FirstOrDefault();
            return order;
        }
        public BarterOrderViewModel GetBarterOrder(string OrderId)
        {
            BarterOrderViewModel order = new();
            var sellername = (from o in _context.BarterOrders from u in _context.Users where o.SellerId == u.Id select u.Name).FirstOrDefault();
            var buyername = (from o in _context.BarterOrders from u in _context.Users where o.BuyerId == u.Id select u.Name).FirstOrDefault();
            var buyerproduct = (from o in _context.BarterOrders
                                from p in _context.Products
                                where o.BuyerProductId == p.ProductId && o.OrderId == OrderId
                                orderby o.CreateDate descending
                                select new BarterOrderViewModel { BuyerTitle = p.Title, BuyerISBN = p.ISBN, BuyerAuthor = p.Author, BuyerImage1 = p.Image1 }).FirstOrDefault();
            order = (from o in _context.BarterOrders
                     from p in _context.Products
                     where o.SellerProductId == p.ProductId && o.OrderId == OrderId
                     orderby o.CreateDate descending
                     select new BarterOrderViewModel { OrderId = o.OrderId, SellerId = o.SellerId, BuyerId = o.BuyerId, SellerName = sellername, BuyerName = buyername, DenyReason = o.DenyReason, Status = o.Status, Trade = o.Trade, SellerProductId = o.SellerProductId, SellerTitle = p.Title, SellerISBN = p.ISBN, SellerAuthor = p.Author, SellerImage1 = p.Image1, BuyerProductId = o.BuyerProductId, BuyerTitle = buyerproduct.BuyerTitle, BuyerISBN = buyerproduct.BuyerISBN, BuyerAuthor = buyerproduct.BuyerAuthor, BuyerImage1 = buyerproduct.BuyerImage1 }).FirstOrDefault();
            return order;
        }
        public void FinishOrder(string orderId)
        {
            var order = _context.Orders.Where(w => w.OrderId == orderId).FirstOrDefault();
            _context.Entry(order).State = EntityState.Modified;
            order.Status = "已完成";
            _context.SaveChanges();
        }
        public void CancelOrder(string orderId, string DenyReason)
        {
            var order = _context.Orders.Where(w => w.OrderId == orderId).FirstOrDefault();
            _context.Entry(order).State = EntityState.Modified;
            order.Status = "待取消";
            order.DenyReason = DenyReason;
            _context.SaveChanges();
        }
        public void FinishBarterOrder(string orderId)
        {
            var order = _context.BarterOrders.Where(w => w.OrderId == orderId).FirstOrDefault();
            _context.Entry(order).State = EntityState.Modified;
            order.Status = "已完成";
            _context.SaveChanges();
        }
        public void CancelBarterOrder(string orderId, string DenyReason)
        {
            var order = _context.BarterOrders.Where(w => w.OrderId == orderId).FirstOrDefault();
            _context.Entry(order).State = EntityState.Modified;
            order.Status = "待取消";
            order.DenyReason = DenyReason;
            _context.SaveChanges();
        }
    }
}
