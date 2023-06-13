﻿using Microsoft.EntityFrameworkCore;
using CoreMVC5_UsedBookProject.Models;
using CoreMVC5_UsedBookProject.Interfaces;
using CoreMVC5_UsedBookProject.Services;

namespace CoreMVC5_UsedBookProject.Data
{
    public class AdminAccountContext : DbContext
    {
        private readonly IHashService _hashService;
        public AdminAccountContext(DbContextOptions<AdminAccountContext> options, IHashService hashService) : base(options)
        {
            _hashService = hashService;
        }

        public DbSet<AdministratorUser> Users { get; set; }
        public DbSet<AdminRole> Roles { get; set; }
        public DbSet<AdministratorUserRoles> UserRoles  { get; set; }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //使用Entity Framework的Fluent API，通過使用HasKey方法將UserId和RoleId屬性標記為複合主鍵
            modelBuilder.Entity<AdministratorUserRoles>().HasKey(ur => new { ur.UserId, ur.RoleId });
            //Password未加密
            //modelBuilder.Entity<AdministratorUser>().HasData(
                //new AdministratorUser { Id = "B001", Name = "Potatodog@bookshelf.com", Password = "MBpassword001", Nickname = "馬鈴薯狗" },
               // new AdministratorUser { Id = "B002", Name = "Tony@bookshelf.com", Password = "MBpassword002", Nickname = "Tony" },
                //new AdministratorUser { Id = "B003", Name = "Neverloses@bookshelf.com", Password = "MBpassword003", Nickname = "甲甲志" }
                //);
            //Password未加密
            modelBuilder.Entity<AdministratorUser>().HasData(
                new AdministratorUser { Id = "B001", Name = "Potatodog@bookshelf.com", Password = _hashService.MD5Hash("MBpassword001"), Nickname = "馬鈴薯狗" },
                new AdministratorUser { Id = "B002", Name = "Tony@bookshelf.com", Password = _hashService.MD5Hash("MBpassword002"), Nickname = "Tony" },
                new AdministratorUser { Id = "B003", Name = "Neverloses@bookshelf.com", Password = _hashService.MD5Hash("MBpassword003"), Nickname = "甲甲志" }
                );

            modelBuilder.Entity<AdminRole>().HasData(
            new AdminRole { Id = "R001", Name = "Administrator" },
                new AdminRole { Id = "R002", Name = "common user" }
                );

            modelBuilder.Entity<AdministratorUserRoles>()
                .HasData(
                new AdministratorUserRoles { UserId = "B001", RoleId = "R001" },
                new AdministratorUserRoles { UserId = "B002", RoleId = "R002" }
                );
        }
    }
}
