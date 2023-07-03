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

namespace CoreMVC5_UsedBookProject.Services
{
    public class SellerService
    {
        private readonly ProductContext _context;
        private readonly int _limit;
        private readonly IHashService _hashService;
        private readonly SellerRepository _sellerRepository;
        public SellerService(ProductContext productContext, IHashService hashService, SellerRepository sellerRepository)
        {
            _context = productContext;
            _limit = 100;
            _hashService = hashService;
            _sellerRepository = sellerRepository;
        }
        public List<Dictionary<string, int>> OrdersAllCountList(string name)
        {
            Dictionary<string, int> countM = OrdersCountList("金錢", name);
            Dictionary<string, int> countB = OrdersCountList("以物易物", name);
            List<Dictionary<string, int>> count = new() { countM, countB };
            return count;
        }
        public MySalesViewModel GetOrders(string status, string trade, int now_page, string name)
        {
            Dictionary<string, int> count = OrdersCountList(trade, name);
            status ??= "待確認";
            now_page = now_page == 0 ? 1 : now_page;
            int all_pages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(count[status]) / 10));
            List<OrderViewModel> orders = new();
            orders = (from o in _context.Orders
            from p in _context.Products
            where o.ProductId == p.ProductId && o.Status == (status == "全部" ? o.Status : $"{status}") && o.SellerId == $"{name}" && o.Trade == trade
                      orderby o.CreateDate descending
                      select new OrderViewModel { OrderId = o.OrderId, UnitPrice = o.UnitPrice, SellerId = o.SellerId, BuyerId = o.BuyerId, DenyReason = o.DenyReason, Status = o.Status, Trade = o.Trade, ProductId = p.ProductId, Title = p.Title, Image1 = p.Image1 }).Skip((now_page - 1) * 30).Take(30).ToList();
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
            countList = _context.Orders.Where(w => w.SellerId == name && w.Trade == trade).GroupBy(p => p.Status).Select(g => new { Status = g.Key, count = g.Count() }).ToDictionary(product => product.Status, product => product.count);
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
            int all_pages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(count[status]) / 10));
            var products = (from p in _context.Products
                           where p.Status == (status == "全部" ? p.Status : $"{status}") && p.CreateBy == $"{name}" && p.Trade == $"{trade}"
                           orderby p.CreateDate descending
                           select new ProductViewModel { ProductId = p.ProductId, Title = p.Title, ISBN = p.ISBN, Author = p.Author, Publisher = p.Publisher, PublicationDate = p.PublicationDate, Degree = p.Degree, ContentText = p.ContentText, Image1 = p.Image1, Image2 = p.Image2, Status = p.Status, Trade = p.Trade, UnitPrice = p.UnitPrice, CreateDate = p.CreateDate, EditDate = p.EditDate, CreateBy = p.CreateBy }).Skip((now_page - 1) * 30).Take(30).ToList();
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
            countList = _context.Products.Where(w => w.CreateBy == name && w.Trade == $"{trade}").GroupBy(p => p.Status).Select(g => new { Status = g.Key, count = g.Count() }).ToDictionary(d => d.Status, d => d.count);
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
        public void CreateProduct(ProductCreateViewModel productCreateViewModel, string name)
        {
            List<string> randomstrings = new();
            for (int i = 1; i <= 9; i++)
            {
                randomstrings.Add(_hashService.RandomString(7));
            }
            List<IFormFile> filenames = new()
                {
                    productCreateViewModel.File1 ?? null,
                    productCreateViewModel.File2 ?? null,
                    productCreateViewModel.File3 ?? null,
                    productCreateViewModel.File4 ?? null,
                    productCreateViewModel.File5 ?? null,
                    productCreateViewModel.File6 ?? null,
                    productCreateViewModel.File7 ?? null,
                    productCreateViewModel.File8 ?? null,
                    productCreateViewModel.File9 ?? null
                };

            List<string> Images = new()
                {
                    productCreateViewModel.Image1,
                    productCreateViewModel.Image2,
                    productCreateViewModel.Image3,
                    productCreateViewModel.Image4,
                    productCreateViewModel.Image5,
                    productCreateViewModel.Image6,
                    productCreateViewModel.Image7,
                    productCreateViewModel.Image8,
                    productCreateViewModel.Image9
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
            decimal checkUnitPrice = -1;
            if (productCreateViewModel.Trade == "金錢")
            {
                checkUnitPrice = productCreateViewModel.UnitPrice;
            }
            Product product = new()
            {
                ProductId = Id,
                Title = productCreateViewModel.Title,
                ContentText = productCreateViewModel.ContentText,
                Image1 = checkImage[0],
                Image2 = String.Join(",", checkImage[1..]),
                ISBN = productCreateViewModel.ISBN,
                Author = productCreateViewModel.Author,
                Publisher = productCreateViewModel.Publisher,
                PublicationDate = productCreateViewModel.PublicationDate,
                Degree = productCreateViewModel.Degree,
                Status = productCreateViewModel.Status,
                Trade = productCreateViewModel.Trade,
                UnitPrice = checkUnitPrice,
                CreateDate = DateTime.Now,
                EditDate = DateTime.Now,
                TradingPlaceAndTime = productCreateViewModel.TradingPlaceAndTime,
                CreateBy = name
            };
            _context.Add(product);
            _context.SaveChanges();
            UploadImages(filenames, Images, Id, randomstrings);
        }
        public string[] CheckImageName(List<IFormFile> filenames, List<string> Images, List<string> randomstrings)
        {
            string[] checkImage = { "", "", "", "", "", "", "", "", "" };
            for (int i = 0; i <= 8; i++)
            {
                checkImage[i] = filenames[i] == null ? Images[i] : String.Concat($"{randomstrings[i]}{i + 1}", Path.GetExtension(Convert.ToString(filenames[i].FileName)));
            }
            return checkImage;
        }
        public void UploadImages(List<IFormFile> filenames, List<string> Images, string ProductId, List<string> randomstrings)
        {
            string folderPath = $@"Images\Products\{ProductId}";
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
        public ProductEditViewModel GetProduct(string ProductId, string name)
        {
            var product = _sellerRepository.GetProductRaw(ProductId, name);
            var dm = _sellerRepository.DMToVM(product);
            return dm;
        }
        public OrderViewModel GetOrder(string OrderId, string trade)
        {
            OrderViewModel order = new();
            order = (from o in _context.Orders
                     from p in _context.Products
                     where o.ProductId == p.ProductId && o.OrderId == OrderId
                     orderby o.CreateDate descending
                     select new OrderViewModel { OrderId = o.OrderId, SellerId = o.SellerId, BuyerId = o.BuyerId, DenyReason = o.DenyReason, Status = o.Status, Trade = o.Trade, UnitPrice = o.UnitPrice, ProductId = p.ProductId, Title = p.Title, Image1 = p.Image1 }).FirstOrDefault();
            return order;
        }
        public void EditProduct(ProductEditViewModel productEditViewModel, string name)
        {
            List<string> randomstrings = new List<string>();
            for (int i = 1; i <= 9; i++)
            {
                randomstrings.Add(_hashService.RandomString(7));
            }
            List<IFormFile> filenames = new()
                {
                    productEditViewModel.File1 ?? null,
                    productEditViewModel.File2 ?? null,
                    productEditViewModel.File3 ?? null,
                    productEditViewModel.File4 ?? null,
                    productEditViewModel.File5 ?? null,
                    productEditViewModel.File6 ?? null,
                    productEditViewModel.File7 ?? null,
                    productEditViewModel.File8 ?? null,
                    productEditViewModel.File9 ?? null
                };

            List<string> Images = new()
                {
                    productEditViewModel.Image1,
                    productEditViewModel.Image2,
                    productEditViewModel.Image3,
                    productEditViewModel.Image4,
                    productEditViewModel.Image5,
                    productEditViewModel.Image6,
                    productEditViewModel.Image7,
                    productEditViewModel.Image8,
                    productEditViewModel.Image9
                };
            string[] checkImage = CheckImageName(filenames, Images, randomstrings);
            var product = _sellerRepository.GetProductRaw(productEditViewModel.ProductId, name);
            decimal checkUnitPrice = -1;
            if (productEditViewModel.Trade == "金錢")
            {
                checkUnitPrice = productEditViewModel.UnitPrice;
            }
            _context.Entry(product).State = EntityState.Modified;
            product.ProductId = productEditViewModel.ProductId;
            product.Title = productEditViewModel.Title;
            product.ContentText = productEditViewModel.ContentText;
            product.Image1 = checkImage[0];
            product.Image2 = String.Join(",", checkImage[1..]);
            product.ISBN = productEditViewModel.ISBN;
            product.Author = productEditViewModel.Author;
            product.Publisher = productEditViewModel.Publisher;
            product.PublicationDate = productEditViewModel.PublicationDate;
            product.Degree = productEditViewModel.Degree;
            product.Status = productEditViewModel.Status;
            product.Trade = productEditViewModel.Trade;
            product.UnitPrice = checkUnitPrice;
            product.EditDate = DateTime.Now;
            product.TradingPlaceAndTime = productEditViewModel.TradingPlaceAndTime;
            _context.SaveChanges();
            UploadImages(filenames, Images, productEditViewModel.ProductId, randomstrings);
        }
        public void DeleteProduct(string ProductId, string name)
        {
            var product = _sellerRepository.GetProductRaw(ProductId, name);
            _context.Entry(product).State = EntityState.Modified;
            product.Status = "刪除";
            product.EditDate = DateTime.Now;
            _context.Update(product);
            _context.SaveChanges();
        }
        public void PermanentDeleteProduct(string ProductId, string name)
        {
            var product = _sellerRepository.GetProductRaw(ProductId, name);
            _context.Remove(product);
            string folderPath = $@"Images\Products\{ProductId}";
            if (Directory.Exists(folderPath))
            {
                Directory.Delete(folderPath, true);
            }
            _context.SaveChanges();
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
    }
}
