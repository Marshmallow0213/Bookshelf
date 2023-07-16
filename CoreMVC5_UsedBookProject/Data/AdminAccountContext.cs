using Microsoft.EntityFrameworkCore;
using CoreMVC5_UsedBookProject.Models;
using CoreMVC5_UsedBookProject.Interfaces;
using CoreMVC5_UsedBookProject.Services;
using CoreMVC5_UsedBookProject.ViewModels;


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
        public DbSet<AdministratorRole> Roles { get; set; }
        public DbSet<AdministratorUserRoles> UserRoles  { get; set; }
        public DbSet<Textbox> TextValue { get; set; }
        public DbSet<Adminlist> Adminlists { get; set; }
        public DbSet<AdminRole> AdminRoles { get; set; }
        public DbSet<AdminlistRole> AdminlistRoles { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //使用Entity Framework的Fluent API，通過使用HasKey方法將UserId和RoleId屬性標記為複合主鍵
            modelBuilder.Entity<AdministratorUserRoles>().HasKey(ur => new { ur.UserId, ur.RoleId });
            modelBuilder.Entity<AdminlistRole>().HasKey(lr => new { lr.ListId, lr.RoleId });

            //Password未加密
            //modelBuilder.Entity<AdministratorUser>().HasData(
            //new AdministratorUser { Id = "B001", Name = "Potatodog@bookshelf.com", Password = "MBpassword001", Nickname = "馬鈴薯狗" },
            // new AdministratorUser { Id = "B002", Name = "Tony@bookshelf.com", Password = "MBpassword002", Nickname = "Tony" },
            //new AdministratorUser { Id = "B003", Name = "Neverloses@bookshelf.com", Password = "MBpassword003", Nickname = "甲甲志" }
            //);
            //Password未加密
            modelBuilder.Entity<AdministratorUser>().HasData(
                new AdministratorUser { Id = "B001", Name = "Potatodog@bookshelf.com", Password = _hashService.MD5Hash("MBpassword001"), Nickname = "馬鈴薯狗", PhoneNo = "0900-951-456", Email = "Potatodog@bookshelf.com" },
                new AdministratorUser { Id = "B002", Name = "Tony@bookshelf.com", Password = _hashService.MD5Hash("MBpassword002"), Nickname = "Tony", PhoneNo = "0933-941-941", Email = "Tony@bookshelf.com" },
                new AdministratorUser { Id = "B003", Name = "Neverloses@bookshelf.com", Password = _hashService.MD5Hash("MBpassword003"), Nickname = "甲甲志", PhoneNo = "0987-587-587", Email = "Neverloses@bookshelf.com" }
                );

            modelBuilder.Entity<AdministratorRole>().HasData(
                new AdministratorRole { Id = "R001", Name = "Top Administrator" },
                new AdministratorRole { Id = "R002", Name = "common Administrator" },
                new AdministratorRole { Id = "R003", Name = "Suspended Administrator" }
                );

            modelBuilder.Entity<AdministratorUserRoles>().HasData(
                new AdministratorUserRoles { UserId = "B001", RoleId = "R001" },
                new AdministratorUserRoles { UserId = "B002", RoleId = "R001" },
                new AdministratorUserRoles { UserId = "B003", RoleId = "R001" }
                );
            modelBuilder.Entity<Textbox>().HasData(
                new Textbox { Id = "T001", TextValue = "二手書交換平台", Image1 = "Deafult.jpg", Image2 = "Deafult.jpg", Image3 = "Deafult.jpg" }
                );


            modelBuilder.Entity<Adminlist>().HasData(
                new Adminlist { Id = "2023/07/16", Maintitle = "帶烏龜看醫生", subtitle = "帶烏龜到334桃園市八德區介壽路一段248-1號(丹尼爾動物醫院)看醫生，並且要確定身高體重以及服藥次數" },
                new Adminlist { Id = "2023/07/18", Maintitle = "買便當回管理員室", subtitle = "因為平常樓下便當實在太難吃了，所以老闆希望可以買山下平常聚會地點的羊肉爐"},
                new Adminlist { Id = "2023/07/20", Maintitle = "倒垃圾", subtitle = "管理員室的垃圾已經推積如山，所以要盡快完成"}
                );

            modelBuilder.Entity<AdminRole>().HasData(
                new AdminRole { Id = "R001", Name = "undone" },
                new AdminRole { Id = "R002", Name = "done" },
                new AdminRole { Id = "R003", Name = "checkF" },
                new AdminRole { Id = "R004", Name = "checkT" }
                );

            modelBuilder.Entity<AdminlistRole>()
                .HasData(
                new AdminlistRole { ListId = "2023/07/16", RoleId = "R001" },
                new AdminlistRole { ListId = "2023/07/18", RoleId = "R001" },
                new AdminlistRole { ListId = "2023/07/20", RoleId = "R001" }
                );




        }




        public DbSet<CoreMVC5_UsedBookProject.ViewModels.AdministratorUserHomePage> AdministratorUserHomePage { get; set; }




        public DbSet<CoreMVC5_UsedBookProject.ViewModels.ToDoListViewModels> ToDoListViewModels { get; set; }
    }
}
