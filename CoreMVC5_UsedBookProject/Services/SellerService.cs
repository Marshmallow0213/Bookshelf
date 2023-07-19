using CoreMVC5_UsedBookProject.Data;
using CoreMVC5_UsedBookProject.Interfaces;
using CoreMVC5_UsedBookProject.Models;
using CoreMVC5_UsedBookProject.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CoreMVC5_UsedBookProject.Repositories;
using System.Xml.Linq;
using Microsoft.AspNetCore.Hosting;

namespace CoreMVC5_UsedBookProject.Services
{
    public class SellerService
    {
        private readonly ProductContext _context;
        private readonly int _limit;
        private readonly IHashService _hashService;
        private readonly SellerRepository _sellerRepository;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public SellerService(ProductContext productContext, IHashService hashService, SellerRepository sellerRepository, IWebHostEnvironment hostingEnvironment)
        {
            _context = productContext;
            _limit = 100;
            _hashService = hashService;
            _sellerRepository = sellerRepository;
            _hostingEnvironment = hostingEnvironment;
        }
        public List<Dictionary<string, int>> OrdersAllCountList(string name)
        {
            Dictionary<string, int> countM = OrdersCountList(name);
            Dictionary<string, int> countB = BarterOrdersCountList(name);
            List<Dictionary<string, int>> count = new() { countM, countB };
            return count;
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
        public bool CheckProductsStatus(string status)
        {
            switch (status)
            {
                case "全部":
                    return true;
                case "未上架":
                    return true;
                case "已上架":
                    return true;
                case "待確認":
                    return true;
                case "已售完":
                    return true;
                case "刪除":
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
        public bool CheckProductsTrade(string trade)
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
        public MySalesViewModel GetOrders(string status, string trade, int now_page, string name)
        {
            Dictionary<string, int> count = OrdersCountList(name);
            status ??= "待確認";
            now_page = now_page == 0 ? 1 : now_page;
            int all_pages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(count[status]) / 30));
            List<OrderViewModel> orders = new();
            var sellername = (from o in _context.Orders from u in _context.Users where o.SellerId == u.Id select u.Name).FirstOrDefault();
            var buyername = (from o in _context.Orders from u in _context.Users where o.BuyerId == u.Id select u.Name).FirstOrDefault();
            orders = (from o in _context.Orders
            from p in _context.Products
            where o.ProductId == p.ProductId && o.Status == (status == "全部" ? o.Status : $"{status}") && o.SellerId == $"{name}" && o.Trade.Contains(trade)
                      orderby o.CreateDate descending
                      select new OrderViewModel { OrderId = o.OrderId, SellerUnitPrice = o.UnitPrice, SellerId = o.SellerId, BuyerId = o.BuyerId, SellerName = sellername, BuyerName = buyername, DenyReason = o.DenyReason, Status = o.Status, Trade = o.Trade, SellerProductId = p.ProductId, SellerTitle = p.Title, SellerISBN = p.ISBN }).Skip((now_page - 1) * 30).Take(30).ToList();
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
        public MySalesViewModel GetBarterOrders(string status, string trade, int now_page, string name)
        {
            Dictionary<string, int> count = BarterOrdersCountList(name);
            status ??= "待確認";
            now_page = now_page == 0 ? 1 : now_page;
            int all_pages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(count[status]) / 30));
            List<BarterOrderViewModel> barterorders = new();
            var sellername = (from o in _context.BarterOrders from u in _context.Users where o.SellerId == u.Id select u.Name).FirstOrDefault();
            var buyername = (from o in _context.BarterOrders from u in _context.Users where o.BuyerId == u.Id select u.Name).FirstOrDefault();
            barterorders = (from o in _context.BarterOrders
                      from p in _context.Products
                      where o.SellerProductId == p.ProductId && o.Status == (status == "全部" ? o.Status : $"{status}") && o.SellerId == $"{name}" && o.Trade.Contains(trade)
                      orderby o.CreateDate descending
                      select new BarterOrderViewModel { OrderId = o.OrderId, SellerId = o.SellerId, BuyerId = o.BuyerId, SellerName = sellername, BuyerName = buyername, DenyReason = o.DenyReason, Status = o.Status, Trade = o.Trade, SellerProductId = o.SellerProductId, SellerTitle = p.Title, SellerISBN = p.ISBN, SellerAuthor = p.Author, BuyerProductId = o.BuyerProductId }).Skip((now_page - 1) * 30).Take(30).ToList();
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
        public Dictionary<string, int> OrdersCountList(string name)
        {
            Dictionary<string, int> countList = new();
            countList = _context.Orders.Where(w => w.SellerId == name).GroupBy(p => p.Status).Select(g => new { Status = g.Key, count = g.Count() }).ToDictionary(product => product.Status, product => product.count);
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
        public Dictionary<string, int> BarterOrdersCountList(string name)
        {
            Dictionary<string, int> countList = new();
            countList = _context.BarterOrders.Where(w => w.SellerId == name).GroupBy(p => p.Status).Select(g => new { Status = g.Key, count = g.Count() }).ToDictionary(product => product.Status, product => product.count);
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
        public MyProductsViewModel GetProducts(string status, int now_page, string name, string trade)
        {
            Dictionary<string, int> count = ProductsCountList(trade, name);
            status ??= "已上架";
            now_page = now_page == 0 ? 1 : now_page;
            int all_pages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(count[status]) / 30));
            var products = (from p in _context.Products
                           where p.Status == (status == "全部" ? p.Status : $"{status}") && p.CreateBy == $"{name}" && p.Trade.Contains(trade)
                           orderby p.CreateDate descending
                           select new ProductViewModel { ProductId = p.ProductId, Title = p.Title, ISBN = p.ISBN, Author = p.Author, Publisher = p.Publisher, Trade = p.Trade, Status = p.Status, UnitPrice = p.UnitPrice, CreateBy = p.CreateBy }).Skip((now_page - 1) * 30).Take(30).ToList();
            MyProductsViewModel mymodel = new()
            {
                Products = products,
                PagesCount = new int[] { now_page, all_pages },
                ProductsCount = count,
                StatusPage = status,
                ProductNewLimit = ProductNewLimit(name)
            };
            return mymodel;
        }
        public Dictionary<string, int> ProductsCountList(string trade, string name)
        {
            Dictionary<string, int> countList = new();
            countList = _context.Products.Where(w => w.CreateBy == name && w.Trade.Contains(trade)).GroupBy(p => p.Status).Select(g => new { Status = g.Key, count = g.Count() }).ToDictionary(d => d.Status, d => d.count);
            Dictionary<string, int> count = new()
            {
                { "全部", 0 },
                { "未上架", 0 },
                { "已上架", 0 },
                { "待確認", 0 },
                { "已售完", 0 },
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
        public int[] ProductNewLimit(string name)
        {
            Dictionary<string, int> countList = new();
            countList = _context.Products.Where(w => w.CreateBy == name && (w.Status == "未上架" || w.Status == "已上架")).GroupBy(p => p.Status).Select(g => new { Status = g.Key, count = g.Count() }).ToDictionary(d => d.Status, d => d.count);
            Dictionary<string, int> count = new()
            {
                { "未上架", 0 },
                { "已上架", 0 }
            };
            foreach (var item in countList.Keys)
            {
                count[item] = countList[item];
            }
            int all = 0;
            all = count["未上架"] + count["已上架"];
            int[] productNewLimit = { all, _limit };
            return productNewLimit;
        }
        public void CreateProduct(ProductViewModel ProductViewModel, string name)
        {
            List<string> randomstrings = new();
            for (int i = 1; i <= 9; i++)
            {
                randomstrings.Add(_hashService.RandomString(7));
            }
            List<IFormFile> filenames = new()
                {
                    ProductViewModel.File2 ?? null,
                    ProductViewModel.File3 ?? null,
                    ProductViewModel.File4 ?? null,
                    ProductViewModel.File5 ?? null,
                    ProductViewModel.File6 ?? null,
                    ProductViewModel.File7 ?? null,
                    ProductViewModel.File8 ?? null,
                    ProductViewModel.File9 ?? null
                };

            List<string> Images = new()
                {
                    ProductViewModel.Image2,
                    ProductViewModel.Image3,
                    ProductViewModel.Image4,
                    ProductViewModel.Image5,
                    ProductViewModel.Image6,
                    ProductViewModel.Image7,
                    ProductViewModel.Image8,
                    ProductViewModel.Image9
                };
            string[] checkImage = CheckImageName(filenames, Images, randomstrings);
            string Id = $"{_hashService.RandomString(16)}";
            var checkProductExist = (from p in _context.Products
                                     where p.ProductId == $"{Id}" && p.CreateBy == $"{name}"
                                     orderby p.CreateDate descending
                                     select new { p.ProductId }).FirstOrDefault();
            while (checkProductExist != null)
            {
                Id = $"{_hashService.RandomString(16)}";
                checkProductExist = (from p in _context.Products
                                     where p.ProductId == $"{Id}" && p.CreateBy == $"{name}"
                                     orderby p.CreateDate descending
                                     select new { p.ProductId }).FirstOrDefault();
            };
            decimal checkUnitPrice = -1000;
            if (ProductViewModel.Trade.Contains("買賣"))
            {
                checkUnitPrice = ProductViewModel.UnitPrice;
            }
            Product product = new()
            {
                ProductId = Id,
                Title = ProductViewModel.Title,
                ContentText = ProductViewModel.ContentText,
                ImageList = String.Join(",", checkImage),
                ISBN = ProductViewModel.ISBN,
                Author = ProductViewModel.Author,
                Publisher = ProductViewModel.Publisher,
                Degree = ProductViewModel.Degree,
                Status = ProductViewModel.Status,
                Trade = ProductViewModel.Trade,
                UnitPrice = checkUnitPrice,
                CreateDate = DateTime.Now,
                EditDate = DateTime.Now,
                TradingRemarque = ProductViewModel.TradingRemarque,
                CreateBy = name
            };
            _context.Add(product);
            _context.SaveChanges();
            UploadImages(filenames, Images, Id, randomstrings, name);
        }
        public string[] CheckImageName(List<IFormFile> filenames, List<string> Images, List<string> randomstrings)
        {
            string[] checkImage = { "", "", "", "", "", "", "", "" };
            for (int i = 0; i <= 7; i++)
            {
                checkImage[i] = filenames[i] == null ? Images[i] : String.Concat($"{randomstrings[i]}{i + 2}", Path.GetExtension(Convert.ToString(filenames[i].FileName)));
            }
            return checkImage;
        }
        public void UploadImages(List<IFormFile> filenames, List<string> Images, string ProductId, List<string> randomstrings, string Id)
        {
            string folderPath = $@"{_hostingEnvironment.WebRootPath}\Images\Users\{Id}\Products\{ProductId}";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            int i = 1;
            foreach (var file in Images)
            {
                if (file == "無圖片")
                {
                    string[] files = System.IO.Directory.GetFiles(folderPath, $"*{i++}.*");
                    foreach (string f in files)
                    {
                        System.IO.File.Delete(f);
                    }
                }
                else
                {
                    i++;
                }
            }
            i = 1;
            foreach (var file in filenames)
            {
                if (file != null)
                {
                    string[] files = System.IO.Directory.GetFiles(folderPath, $"{i}.*");
                    foreach (string f in files)
                    {
                        System.IO.File.Delete(f);
                    }
                    var path = $@"{folderPath}\{randomstrings[i - 1]}{i++}{Path.GetExtension(Convert.ToString(file.FileName))}";
                    using var stream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite, FileShare.None, 2097152);
                    file.CopyTo(stream);
                }
                else
                {
                    i++;
                }
            }
        }
        public ProductViewModel GetProduct(string ProductId, string name)
        {
            var product = _sellerRepository.GetProductRaw(ProductId);
            if (product == null)
            {
                return null;
            }
            var dm = _sellerRepository.DMToVM(product);
            var username = _context.Users.Where(w=>w.Id == dm.CreateBy).Select(s=>s.Name).FirstOrDefault();
            dm.CreateByName = username;
            return dm;
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
                     select new OrderViewModel { OrderId = o.OrderId, SellerId = o.SellerId, BuyerId = o.BuyerId, SellerName = sellername, BuyerName = buyername, DenyReason = o.DenyReason, Status = o.Status, Trade = o.Trade, SellerUnitPrice = o.UnitPrice, SellerProductId = p.ProductId, SellerTitle = p.Title, SellerISBN = p.ISBN, SellerAuthor = p.Author }).FirstOrDefault();
            return order;
        }
        public BarterOrderViewModel GetBarterOrder(string OrderId)
        {
            BarterOrderViewModel order = new();
            var sellername = (from o in _context.BarterOrders from u in _context.Users where o.SellerId == u.Id select u.Name).FirstOrDefault();
            var buyername = (from o in _context.BarterOrders from u in _context.Users where o.BuyerId == u.Id select u.Name).FirstOrDefault();
            var buyerid = (from o in _context.BarterOrders from u in _context.Users where o.BuyerId == u.Id select u.Id).FirstOrDefault();
            var products = (from p in _context.Products
                            where p.Status == "已上架" && p.CreateBy == $"{buyerid}" && p.Trade.Contains("交換")
                            orderby p.CreateDate descending
                            select new ProductViewModel { ProductId = p.ProductId, Title = p.Title, ISBN = p.ISBN, Author = p.Author, Publisher = p.Publisher, Degree = p.Degree, ContentText = p.ContentText, Image2 = p.ImageList, Status = p.Status, Trade = p.Trade, UnitPrice = p.UnitPrice, CreateDate = p.CreateDate, EditDate = p.EditDate, CreateBy = p.CreateBy }).ToList();
            var buyerproduct = (from o in _context.BarterOrders
                         from p in _context.Products
                         where o.BuyerProductId == p.ProductId && o.OrderId == OrderId
                         orderby o.CreateDate descending
                         select new BarterOrderViewModel { BuyerTitle = p.Title, BuyerISBN = p.ISBN, BuyerAuthor = p.Author }).FirstOrDefault();
            order = (from o in _context.BarterOrders
                     from p in _context.Products
                     where o.SellerProductId == p.ProductId && o.OrderId == OrderId
                     orderby o.CreateDate descending
                     select new BarterOrderViewModel { OrderId = o.OrderId, SellerId = o.SellerId, BuyerId = o.BuyerId, SellerName = sellername, BuyerName = buyername, DenyReason = o.DenyReason, Status = o.Status, Trade = o.Trade, SellerProductId = o.SellerProductId, SellerTitle = p.Title, SellerISBN = p.ISBN, SellerAuthor = p.Author, BuyerProductId = o.BuyerProductId, BuyerTitle = buyerproduct.BuyerTitle, BuyerISBN = buyerproduct.BuyerISBN, BuyerAuthor = buyerproduct.BuyerAuthor, Products = products }).FirstOrDefault();
            return order;
        }
        public void EditProduct(ProductViewModel ProductViewModel, string name)
        {
            List<string> randomstrings = new List<string>();
            for (int i = 1; i <= 9; i++)
            {
                randomstrings.Add(_hashService.RandomString(7));
            }
            List<IFormFile> filenames = new()
                {
                    ProductViewModel.File2 ?? null,
                    ProductViewModel.File3 ?? null,
                    ProductViewModel.File4 ?? null,
                    ProductViewModel.File5 ?? null,
                    ProductViewModel.File6 ?? null,
                    ProductViewModel.File7 ?? null,
                    ProductViewModel.File8 ?? null,
                    ProductViewModel.File9 ?? null
                };

            List<string> Images = new()
                {
                    ProductViewModel.Image2,
                    ProductViewModel.Image3,
                    ProductViewModel.Image4,
                    ProductViewModel.Image5,
                    ProductViewModel.Image6,
                    ProductViewModel.Image7,
                    ProductViewModel.Image8,
                    ProductViewModel.Image9
                };
            string[] checkImage = CheckImageName(filenames, Images, randomstrings);
            var product = _sellerRepository.GetProductRaw(ProductViewModel.ProductId);
            decimal checkUnitPrice = -1000;
            if (ProductViewModel.Trade.Contains("買賣"))
            {
                checkUnitPrice = ProductViewModel.UnitPrice;
            }
            _context.Entry(product).State = EntityState.Modified;
            product.ProductId = ProductViewModel.ProductId;
            product.Title = ProductViewModel.Title;
            product.ContentText = ProductViewModel.ContentText;
            product.ImageList = String.Join(",", checkImage);
            product.ISBN = ProductViewModel.ISBN;
            product.Author = ProductViewModel.Author;
            product.Publisher = ProductViewModel.Publisher;
            product.Degree = ProductViewModel.Degree;
            product.Status = ProductViewModel.Status;
            product.Trade = ProductViewModel.Trade;
            product.UnitPrice = checkUnitPrice;
            product.EditDate = DateTime.Now;
            product.TradingRemarque = ProductViewModel.TradingRemarque;
            _context.SaveChanges();
            UploadImages(filenames, Images, ProductViewModel.ProductId, randomstrings, name);
        }
        public bool DeleteProduct(string ProductId, string name)
        {
            var product = _sellerRepository.GetProductRaw(ProductId);
            if (product == null || product.CreateBy != name)
            {
                return false;
            }
            if (product.Status != "未上架")
            {
                return false;
            }
            _context.Entry(product).State = EntityState.Modified;
            product.Status = "刪除";
            product.EditDate = DateTime.Now;
            _context.Update(product);
            _context.SaveChanges();
            return true;
        }
        public void AcceptOrder(string orderId)
        {
            var order = _context.Orders.Where(w=>w.OrderId == orderId).FirstOrDefault();
            _context.Entry(order).State = EntityState.Modified;
            order.Status = "已成立";
            var product = _context.Products.Where(w => w.ProductId == order.ProductId).FirstOrDefault();
            _context.Entry(order).State = EntityState.Modified;
            product.Status = "已售完";
            _context.SaveChanges();
        }
        public void DenyOrder(string orderId)
        {
            var order = _context.Orders.Where(w => w.OrderId == orderId).FirstOrDefault();
            _context.Entry(order).State = EntityState.Modified;
            order.Status = "不成立";
            var product = _context.Products.Where(w => w.ProductId == order.ProductId).FirstOrDefault();
            _context.Entry(product).State = EntityState.Modified;
            product.Status = "已上架";
            _context.SaveChanges();
        }
        public void CancelOrder(string orderId)
        {
            var order = _context.Orders.Where(w => w.OrderId == orderId).FirstOrDefault();
            _context.Entry(order).State = EntityState.Modified;
            order.Status = "不成立";
            var product = _context.Products.Where(w => w.ProductId == order.ProductId).FirstOrDefault();
            _context.Entry(product).State = EntityState.Modified;
            product.Status = "已上架";
            _context.SaveChanges();
        }
        public void AcceptBarterOrder(string orderId, string ProductId)
        {
            var order = _context.BarterOrders.Where(w => w.OrderId == orderId).FirstOrDefault();
            _context.Entry(order).State = EntityState.Modified;
            order.Status = "已成立";
            order.BuyerProductId = ProductId;
            var product = _context.Products.Where(w => w.ProductId == order.SellerProductId).FirstOrDefault();
            _context.Entry(product).State = EntityState.Modified;
            product.Status = "已售完";
            var _product = _context.Products.Where(w => w.ProductId == order.BuyerProductId).FirstOrDefault();
            _context.Entry(_product).State = EntityState.Modified;
            _product.Status = "已售完";
            _context.SaveChanges();
        }
        public void DenyBarterOrder(string orderId)
        {
            var order = _context.BarterOrders.Where(w => w.OrderId == orderId).FirstOrDefault();
            _context.Entry(order).State = EntityState.Modified;
            order.Status = "不成立";
            var product = _context.Products.Where(w => w.ProductId == order.SellerProductId).FirstOrDefault();
            _context.Entry(product).State = EntityState.Modified;
            product.Status = "已上架";
            _context.SaveChanges();
        }
        public void CancelBarterOrder(string orderId)
        {
            var order = _context.BarterOrders.Where(w => w.OrderId == orderId).FirstOrDefault();
            _context.Entry(order).State = EntityState.Modified;
            order.Status = "不成立";
            var product = _context.Products.Where(w => w.ProductId == order.SellerProductId).FirstOrDefault();
            _context.Entry(product).State = EntityState.Modified;
            product.Status = "已上架";
            var _product = _context.Products.Where(w => w.ProductId == order.BuyerProductId).FirstOrDefault();
            _context.Entry(_product).State = EntityState.Modified;
            _product.Status = "已上架";
            _context.SaveChanges();
        }
    }
}
