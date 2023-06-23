using CoreMVC5_UsedBookProject.ViewModel;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using CoreMVC5_UsedBookProject.Data;
using CoreMVC5_UsedBookProject.Interfaces;
using CoreMVC5_UsedBookProject.Repositories;
using System.Linq;

namespace CoreMVC5_UsedBookProject.Services
{
    public class BuyerService
    {
        private readonly ProductContext _context;
        private readonly IHashService _hashService;

        public BuyerService(ProductContext productContext, IHashService hashService)
        {
            _context = productContext;
           
            _hashService = hashService;
           
        }
        public ProductEditViewModel GetProduct(   string id)
        {



            //var product = (from p in _context.Products

            //               where p.Status == "已上架"
            //               orderby p.CreateDate descending
            //               select new ProductViewModel {  Title = p.Title, ISBN = p.ISBN, Author = p.Author, Publisher = p.Publisher, PublicationDate = p.PublicationDate, Degree = p.Degree, ContentText = p.ContentText, Image1 = p.Image1, Image2 = p.Image2, Status = p.Status, Trade = p.Trade, UnitPrice = p.UnitPrice, CreateDate = p.CreateDate, EditDate = p.EditDate, CreateBy = p.CreateBy }).ToList();
            var product = _context.Products
            .Where(p => p.Status == "已上架"&&p.ProductId == id)
            .OrderByDescending(p => p.CreateDate)
            .Select(p => new ProductEditViewModel
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

            return product;
        }

    }
}
