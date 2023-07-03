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
        public Product GetProductRaw(string ProductId, string name)
        {
            var product = (from p in _context.Products
                           where p.ProductId == $"{ProductId}" && p.CreateBy == $"{name}"
                           orderby p.CreateDate descending
                           select new Product { ProductId = p.ProductId, Title = p.Title, ISBN = p.ISBN, Author = p.Author, Publisher = p.Publisher, PublicationDate = p.PublicationDate, Degree = p.Degree, ContentText = p.ContentText, Image1 = p.Image1, Image2 = p.Image2, Status = p.Status, Trade = p.Trade, UnitPrice = p.UnitPrice, CreateDate = p.CreateDate, EditDate = p.EditDate, TradingPlaceAndTime = p.TradingPlaceAndTime, CreateBy = p.CreateBy }).FirstOrDefault();
            return product;
        }
        public ProductEditViewModel DMToVM(Product product)
        {
            List<string> result = product.Image2.Split(',').ToList();
            ProductEditViewModel viewModel = new()
            {
                ProductId = product.ProductId,
                Title = product.Title,
                ISBN = product.ISBN,
                Author = product.Author,
                Publisher = product.Publisher,
                PublicationDate = product.PublicationDate,
                Degree = product.Degree,
                ContentText = product.ContentText,
                Image1 = product.Image1,
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
                TradingPlaceAndTime = product.TradingPlaceAndTime,
                CreateBy = product.CreateBy
            };
            return viewModel;
        }
        public Product VMToDM(ProductEditViewModel productEditViewModel, string[] checkImage)
        {
            Product model = new()
            {
                ProductId = productEditViewModel.ProductId,
                Title = productEditViewModel.Title,
                ISBN = productEditViewModel.ISBN,
                Author = productEditViewModel.Author,
                Publisher = productEditViewModel.Publisher,
                PublicationDate = productEditViewModel.PublicationDate,
                Degree = productEditViewModel.Degree,
                ContentText = productEditViewModel.ContentText,
                Image1 = productEditViewModel.Image1,
                Image2 = String.Join(",", checkImage[1..]),
                Status = productEditViewModel.Status,
                Trade = productEditViewModel.Trade,
                UnitPrice = productEditViewModel.UnitPrice,
                CreateDate = productEditViewModel.CreateDate,
                EditDate = productEditViewModel.EditDate,
                TradingPlaceAndTime = productEditViewModel.TradingPlaceAndTime,
                CreateBy = productEditViewModel.CreateBy
            };
            return model;
        }
    }
}
