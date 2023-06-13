using CoreMVC5_UsedBookProject.Models;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics;
using System.Security.Policy;
using System;

namespace CoreMVC5_UsedBookProject.Data
{
    public class ProductContext: DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options): base(options)
        { 
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderByMoney> OrderByMoneys { get; set; }
        public DbSet<OrderByBarter> OrderByBarters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //使用Entity Framework的Fluent API，通過使用HasKey方法將UserId和RoleId屬性標記為複合主鍵
            modelBuilder.Entity<Product>().HasKey(ur => new { ur.ProductId });
            modelBuilder.Entity<OrderByMoney>().HasKey(ur => new { ur.OrderByMoneyId });
            modelBuilder.Entity<OrderByBarter>().HasKey(ur => new { ur.OrderByBarterId });
            modelBuilder.Entity<OrderByMoney>(entity =>
            {
                entity.HasOne(d => d.Product)
                   .WithMany()
                   .HasForeignKey(d => d.ProductId);
            });
            modelBuilder.Entity<OrderByBarter>(entity =>
            {
                entity.HasOne(d => d.Product)
                   .WithMany()
                   .HasForeignKey(d => d.ProductId);
            });
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().HasData(
            new Product
            {
                ProductId = "P001",
                Title = "Book1",
                ContentText = "Context1",
                Image1 = "example.jpg",
                Image2 = "無圖片,無圖片,無圖片,無圖片,無圖片,無圖片,無圖片,無圖片",
                ISBN = "9876543210",
                Author = "作者",
                Publisher = "出版社",
                PublicationDate = "2023-01-01",
                Degree = "二手",
                Status = "未上架",
                Trade = "金錢",
                UnitPrice = 500,
                CreateDate = DateTime.Now,
                EditDate = DateTime.Now,
                CreateBy = "U001"
            },
            new Product
            {
                ProductId = "P002",
                Title = "Book2",
                ContentText = "Context2",
                Image1 = "example.jpg",
                Image2 = "無圖片,無圖片,無圖片,無圖片,無圖片,無圖片,無圖片,無圖片",
                ISBN = "9876543211",
                Author = "作者",
                Publisher = "出版社",
                PublicationDate = "2023-01-01",
                Degree = "二手",
                Status = "未上架",
                Trade = "金錢",
                UnitPrice = 500,
                CreateDate = DateTime.Now,
                EditDate = DateTime.Now,
                CreateBy = "U001"
            },
            new Product
            {
                ProductId = "P003",
                Title = "Book3",
                ContentText = "Context3",
                Image1 = "example.jpg",
                Image2 = "無圖片,無圖片,無圖片,無圖片,無圖片,無圖片,無圖片,無圖片",
                ISBN = "9876543212",
                Author = "作者",
                Publisher = "出版社",
                PublicationDate = "2023-01-01",
                Degree = "二手",
                Status = "未上架",
                Trade = "以物易物",
                UnitPrice = -1,
                CreateDate = DateTime.Now,
                EditDate = DateTime.Now,
                CreateBy = "U001"
            }
            );
            modelBuilder.Entity<OrderByMoney>().HasData(
            new OrderByMoney
            {
                OrderByMoneyId = "OM001",
                SellerId = "U001",
                BuyerId = "U002",
                DenyReason = "none",
                ProductId = "P001",
                UnitPrice = 500,
                Status = "待確認",
                CreateDate = DateTime.Now
            },
            new OrderByMoney
            {
                OrderByMoneyId = "OM002",
                SellerId = "U001",
                BuyerId = "U002",
                DenyReason = "none",
                ProductId = "P002",
                UnitPrice = 500,
                Status = "待確認",
                CreateDate = DateTime.Now
            }
            );
            modelBuilder.Entity<OrderByBarter>().HasData(
            new OrderByBarter
            {
                OrderByBarterId = "OB001",
                SellerId = "U001",
                BuyerId = "U003",
                DenyReason = "none",
                ProductId = "P003",
                Status = "待確認",
                CreateDate = DateTime.Now
            }
            );
        }
    }

}
