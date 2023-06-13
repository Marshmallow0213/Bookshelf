using CoreMVC5_UsedBookProject.Data;
using CoreMVC5_UsedBookProject.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using CoreMVC5_UsedBookProject.Interfaces;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System;
using CoreMVC5_UsedBookProject.Models;

namespace CoreMVC5_UsedBookProject.Services
{
    public class AccountService
    {
        private readonly ProductContext _ctx;
        private readonly IHashService _hashService;
        public AccountService(ProductContext ctx, IHashService hashService)
        {
            _ctx = ctx;
            _hashService = hashService;
        }

        public  async Task<ApplicationUser> AuthenticateUser(LoginViewModel loginVM)
        {
            var password = await _ctx.Users.Where(w => w.Name.ToUpper() == loginVM.UserName).Select(s => s.Password)
                .FirstOrDefaultAsync();
            bool isPassword = _hashService.Verify(loginVM.Password, password);
            var user = await _ctx.Users.Where(w => w.Name.ToUpper() == loginVM.UserName)
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
                    Roles = roleNames.ToArray()
                };

                return userInfo;
            }
            else
            {
                return null;
            }
        }
        public UserViewModel GetUser(string name)
        {
            var user = (from p in _ctx.Users
                        where p.Id == name
                        select new UserViewModel { Id = p.Id, Name = p.Name, Nickname = p.Nickname, Email = p.Email, PhoneNo = p.PhoneNo }).FirstOrDefault();
            return user;
        }

    }
}
