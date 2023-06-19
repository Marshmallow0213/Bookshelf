using Microsoft.AspNetCore.Mvc;
using CoreMVC5_UsedBookProject.ViewModels;
using CoreMVC5_UsedBookProject.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using CoreMVC5_UsedBookProject.Data;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using CoreMVC5_UsedBookProject.Services;
using CoreMVC5_UsedBookProject.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace CoreMVC5_UsedBookProject.Controllers
{
    public class BuyerAccountController : Controller
    {
        private readonly ProductContext _ctx;
        private readonly AccountService _accountService;
        private readonly IHashService _hashService;
        public BuyerAccountController(ProductContext ctx, AccountService accountService, IHashService hashService)
        {
            _ctx = ctx;
            _accountService = accountService;
            _hashService = hashService;
            //HttpContext.Response.Cookies.Append("setCookie", "CookieValue");
            //HttpContext.Request.Cookies["key"];
            //HttpContext.Response.Cookies.Delete("key");
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
                if (loginVM.UserName.ToUpper() != "Kevin".ToUpper() || loginVM.Password != "12345")
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

                return LocalRedirect("~/Seller/Index");
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
                //ApplicationUser user = await AuthenticateUser(loginVM);

                ApplicationUser user = await _accountService.AuthenticateUser(loginVM);

                //失敗
                if (user.Id == "noExist")
                {
                    ModelState.AddModelError(string.Empty, "帳號不存在!!!");
                    return View(loginVM);
                }
                if (user.Id == "error")
                {
                    ModelState.AddModelError(string.Empty, "帳號密碼有錯!!!");
                    return View(loginVM);
                }
                //成功,通過帳比對,以下開始建授權
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Id),
                    new Claim(ClaimTypes.Email, user.Nickname),
                    //new Claim(ClaimTypes.Role, user.Role) // 如果要有「群組、角色、權限」，可以加入這一段
                };
                foreach (var item in user.Roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, item));
                }
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
                HttpContext.Response.Cookies.Append("Nickname", user.Nickname);
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties
                    );

                return LocalRedirect("~/Home/Index");
            }

            return View(loginVM);
        }

        //登出
        public async Task<IActionResult> Signout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Response.Cookies.Delete("Nickname");
            return LocalRedirect("/");
        }
        [HttpGet]
        public IActionResult RegisterUserName()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RegisterUserName(RegisterViewModel registerVM)
        {
            var checkAccountExist = (from p in _ctx.Users
                                     where p.Name.ToUpper() == $"{registerVM.UserName.ToUpper()}"
                                     select new { p.Id }).FirstOrDefault();
            if (checkAccountExist == null)
            {
                return RedirectToAction("Register", new { UserName = registerVM.UserName });
            }
            ViewBag.Error = "使用者名稱已存在";
            return View(registerVM);
        }
        [HttpGet]
        public IActionResult Register(string username)
        {
            ViewBag.UserName = username;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel registerVM)
        {
            if (ModelState.IsValid)
            {
                var checkAccountExist = (from p in _ctx.Users
                                         where p.Name == $"{registerVM.UserName}"
                                         select new { p.Id }).FirstOrDefault();
                if (checkAccountExist != null)
                {
                    ViewBag.Error = "使用者名稱已存在";
                    return View(registerVM);
                }
                string Id = Guid.NewGuid().ToString();
                var checkProductExist = (from p in _ctx.Users
                                         where p.Id == $"{Id}"
                                         select new { p.Id }).FirstOrDefault();
                while (checkProductExist != null)
                {
                    Id = Guid.NewGuid().ToString();
                    checkProductExist = (from p in _ctx.Users
                                         where p.Id == $"{Id}"
                                         select new { p.Id }).FirstOrDefault();
                };
                //user => DB
                //ViewModel => Data Model
                string password = _hashService.HashPassword(registerVM.Password);
                User user = new User
                {
                    Id = Id,
                    Name = registerVM.UserName,
                    //Password = _hashService.MD5Hash(registerVM.Password),
                    Password = password,
                    Nickname = registerVM.UserName
                };

                _ctx.Users.Add(user);
                UserRoles userRoles1 = new UserRoles
                {
                    UserId = Id,
                    RoleId = "R001",
                };
                UserRoles userRoles2 = new UserRoles
                {
                    UserId = Id,
                    RoleId = "R002",
                };
                _ctx.UserRoles.Add(userRoles1);
                _ctx.UserRoles.Add(userRoles2);
                await _ctx.SaveChangesAsync();

                ViewData["Title"] = "帳號註冊";
                ViewData["Message"] = "使用者帳號註冊成功!";  //顯示訊息

                return View("~/Views/Shared/ResultMessage.cshtml");
            }

            return View();
        }
    }
}
