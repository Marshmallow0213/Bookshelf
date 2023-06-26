using CoreMVC5_UsedBookProject.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;
using CoreMVC5_UsedBookProject.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using CoreMVC5_UsedBookProject.Services;
using CoreMVC5_UsedBookProject.Interfaces;
using CoreMVC5_UsedBookProject.ViewModels;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CoreMVC5_UsedBookProject.Controllers
{
    public class AdministratorLoginController : Controller
    {
        private readonly AdminAccountContext _ctx;
        private readonly AdminAccountService _accountService;
        private readonly IHashService _hashService;
        public AdministratorLoginController(AdminAccountContext ctx, AdminAccountService accountService, IHashService hashService)
        {
            _ctx = ctx;
            _accountService = accountService;
            _hashService = hashService;
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
            var NickName = HttpContext.Request.Cookies["NickName"];
            ViewBag.NickName = NickName;
            var UserIcon = HttpContext.Request.Cookies["UserIcon"];
            ViewBag.UserIcon = UserIcon;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginHardCode(LoginViewModel loginVM)
        {
            if (ModelState.IsValid)
            {
                //失敗
                if (loginVM.UserName.ToUpper() != "POTATO" || loginVM.Password != "123")
                {
                    ModelState.AddModelError(string.Empty, "帳號密碼有錯!!!");
                    return View(loginVM);
                }

                //成功,通過帳比對,以下開始建授權
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, loginVM.UserName),
                    //new Claim(ClaimTypes.Role, "Administrator") // 如果要有「群組、角色、權限」，可以加入這一段  
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    //AllowRefresh = <bool>,
                    // Refreshing the authentication session should be allowed.

                    //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                    // The time at which the authentication ticket expires. A 
                    // value set here overrides the ExpireTimeSpan option of 
                    // CookieAuthenticationOptions set with AddCookie.

                    //IsPersistent = true,
                    // Whether the authentication session is persisted across 
                    // multiple requests. When used with cookies, controls
                    // whether the cookie's lifetime is absolute (matching the
                    // lifetime of the authentication ticket) or session-based.

                    //IssuedUtc = <DateTimeOffset>,
                    // The time at which the authentication ticket was issued.

                    //RedirectUri = <string>
                    // The full path or absolute URI to be used as an http 
                    // redirect response value.
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties
                    );

                return LocalRedirect("~/AdministratorHomePage/Home");
            }

            return View(loginVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if (ModelState.IsValid)
            {
                //驗證使用者帳密
                ApplicationUser user = await AuthenticateUser(loginVM);



                //失敗
                if (user == null)
                {
                    string errorMessage = "帳號密碼錯誤!!!";

                    ViewBag.ErrorMessage = errorMessage; // 将错误消息存储在 ViewBag 中

                    return View(loginVM);
                }


                //成功,通過帳比對,以下開始建授權
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Role, user.Role) // 如果要有「群組、角色、權限」，可以加入這一段  
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    //AllowRefresh = <bool>,
                    // Refreshing the authentication session should be allowed.

                    //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                    // The time at which the authentication ticket expires. A 
                    // value set here overrides the ExpireTimeSpan option of 
                    // CookieAuthenticationOptions set with AddCookie.

                    //IsPersistent = true,
                    // Whether the authentication session is persisted across 
                    // multiple requests. When used with cookies, controls
                    // whether the cookie's lifetime is absolute (matching the
                    // lifetime of the authentication ticket) or session-based.

                    //IssuedUtc = <DateTimeOffset>,
                    // The time at which the authentication ticket was issued.

                    //RedirectUri = <string>
                    // The full path or absolute URI to be used as an http 
                    // redirect response value.
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties
                    );

                return LocalRedirect("~/AdministratorHomePage/Home");

            }

            return View(loginVM);
        }

        private async Task<ApplicationUser> AuthenticateUser(LoginViewModel loginVM)
        {
            var user = await _ctx.Users
                .FirstOrDefaultAsync(u => u.Name.ToUpper() == loginVM.UserName && u.Password == _hashService.MD5Hash(loginVM.Password));

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

        //登出
        public async Task<IActionResult> Signout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return LocalRedirect("/");
        }
    }
}
