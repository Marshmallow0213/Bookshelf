﻿using CoreMVC5_UsedBookProject.Models;
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
        public DbSet<Wish>Wishes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //使用Entity Framework的Fluent API，通過使用HasKey方法將UserId和RoleId屬性標記為複合主鍵
            modelBuilder.Entity<Product>().HasKey(ur => new { ur.ProductId });
            modelBuilder.Entity<Order>().HasKey(ur => new { ur.OrderId });
            modelBuilder.Entity<User>().HasKey(ur => new { ur.Id });
            modelBuilder.Entity<Role>().HasKey(ur => new { ur.Id });
            modelBuilder.Entity<UserRoles>().HasKey(ur => new { ur.UserId, ur.RoleId });
            modelBuilder.Entity<Shoppingcart>().HasKey(ur => new { ur.ShoppingcartId });
            modelBuilder.Entity<Wish>().HasKey(ur => new { ur.WishId });
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
            modelBuilder.Entity<Wish>(entity =>
            {
                entity.HasOne(d => d.User)
                   .WithMany()
                   .HasForeignKey(d => d.Id);
            });
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Name)
                .IsUnique();
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasData(
                new User { Id = "Owner", Name = "uU7SkhR5UQ3sZA5B", Email = "null", Password = _hashService.HashPassword("S3drXR0tFA3v9DzS"), Nickname = "Owner", PhoneNo = "null", UserIcon = "UserIcon.png" }
                );

            modelBuilder.Entity<Role>().HasData(
                new Role { Id = "R001", Name = "Seller" },
                new Role { Id = "R002", Name = "Buyer" },
                new Role { Id = "R003", Name = "Administrator" },
                new Role { Id = "R004", Name = "Owner" }
                );

            modelBuilder.Entity<UserRoles>()
                .HasData(
                new UserRoles { UserId = "Owner", RoleId = "R001" },
                new UserRoles { UserId = "Owner", RoleId = "R002" },
                new UserRoles { UserId = "Owner", RoleId = "R003" },
                new UserRoles { UserId = "Owner", RoleId = "R004" }
                );
        }
    }

}
