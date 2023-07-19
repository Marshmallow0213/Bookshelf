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
                new Adminlist { Id = 1,Date = "2023/07/13", Maintitle = "頁面修改", Subtitle = "聯絡開發人員將登入做修改後請他也做介面優化" },
                new Adminlist { Id = 2, Date = "2023/07/14", Maintitle = "交換書籍測試", Subtitle = "交換書籍部分測試部分已釋出，所有管理人員測試完後在LINE群回復使用心得"},
                new Adminlist { Id = 3, Date = "2023/07/14", Maintitle = "介面設計", Subtitle = "介面太醜需要調整，聯絡開發人員做修改"},
                new Adminlist { Id = 4, Date = "2023/07/15", Maintitle = "買賣書籍跑版", Subtitle = "買賣書籍頁面跑版，聯絡開發人員做修改" },
                new Adminlist { Id = 5, Date = "2023/07/16", Maintitle = "管理員介面測試", Subtitle = "管理員介面做優化，測試開啟速度" },
                new Adminlist { Id = 6, Date = "2023/07/17", Maintitle = "首頁資訊", Subtitle = "首頁東西太多，我們的目的是需要將買賣與交換的物品放在專屬賣場，原本的主頁面要維持簡潔，請各位討論好後聯絡開發人員做修改" },
                new Adminlist { Id = 7, Date = "2023/07/17", Maintitle = "主頁介面更改", Subtitle = "首頁登入與搜尋框位置做調換" },
                new Adminlist { Id = 8, Date = "2023/07/18", Maintitle = "管理員登入介面公告", Subtitle = "對於新進管理人員許多人不知道隱藏登入位置，要在LINE群做公告" },
                new Adminlist { Id = 9, Date = "2023/07/19", Maintitle = "購物車介面", Subtitle = "購物車介面想要做更換，請聯絡開發人員做修改" },
                new Adminlist { Id = 10, Date = "2023/07/19", Maintitle = "按鈕樣式", Subtitle = "找尋不同按鈕以做為以後界面更新時的備選方案" }
                );

            modelBuilder.Entity<AdminRole>().HasData(
                new AdminRole { Id = 1, Name = "checkT" },
                new AdminRole { Id = 2, Name = "checkF" },
                new AdminRole { Id = 3, Name = "done" },
                new AdminRole { Id = 4, Name = "undone" }
                );

            modelBuilder.Entity<AdminlistRole>()
                .HasData(
                new AdminlistRole { ListId = 1, RoleId = 4 },
                new AdminlistRole { ListId = 2, RoleId = 4 },
                new AdminlistRole { ListId = 3, RoleId = 4 },
                new AdminlistRole { ListId = 4, RoleId = 4 },
                new AdminlistRole { ListId = 5, RoleId = 4 },
                new AdminlistRole { ListId = 6, RoleId = 4 },
                new AdminlistRole { ListId = 7, RoleId = 4 },
                new AdminlistRole { ListId = 8, RoleId = 4 },
                new AdminlistRole { ListId = 9, RoleId = 4 },
                new AdminlistRole { ListId = 10,RoleId = 4 }
                );




        }




        public DbSet<CoreMVC5_UsedBookProject.ViewModels.AdministratorUserHomePage> AdministratorUserHomePage { get; set; }




        public DbSet<CoreMVC5_UsedBookProject.ViewModels.ToDoListViewModels> ToDoListViewModels { get; set; }
    }
}
