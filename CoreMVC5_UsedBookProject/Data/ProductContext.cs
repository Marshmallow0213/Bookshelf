using CoreMVC5_UsedBookProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using CoreMVC5_UsedBookProject.Interfaces;

namespace CoreMVC5_UsedBookProject.Data
{
    public class ProductContext: DbContext
    {
        private readonly IHashService _hashService;
        public ProductContext(DbContextOptions<ProductContext> options, IHashService hashService) : base(options)
        {
            _hashService = hashService;
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }
        public DbSet<Shoppingcart> Shoppingcarts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //使用Entity Framework的Fluent API，通過使用HasKey方法將UserId和RoleId屬性標記為複合主鍵
            modelBuilder.Entity<Product>().HasKey(ur => new { ur.ProductId });
            modelBuilder.Entity<Order>().HasKey(ur => new { ur.OrderId });
            modelBuilder.Entity<User>().HasKey(ur => new { ur.Id });
            modelBuilder.Entity<Role>().HasKey(ur => new { ur.Id });
            modelBuilder.Entity<UserRoles>().HasKey(ur => new { ur.UserId, ur.RoleId });
            modelBuilder.Entity<Shoppingcart>().HasKey(ur => new { ur.ShoppingcartId });
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasOne(d => d.User)
                   .WithMany()
                   .HasForeignKey(d => d.CreateBy);
            });
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasOne(d => d.Product)
                   .WithMany()
                   .HasForeignKey(d => d.ProductId);
            });
            modelBuilder.Entity<Shoppingcart>(entity =>
            {
                entity.HasOne(d => d.Product)
                   .WithMany()
                   .HasForeignKey(d => d.ProductId);
            });
            modelBuilder.Entity<Shoppingcart>(entity =>
            {
                entity.HasOne(d => d.User)
                   .WithMany()
                   .HasForeignKey(d => d.Id);
            });
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Name)
                .IsUnique();
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
            modelBuilder.Entity<Order>().HasData(
            new Order
            {
                OrderId = "O001",
                SellerId = "U001",
                BuyerId = "U002",
                DenyReason = "none",
                ProductId = "P001",
                UnitPrice = 500,
                Status = "待確認",
                Trade = "金錢",
                CreateDate = DateTime.Now
            },
            new Order
            {
                OrderId = "O002",
                SellerId = "U001",
                BuyerId = "U002",
                DenyReason = "none",
                ProductId = "P002",
                UnitPrice = 500,
                Status = "待確認",
                Trade = "金錢",
                CreateDate = DateTime.Now
            },
            new Order
            {
                OrderId = "O003",
                SellerId = "U001",
                BuyerId = "U002",
                DenyReason = "none",
                ProductId = "P003",
                UnitPrice = -1,
                Status = "待確認",
                Trade = "以物易物",
                CreateDate = DateTime.Now
            }
            );
            //Password未加密
            //modelBuilder.Entity<User>().HasData(
            //    new User { Id = "U001", Name = "Kevin@gmail.com", Email = "kevinxi@gmail.com", Password = "12345", Nickname = "無敵霸主", PhoneNo = "0925-155222" },
            //    new User { Id = "U002", Name = "Mary@gmail.com", Email = "marylee@gmail.com", Password = "12345", Nickname = "飛天女俠", PhoneNo = "0935-123123" },
            //    new User { Id = "U003", Name = "John@gmail.com", Email = "johnwei@gmail.com", Password = "12345", Nickname = "小李飛刀", PhoneNo = "0955-456456" }
            //    );

            //Password以MD5加密
            modelBuilder.Entity<User>().HasData(
                new User { Id = "U001", Name = "Admin0001", Email = "kevinxi@gmail.com", Password = _hashService.HashPassword("CHU147896301"), Nickname = "Admin0001", PhoneNo = "0925-155222", UserIcon = "UserIcon.png" },
                new User { Id = "U002", Name = "Admin0002", Email = "marylee@gmail.com", Password = _hashService.HashPassword("CHU147896302"), Nickname = "Admin0002", PhoneNo = "0935-123123", UserIcon = "UserIcon.png" },
                new User { Id = "U003", Name = "Admin0003", Email = "johnwei@gmail.com", Password = _hashService.HashPassword("CHU147896303"), Nickname = "Admin0003", PhoneNo = "0955-456456", UserIcon = "UserIcon.png" }
                );

            modelBuilder.Entity<Role>().HasData(
                new Role { Id = "R001", Name = "Seller" },
                new Role { Id = "R002", Name = "Buyer" },
                new Role { Id = "R003", Name = "Administrator" },
                new Role { Id = "R004", Name = "" }
                );

            modelBuilder.Entity<UserRoles>()
                .HasData(
                new UserRoles { UserId = "U001", RoleId = "R001" },
                new UserRoles { UserId = "U002", RoleId = "R001" },
                new UserRoles { UserId = "U003", RoleId = "R001" },
                new UserRoles { UserId = "U001", RoleId = "R002" },
                new UserRoles { UserId = "U002", RoleId = "R002" },
                new UserRoles { UserId = "U003", RoleId = "R002" },
                new UserRoles { UserId = "U001", RoleId = "R003" },
                new UserRoles { UserId = "U002", RoleId = "R003" },
                new UserRoles { UserId = "U003", RoleId = "R003" }
                );
        }
    }

}
