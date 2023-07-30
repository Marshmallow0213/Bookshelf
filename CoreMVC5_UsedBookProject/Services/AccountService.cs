using CoreMVC5_UsedBookProject.Data;
using CoreMVC5_UsedBookProject.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using CoreMVC5_UsedBookProject.Interfaces;
using System.IO;
using System;
using CoreMVC5_UsedBookProject.Models;
using Microsoft.AspNetCore.Http;
using System.Data;
using Microsoft.AspNetCore.Hosting;

namespace CoreMVC5_UsedBookProject.Services
{
    public class AccountService
    {
        private readonly ProductContext _ctx;
        private readonly IHashService _hashService;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public AccountService(ProductContext ctx, IHashService hashService, IWebHostEnvironment hostingEnvironment)
        {
            _ctx = ctx;
            _hashService = hashService;
            _hostingEnvironment = hostingEnvironment;
        }

        public  async Task<ApplicationUser> AuthenticateUser(LoginViewModel loginVM)
        {
            var password = await _ctx.Users.Where(w => w.Name.ToUpper() == loginVM.UserName.ToUpper()).Select(s => s.Password)
                .FirstOrDefaultAsync();
            if (password == null)
            {
                var userInfo = new ApplicationUser
                {
                    Id = "noExist"
                };
                return userInfo;
            }
            bool isPassword = _hashService.Verify(loginVM.Password, password);
            var user = await _ctx.Users.Where(w => w.Name.ToUpper() == loginVM.UserName.ToUpper())
                .FirstOrDefaultAsync();

            if (user != null && isPassword)
            {
                //讀取第一個Role
                var roleName = await _ctx.Users
                                .Where(u => u.Name == loginVM.UserName)
                                .SelectMany(u => u.UserRoles)
                                .Select(ur => ur.Role.Name)
                                .FirstOrDefaultAsync();

                //讀取所有Role角色
                List<string> roleNames = await _ctx.Users
                                            .Where(u => u.Name == loginVM.UserName)
                                            .SelectMany(u => u.UserRoles)
                                            .Select(ur => ur.Role.Name)
                                            .ToListAsync();

                var userInfo = new ApplicationUser
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Nickname = user.Nickname,
                    PhoneNo = user.PhoneNo,
                    Role = roleName ?? "",
                    Roles = roleNames.ToArray(),
                    UserIcon = user.UserIcon
                };

                return userInfo;
            }
            else
            {
                var userInfo = new ApplicationUser
                {
                    Id = "error"
                };
                return userInfo;
            }
        }
        public UserViewModel GetUser(string name)
        {
            var user = (from p in _ctx.Users
                        where p.Id == name
                        select new UserViewModel { Id = p.Id, Name = p.Name, Nickname = p.Nickname, Email = p.Email, PhoneNo = p.PhoneNo, UserIcon = p.UserIcon }).FirstOrDefault();
            return user;
        }
        public User GetUserRaw(string name)
        {
            var user = (from p in _ctx.Users
                        where p.Id == name
                        select new User { Id = p.Id, Name = p.Name, Password = p.Password, Nickname = p.Nickname, Email = p.Email, PhoneNo = p.PhoneNo, UserIcon = p.UserIcon }).FirstOrDefault();
            return user;
        }
        public void UploadImages(IFormFile filename, string UserId)
        {
            string folderPath = $@"{_hostingEnvironment.WebRootPath}\Images\Users\{UserId}";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            if (filename != null)
            {
                string[] files = System.IO.Directory.GetFiles(folderPath, $"UserIcon.*");
                foreach (string f in files)
                {
                    System.IO.File.Delete(f);
                }
                var path = $@"{folderPath}\UserIcon{Path.GetExtension(Convert.ToString(filename.FileName))}";
                using var stream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite, FileShare.None, 2097152);
                filename.CopyTo(stream);
            }
        }
    }
}
