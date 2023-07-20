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
using System.IO;
using Microsoft.AspNetCore.Mvc.Filters;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.CodeAnalysis;
using Microsoft.AspNetCore.Hosting;

namespace CoreMVC5_UsedBookProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly ProductContext _ctx;
        private readonly AccountService _accountService;
        private readonly SellerService _sellerService;
        private readonly IHashService _hashService;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public AccountController(ProductContext ctx, AccountService accountService, SellerService sellerService, IHashService hashService, IWebHostEnvironment hostingEnvironment)
        {
            _ctx = ctx;
            _accountService = accountService;
            _sellerService = sellerService;
            _hashService = hashService;
            _hostingEnvironment = hostingEnvironment;
            //HttpContext.Response.Cookies.Append("setCookie", "CookieValue");
            //HttpContext.Request.Cookies["key"];
            //HttpContext.Response.Cookies.Delete("key");
        }
        [HttpGet]
        public IActionResult Login(string ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginVM, string ReturnUrl)
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
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.Nickname),
                    new Claim(ClaimTypes.GivenName, user.UserIcon),
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
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties
                    );
                HttpContext.Response.Cookies.Append("Login", "Now");
                try
                {
                    if(ReturnUrl != null)
                    {
                        var ProductId = ReturnUrl.Split('-');
                        if (ProductId[0] == "ProductId")
                        {
                            return LocalRedirect($"~/Home/Details?ProductId={ProductId[1]}");
                        }
                        else
                        {
                            return LocalRedirect("~/Home/Index");
                        }
                    }
                    else
                    {
                        return LocalRedirect("~/Home/Index");
                    }
                }
                catch
                {
                    return LocalRedirect("~/Home/Index");
                }
            }

            return View(loginVM);
        }

        //登出
        public async Task<IActionResult> Signout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Response.Cookies.Delete("Login");
            return LocalRedirect("/");
        }
        public IActionResult Forbidden()
        {
            return View();
        }
        [Authorize(Roles = "User")]
        public IActionResult Details(string ReturnUrl)
        {
            var name = User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value;
            var user = _accountService.GetUser(name);
            if (ReturnUrl == "/Changed")
            {
                TempData["Message"] = "使用者資訊變更成功!";
            }
            return View(user);
        }

        [Authorize(Roles = "User")]
        public IActionResult ChangePassword()
        {
            return View();
        }
        [Authorize(Roles = "User")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangePassword(UserPasswordChangeViewModel userPasswordChangeViewModel)
        {
            if (ModelState.IsValid)
            {
                var name = User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value;
                string password = _hashService.HashPassword(userPasswordChangeViewModel.Password);
                var user = (from p in _ctx.Users
                            where p.Id == $"{name}"
                            select new User { Id = p.Id, Name = p.Name, Password = p.Password, Nickname = p.Nickname, Email = p.Email, PhoneNo = p.PhoneNo, UserIcon = p.UserIcon }).FirstOrDefault();
                if (!_hashService.Verify(userPasswordChangeViewModel.OldPassword, user.Password))
                {
                    ViewBag.Error = "舊密碼不符";
                    return View(userPasswordChangeViewModel);
                }
                _ctx.Entry(user).State = EntityState.Modified;
                user.Password = password;
                _ctx.SaveChanges();
                TempData["Message"] = "使用者密碼變更成功!";
                return View();
            }
            return View(userPasswordChangeViewModel);
        }
        [Authorize(Roles = "User")]
        public IActionResult ChangeUserInfo()
        {
            var name = User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value;
            var user = _accountService.GetUser(name);
            return View(user);
        }
        [Authorize(Roles = "User")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangeUserInfo(UserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                var name = User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value;
                var user = _accountService.GetUserRaw(name);
                _ctx.Entry(user).State = EntityState.Modified;
                user.Nickname = userViewModel.Nickname;
                user.Email = userViewModel.Email;
                user.PhoneNo = userViewModel.PhoneNo;
                if (userViewModel.File1 != null)
                {
                    user.UserIcon = String.Concat($"UserIcon", Path.GetExtension(Convert.ToString(userViewModel.File1.FileName)));
                }
                _ctx.SaveChanges();
                _accountService.UploadImages(userViewModel.File1, user.Id);
                return RedirectToAction("Details", new { ReturnUrl = "/Changed" });
            }
            return View(userViewModel);
        }
        [Authorize(Roles = "Top Administrator")]
        [HttpGet]
        public IActionResult RegisterFromCSV()
        {
            return View();
        }
        [Authorize(Roles = "Top Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RegisterFromCSV(RegisterFromCSVViewModel registerFromCSV, string submit)
        {
            if (ModelState.IsValid)
            {
                List<string> listA = new();
                List<string> listB = new();
                try
                {
                    using (var reader = new StreamReader(registerFromCSV.File.OpenReadStream()))
                    {
                        while (!reader.EndOfStream)
                        {
                            var line = reader.ReadLine();
                            var values = line.Split(',');

                            listA.Add(values[0]);
                            listB.Add(values[1]);
                        }
                    }
                }
                catch
                {
                    ViewData["Title"] = "錯誤";
                    ViewData["Message"] = $"無法讀取CSV檔或格式不正確。";  //顯示訊息
                }
                if (submit == "批量註冊")
                {
                    int newuserscount = 0;
                    for (int i = 1; i < listA.Count(); i++)
                    {
                        var checkAccountExist = (from p in _ctx.Users
                                                 where p.Name == $"{listA[i]}"
                                                 select new { p.Id }).FirstOrDefault();
                        if (checkAccountExist == null)
                        {
                            string Id = Guid.NewGuid().ToString();
                            var checkIdExist = (from p in _ctx.Users
                                                where p.Id == $"{Id}"
                                                select new { p.Id }).FirstOrDefault();
                            while (checkIdExist != null)
                            {
                                Id = Guid.NewGuid().ToString();
                                checkIdExist = (from p in _ctx.Users
                                                where p.Id == $"{Id}"
                                                select new { p.Id }).FirstOrDefault();
                            };
                            //user => DB
                            //ViewModel => Data Model
                            string password = _hashService.HashPassword(listB[i]);
                            User user = new User
                            {
                                Id = Id,
                                Name = listA[i],
                                //Password = _hashService.MD5Hash(registerVM.Password),
                                Password = password,
                                Nickname = listA[i],
                                UserIcon = "UserIcon.png"
                            };

                            _ctx.Users.Add(user);
                            string folderPath = $@"{_hostingEnvironment.WebRootPath}\Images\Users\{Id}";
                            if (!Directory.Exists(folderPath))
                            {
                                Directory.CreateDirectory(folderPath);
                            }
                            FileInfo fi = new FileInfo($@"{_hostingEnvironment.WebRootPath}\DeafultPictures\EmptyUserIcon.png");
                            fi.CopyTo($@"{folderPath}\UserIcon.png", true);
                            UserRoles userRoles1 = new UserRoles
                            {
                                UserId = Id,
                                RoleId = "R001",
                            };
                            _ctx.UserRoles.Add(userRoles1);
                            newuserscount += 1;
                            _ctx.SaveChanges();
                        }
                    }
                    ViewData["Title"] = "帳號批量註冊";
                    ViewData["Message"] = $"使用者帳號批量註冊成功! 新增{newuserscount}個使用者。";  //顯示訊息
                }
                else if (submit == "批量刪除")
                {
                    int newuserscount = 0;
                    for (int i = 1; i < listA.Count(); i++)
                    {
                        var checkAccountExist = _ctx.Users.Where(w => w.Name.ToUpper() == $"{listA[i]}".ToUpper()).FirstOrDefault();
                        if (checkAccountExist != null)
                        {
                            string Id = checkAccountExist.Id;
                            string folderPath = $@"{_hostingEnvironment.WebRootPath}\Images\Users\{Id}";
                            if (Directory.Exists(folderPath))
                            {
                                Directory.Delete(folderPath, true);
                            }
                            _ctx.Users.Remove(checkAccountExist);
                            newuserscount += 1;
                            _ctx.SaveChanges();
                        }
                        ViewData["Title"] = "帳號批量刪除";
                        ViewData["Message"] = $"使用者帳號批量刪除成功! 刪除{newuserscount}個使用者。";  //顯示訊息
                    }
                }
                return View("~/Views/Shared/ResultMessage.cshtml");
            }
            return View(registerFromCSV);
        }
        public IActionResult DeleteUser(string Id)
        {
            
            var name = User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value;
            if (name != Id || Id == null)
            {
                TempData["Message"] = "你只能刪除自己的帳號!";
                var _user = _accountService.GetUser(name);
                return View(_user);
            }
            string folderPath = $@"{_hostingEnvironment.WebRootPath}\Images\Users\{Id}";
            if (Directory.Exists(folderPath))
            {
                Directory.Delete(folderPath, true);
            }
            var barterorders = _ctx.BarterOrders.Where(w => w.SellerId == Id || w.BuyerId == Id).ToList();
            foreach (var barterorder in barterorders)
            {
                _ctx.BarterOrders.Remove(barterorder);
            }
            var orders = _ctx.Orders.Where(w => w.SellerId == Id || w.BuyerId == Id).ToList();
            foreach (var order in orders)
            {
                _ctx.Orders.Remove(order);
            }
            var shoppingcarts = _ctx.Shoppingcarts.Where(w => w.Id == Id).ToList();
            foreach (var shoppingcart in shoppingcarts)
            {
                _ctx.Shoppingcarts.Remove(shoppingcart);
            }
            var productsId = _ctx.Products.Where(w => w.CreateBy == Id).Select(s=>s.ProductId).ToList();
            foreach (var productId in productsId)
            {
                var shoppingcartsId = _ctx.Shoppingcarts.Where(w => w.ProductId == productId).ToList();
                foreach (var shoppingcartId in shoppingcartsId)
                {
                    _ctx.Shoppingcarts.Remove(shoppingcartId);
                }
            }
            var products = _ctx.Products.Where(w=>w.CreateBy == Id).ToList();
            foreach (var product in products)
            {
                _ctx.Products.Remove(product);
            }
            var wishs = _ctx.Wishes.Where(w => w.Id == Id).ToList();
            foreach (var wish in wishs)
            {
                _ctx.Wishes.Remove(wish);
            }
            var user = _accountService.GetUserRaw(name);
            _ctx.Users.Remove(user);
            _ctx.SaveChanges();
            ViewData["Title"] = "帳號刪除";
            ViewData["Message"] = "使用者帳號刪除成功!";  //顯示訊息
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Response.Cookies.Delete("Login");
            return View("~/Views/Account/ResultMessage.cshtml");
        }
    }
}
