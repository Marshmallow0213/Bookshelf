using CoreMVC5_UsedBookProject.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using CoreMVC5_UsedBookProject.ViewModels;

namespace CoreMVC5_UsedBookProject.Services
{
    public class AdminAccountService
    {
        private readonly AdminAccountContext _ctx;
        public AdminAccountService(AdminAccountContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<ApplicationUser> AuthenticateUser(LoginViewModel loginVM)
        {
            var user = await _ctx.Users
                .FirstOrDefaultAsync(u => u.Name.ToUpper() == loginVM.UserName && u.Password == (loginVM.Password));

            if (user != null)
            {
                //讀取第一個Role
                var roleName = await _ctx.Users
                                .Where(u => u.Name == loginVM.UserName)
                                .SelectMany(u => u.AdministratorRoles)
                                .Select(ur => ur.Role.Name)
                                .FirstOrDefaultAsync();

                //讀取所有Role角色
                List<string> roleNames = await _ctx.Users
                                            .Where(u => u.Name == loginVM.UserName)
                                            .SelectMany(u => u.AdministratorRoles)
                                            .Select(ur => ur.Role.Name)
                                            .ToListAsync();

                var userInfo = new ApplicationUser
                {
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
    }
}
