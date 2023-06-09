using Microsoft.AspNetCore.Mvc;
using CoreMVC5_UsedBookProject.Models;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Authorization;
using CoreMVC5_UsedBookProject.ViewModel;
using CoreMVC5_UsedBookProject.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis;
using CoreMVC5_UsedBookProject.Interfaces;

namespace CoreMVC5_UsedBookProject.Controllers
{
    [Authorize]
    public class SellerController : Controller
    {
        private readonly ProductContext _context;
        private readonly int _limit;
        private readonly IHashService _hashService;

        public SellerController(ProductContext context, IHashService hashService)
        {
            _context = context;
            _limit = 100;
            _hashService = hashService;
        }
        public IActionResult Index()
        {
            Dictionary<string, int> count = OrdersAllCountList();
            ViewOrder mymodel = new()
            {
                OrdersCount = count
            };
            return View(mymodel);
        }
        public IActionResult MySalesMoney(String status, int now_page)
        {
            string name = User.Identity.Name;
            Dictionary<string, int> count = OrdersCountList("金錢");
            status ??= "待確認";
            now_page = now_page == 0 ? 1 : now_page;
            int all_pages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(count[status]) / 10));
            List<OrderViewModel> orders = new();
            orders = (from o in _context.OrderByMoneys
                     from p in _context.Products
                     where o.ProductId == p.ProductId && o.Status == (status == "全部" ? o.Status : $"{status}") && (o.SellerId == $"{name}" || o.BuyerId == $"{name}")
                     orderby o.CreateDate descending
                     select new OrderViewModel { OrderId = o.OrderByMoneyId, UnitPrice = o.UnitPrice, SellerId = o.SellerId, BuyerId = o.BuyerId, DenyReason = o.DenyReason, Status = o.Status, ProductId = p.ProductId, Title = p.Title, Image1 = p.Image1 }).Skip((now_page - 1) * 10).Take(10).ToList();
            ViewOrder mymodel = new()
            {
                Orders = orders,
                PagesCount = new int[] { now_page, all_pages },
                OrdersCount = count,
                StatusPage = status
            };
            return View(mymodel);
        }
        public IActionResult MySalesBarter(String status, int now_page)
        {
            string name = User.Identity.Name;
            Dictionary<string, int> count = OrdersCountList("以物易物");
            status ??= "待確認";
            now_page = now_page == 0 ? 1 : now_page;
            int all_pages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(count[status]) / 10));
            List<OrderViewModel> orders = new();
            orders = (from o in _context.OrderByBarters
                     from p in _context.Products
                     where o.ProductId == p.ProductId && o.Status == (status == "全部" ? o.Status : $"{status}") && (o.SellerId == $"{name}" || o.BuyerId == $"{name}")
                     orderby o.CreateDate descending
                     select new OrderViewModel { OrderId = o.OrderByBarterId, SellerId = o.SellerId, BuyerId = o.BuyerId, DenyReason = o.DenyReason, Status = o.Status, ProductId = p.ProductId, Title = p.Title, Image1 = p.Image1 }).Skip((now_page - 1) * 10).Take(10).ToList();
            ViewOrder mymodel = new()
            {
                Orders = orders,
                PagesCount = new int[] { now_page, all_pages },
                OrdersCount = count,
                StatusPage = status
            };
            return View(mymodel);
        }
        public IActionResult MyProductsMoney(String status, int now_page)
        {
            string name = User.Identity.Name;
            Dictionary<string, int> count = ProductsCountList("金錢");
            status ??= "已上架";
            now_page = now_page == 0 ? 1 : now_page;
            int all_pages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(count[status]) / 10));
            var product = (from p in _context.Products
                       where p.Status == (status == "全部" ? p.Status : $"{status}") && p.CreateBy == $"{name}" && p.Trade == "金錢"
                       orderby p.CreateDate descending
                       select new ProductViewModel { ProductId = p.ProductId, Title = p.Title, ISBN = p.ISBN, Author = p.Author, Publisher = p.Publisher, PublicationDate = p.PublicationDate, Degree = p.Degree, ContentText = p.ContentText, Image1 = p.Image1, Image2 = p.Image2, Image3 = p.Image3, Image4 = p.Image4, Image5 = p.Image5, Image6 = p.Image6, Image7 = p.Image7, Image8 = p.Image8, Image9 = p.Image9, Status = p.Status, Trade = p.Trade, UnitPrice = p.UnitPrice, CreateDate = p.CreateDate, EditDate = p.EditDate, CreateBy = p.CreateBy }).Skip((now_page - 1) * 10).Take(10).ToList();
            ViewProduct mymodel = new()
            {
                Products = product,
                PagesCount = new int[] { now_page, all_pages },
                ProductsCount = count,
                StatusPage = status,
                ProductNewLimit = ProductNewLimit()
            };
            return View(mymodel);
        }
        public IActionResult MyProductsBarter(String status, int now_page)
        {
            string name = User.Identity.Name;
            Dictionary<string, int> count = ProductsCountList("以物易物");
            status ??= "已上架";
            now_page = now_page == 0 ? 1 : now_page;
            int all_pages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(count[status]) / 10));
            var product = (from p in _context.Products
                           where p.Status == (status == "全部" ? p.Status : $"{status}") && p.CreateBy == $"{name}" && p.Trade == "以物易物"
                           orderby p.CreateDate descending
                           select new ProductViewModel { ProductId = p.ProductId, Title = p.Title, ISBN = p.ISBN, Author = p.Author, Publisher = p.Publisher, PublicationDate = p.PublicationDate, Degree = p.Degree, ContentText = p.ContentText, Image1 = p.Image1, Image2 = p.Image2, Image3 = p.Image3, Image4 = p.Image4, Image5 = p.Image5, Image6 = p.Image6, Image7 = p.Image7, Image8 = p.Image8, Image9 = p.Image9, Status = p.Status, Trade = p.Trade, UnitPrice = p.UnitPrice, CreateDate = p.CreateDate, EditDate = p.EditDate, CreateBy = p.CreateBy }).Skip((now_page - 1) * 10).Take(10).ToList();
            ViewProduct mymodel = new()
            {
                Products = product,
                PagesCount = new int[] { now_page, all_pages },
                ProductsCount = count,
                StatusPage = status,
                ProductNewLimit = ProductNewLimit()
            };
            return View(mymodel);
        }
        private Dictionary<string, int> OrdersAllCountList()
        {
            string name = User.Identity.Name;
            var countOrderByMoneysList = _context.OrderByMoneys.Where(w => w.SellerId == name || w.BuyerId == name).GroupBy(p => p.Status).Select(g => new { Status = g.Key, count = g.Count() }).ToDictionary(product => product.Status, product => product.count);
            var countOrderByBartersList = _context.OrderByBarters.Where(w => w.SellerId == name || w.BuyerId == name).GroupBy(p => p.Status).Select(g => new { Status = g.Key, count = g.Count() }).ToDictionary(product => product.Status, product => product.count);
            Dictionary<string, int> count = new()
            {
                { "全部", 0 },
                { "待確認", 0 },
                { "已完成", 0 },
                { "待取消", 0 }
            };
            foreach (var item in countOrderByMoneysList.Keys)
            {
                count[item] += countOrderByMoneysList[item];
            }
            foreach (var item in countOrderByBartersList.Keys)
            {
                count[item] += countOrderByBartersList[item];
            }
            int all = 0;
            foreach (var item in count.Values)
            {
                all += item;
            }
            count["全部"] = all;
            return count;
        }
        private Dictionary<string, int> OrdersCountList(string trade)
        {
            string name = User.Identity.Name;
            Dictionary<string, int> countList = new();
            if (trade == "金錢")
            {
                countList = _context.OrderByMoneys.Where(w => w.SellerId == name || w.BuyerId == name).GroupBy(p => p.Status).Select(g => new { Status = g.Key, count = g.Count() }).ToDictionary(product => product.Status, product => product.count);
            }
            else if (trade == "以物易物")
            {
                countList = _context.OrderByBarters.Where(w => w.SellerId == name || w.BuyerId == name).GroupBy(p => p.Status).Select(g => new { Status = g.Key, count = g.Count() }).ToDictionary(product => product.Status, product => product.count);
            }
            Dictionary<string, int> count = new()
            {
                { "全部", 0 },
                { "待確認", 0 },
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
        private Dictionary<string, int> ProductsCountList(string trade)
        {
            string name = User.Identity.Name;
            Dictionary<string, int> countList = new();
            if (trade == "金錢")
            {
                countList = _context.Products.Where(w => w.CreateBy == name && w.Trade == "金錢").GroupBy(p => p.Status).Select(g => new { Status = g.Key, count = g.Count() }).ToDictionary(d => d.Status, d => d.count);
            }
            else if (trade == "以物易物")
            {
                countList = _context.Products.Where(w => w.CreateBy == name && w.Trade == "以物易物").GroupBy(p => p.Status).Select(g => new { Status = g.Key, count = g.Count() }).ToDictionary(d => d.Status, d => d.count);
            }
            else if (trade == "全部")
            {
                countList = _context.Products.Where(w => w.CreateBy == name).GroupBy(p => p.Status).Select(g => new { Status = g.Key, count = g.Count() }).ToDictionary(d => d.Status, d => d.count);
            }
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
        private int[] ProductNewLimit()
        {
            string name = User.Identity.Name;
            Dictionary<string, int> countList = new();
            countList = _context.Products.Where(w => w.CreateBy == name && w.Status == "未上架" || w.Status == "已上架").GroupBy(p => p.Status).Select(g => new { Status = g.Key, count = g.Count() }).ToDictionary(d => d.Status, d => d.count);
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
        public IActionResult Create()
        {
            int[] checkLimit = ProductNewLimit();
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
                Product product = new()
                {
                    ProductId = Id,
                    Title = productCreateViewModel.Title,
                    ContentText = productCreateViewModel.ContentText,
                    Image1 = checkImage[0],
                    Image2 = checkImage[1],
                    Image3 = checkImage[2],
                    Image4 = checkImage[3],
                    Image5 = checkImage[4],
                    Image6 = checkImage[5],
                    Image7 = checkImage[6],
                    Image8 = checkImage[7],
                    Image9 = checkImage[8],
                    ISBN = productCreateViewModel.ISBN,
                    Author = productCreateViewModel.Author,
                    Publisher = productCreateViewModel.Publisher,
                    PublicationDate = productCreateViewModel.PublicationDate,
                    Degree = productCreateViewModel.Degree,
                    Status = productCreateViewModel.Status,
                    Trade = productCreateViewModel.Trade,
                    UnitPrice = productCreateViewModel.UnitPrice,
                    CreateDate = DateTime.Now,
                    EditDate = DateTime.Now,
                    CreateBy = name
                };
                _context.Add(product);
                _context.SaveChanges();
                UploadImages(filenames, Images, Id, randomstrings);
                if (productCreateViewModel.Trade == "金錢")
                {
                    return RedirectToAction("MyProductsMoney");
                }
                else
                {
                    return RedirectToAction("MyProductsBarter");
                }
            }
            else
            {
                return View(productCreateViewModel);
            }
        }

        private static string[] CheckImageName(List<IFormFile> filenames, List<string> Images, List<string> randomstrings)
        {
            string[] checkImage = { "", "", "", "", "", "", "", "", "" };
            for (int i = 0; i <= 8; i++)
            {
                checkImage[i] = filenames[i] == null ? Images[i] : String.Concat($"{randomstrings[i]}{i+1}", Path.GetExtension(Convert.ToString(filenames[i].FileName)));
            }
            return checkImage;
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
                var product = (from p in _context.Products
                               where p.ProductId == $"{ProductId}" && p.CreateBy == $"{name}"
                               orderby p.CreateDate descending
                               select new ProductEditViewModel { ProductId = p.ProductId, Title = p.Title, ISBN = p.ISBN, Author = p.Author, Publisher = p.Publisher, PublicationDate = p.PublicationDate, Degree = p.Degree, ContentText = p.ContentText, Image1 = p.Image1, Image2 = p.Image2, Image3 = p.Image3, Image4 = p.Image4, Image5 = p.Image5, Image6 = p.Image6, Image7 = p.Image7, Image8 = p.Image8, Image9 = p.Image9, Status = p.Status, Trade = p.Trade, UnitPrice = p.UnitPrice, CreateDate = p.CreateDate, EditDate = p.EditDate, CreateBy = p.CreateBy }).FirstOrDefault();
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
                var name = User.Identity.Name;
                var product = (from p in _context.Products
                               where p.ProductId == $"{productEditViewModel.ProductId}" && p.CreateBy == $"{name}"
                               orderby p.CreateDate descending
                               select new Product { ProductId = p.ProductId, Title = p.Title, ISBN = p.ISBN, Author = p.Author, Publisher = p.Publisher, PublicationDate = p.PublicationDate, Degree = p.Degree, ContentText = p.ContentText, Image1 = p.Image1, Image2 = p.Image2, Image3 = p.Image3, Image4 = p.Image4, Image5 = p.Image5, Image6 = p.Image6, Image7 = p.Image7, Image8 = p.Image8, Image9 = p.Image9, Status = p.Status, Trade = p.Trade, UnitPrice = p.UnitPrice, CreateDate = p.CreateDate, EditDate = p.EditDate, CreateBy = p.CreateBy }).FirstOrDefault();
                _context.Entry(product).State = EntityState.Modified;
                product.ProductId = productEditViewModel.ProductId;
                product.Title = productEditViewModel.Title;
                product.ContentText = productEditViewModel.ContentText;
                product.Image1 = checkImage[0];
                product.Image2 = checkImage[1];
                product.Image3 = checkImage[2];
                product.Image4 = checkImage[3];
                product.Image5 = checkImage[4];
                product.Image6 = checkImage[5];
                product.Image7 = checkImage[6];
                product.Image8 = checkImage[7];
                product.Image9 = checkImage[8];
                product.ISBN = productEditViewModel.ISBN;
                product.Author = productEditViewModel.Author;
                product.Publisher = productEditViewModel.Publisher;
                product.PublicationDate = productEditViewModel.PublicationDate;
                product.Degree = productEditViewModel.Degree;
                product.Status = productEditViewModel.Status;
                product.Trade = productEditViewModel.Trade;
                product.UnitPrice = productEditViewModel.UnitPrice;
                product.EditDate = DateTime.Now;
                _context.SaveChanges();
                UploadImages(filenames, Images, productEditViewModel.ProductId, randomstrings);
                if (productEditViewModel.Trade == "金錢")
                {
                    return RedirectToAction("MyProductsMoney");
                }
                else
                {
                    return RedirectToAction("MyProductsBarter");
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
                var product = (from p in _context.Products
                               where p.ProductId == $"{ProductId}" && p.CreateBy == $"{name}"
                               orderby p.CreateDate descending
                               select new ProductViewModel { ProductId = p.ProductId, Title = p.Title, ISBN = p.ISBN, Author = p.Author, Publisher = p.Publisher, PublicationDate = p.PublicationDate, Degree = p.Degree, ContentText = p.ContentText, Image1 = p.Image1, Image2 = p.Image2, Image3 = p.Image3, Image4 = p.Image4, Image5 = p.Image5, Image6 = p.Image6, Image7 = p.Image7, Image8 = p.Image8, Image9 = p.Image9, Status = p.Status, Trade = p.Trade, UnitPrice = p.UnitPrice, CreateDate = p.CreateDate, EditDate = p.EditDate, CreateBy = p.CreateBy }).FirstOrDefault();
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
                var product = (from p in _context.Products
                               where p.ProductId == $"{ProductId}" && p.CreateBy == $"{name}"
                               orderby p.CreateDate descending
                               select new ProductViewModel { ProductId = p.ProductId, Title = p.Title, ISBN = p.ISBN, Author = p.Author, Publisher = p.Publisher, PublicationDate = p.PublicationDate, Degree = p.Degree, ContentText = p.ContentText, Image1 = p.Image1, Image2 = p.Image2, Image3 = p.Image3, Image4 = p.Image4, Image5 = p.Image5, Image6 = p.Image6, Image7 = p.Image7, Image8 = p.Image8, Image9 = p.Image9, Status = p.Status, Trade = p.Trade, UnitPrice = p.UnitPrice, CreateDate = p.CreateDate, EditDate = p.EditDate, CreateBy = p.CreateBy }).FirstOrDefault();
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
        public IActionResult Delete(ProductViewModel productViewModel)
        {
            var name = User.Identity.Name;
            var product = (from p in _context.Products
                           where p.ProductId == $"{productViewModel.ProductId}" && p.CreateBy == $"{name}"
                           orderby p.CreateDate descending
                           select new Product { ProductId = p.ProductId, Title = p.Title, ISBN = p.ISBN, Author = p.Author, Publisher = p.Publisher, PublicationDate = p.PublicationDate, Degree = p.Degree, ContentText = p.ContentText, Image1 = p.Image1, Image2 = p.Image2, Image3 = p.Image3, Image4 = p.Image4, Image5 = p.Image5, Image6 = p.Image6, Image7 = p.Image7, Image8 = p.Image8, Image9 = p.Image9, Status = p.Status, Trade = p.Trade, UnitPrice = p.UnitPrice, CreateDate = p.CreateDate, EditDate = p.EditDate, CreateBy = p.CreateBy }).FirstOrDefault();
            _context.Entry(product).State = EntityState.Modified;
            product.Status = "刪除";
            product.EditDate = DateTime.Now;
            _context.Update(product);
            _context.SaveChanges();
            if (productViewModel.Trade == "金錢")
            {
                return RedirectToAction("MyProductsMoney");
            }
            else
            {
                return RedirectToAction("MyProductsBarter");
            }
        }
        public IActionResult Error()
        {
            return View();
        }
        private static void UploadImages(List<IFormFile> filenames, List<string> Images, string ProductId, List<string> randomstrings)
        {
            string folderPath = $@"Images\{ProductId}";
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
                    file.CopyToAsync(stream);
                }
                else
                {
                    i++;
                }
            }
        }
    }
}
