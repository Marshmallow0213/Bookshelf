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

namespace CoreMVC5_UsedBookProject.Repositories
{
    public class SellerRepository
    {
        private readonly ProductContext _context;
        public SellerRepository(ProductContext context)
        {
            _context = context;
        }
        public Product GetProductRaw(string ProductId)
        {
            var product = (from p in _context.Products
                           where p.ProductId == $"{ProductId}"
                           orderby p.CreateDate descending
                           select new Product { ProductId = p.ProductId, Title = p.Title, ISBN = p.ISBN, Author = p.Author, Publisher = p.Publisher, Degree = p.Degree, ContentText = p.ContentText, ImageList = p.ImageList, Status = p.Status, Trade = p.Trade, UnitPrice = p.UnitPrice, CreateDate = p.CreateDate, EditDate = p.EditDate, TradingRemarque = p.TradingRemarque, CreateBy = p.CreateBy }).FirstOrDefault();
            return product;
        }
        public ProductViewModel DMToVM(Product product)
        {
            List<string> result = product.ImageList.Split(',').ToList();
            ProductViewModel viewModel = new()
            {
                ProductId = product.ProductId,
                Title = product.Title,
                ISBN = product.ISBN,
                Author = product.Author,
                Publisher = product.Publisher,
                Degree = product.Degree,
                ContentText = product.ContentText,
                Image1 = product.ImageList,
                Image2 = result[0],
                Image3 = result[1],
                Image4 = result[2],
                Image5 = result[3],
                Image6 = result[4],
                Image7 = result[5],
                Image8 = result[6],
                Image9 = result[7],
                Status = product.Status,
                Trade = product.Trade,
                UnitPrice = product.UnitPrice,
                CreateDate = product.CreateDate,
                EditDate = product.EditDate,
                TradingRemarque = product.TradingRemarque,
                CreateBy = product.CreateBy
            };
            return viewModel;
        }
        public Product VMToDM(ProductViewModel ProductViewModel, string[] checkImage)
        {
            Product model = new()
            {
                ProductId = ProductViewModel.ProductId,
                Title = ProductViewModel.Title,
                ISBN = ProductViewModel.ISBN,
                Author = ProductViewModel.Author,
                Publisher = ProductViewModel.Publisher,
                Degree = ProductViewModel.Degree,
                ContentText = ProductViewModel.ContentText,
                ImageList = String.Join(",", checkImage[1..]),
                Status = ProductViewModel.Status,
                Trade = ProductViewModel.Trade,
                UnitPrice = ProductViewModel.UnitPrice,
                CreateDate = ProductViewModel.CreateDate,
                EditDate = ProductViewModel.EditDate,
                TradingRemarque = ProductViewModel.TradingRemarque,
                CreateBy = ProductViewModel.CreateBy
            };
            return model;
        }
    }
}
