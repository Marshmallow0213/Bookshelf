using CoreMVC5_UsedBookProject.Data;
using CoreMVC5_UsedBookProject.Interfaces;
using CoreMVC5_UsedBookProject.Models;
using CoreMVC5_UsedBookProject.Services;
using CoreMVC5_UsedBookProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CoreMVC5_UsedBookProject.Controllers
{
    [Authorize(Roles = "Top Administrator")]
    public class AdminToUserController : Controller
    {
        private readonly ProductContext _ctx;
        private readonly SellerService _sellerService;
        private readonly IHashService _hashService;

        public AdminToUserController(ProductContext ctx, SellerService sellerService, IHashService hashService)
        {
            _ctx = ctx;
            _sellerService = sellerService;
            _hashService = hashService;
        }
        [Authorize]
        public IActionResult UserData()
        {
            var data = (from ur in _ctx.UserRoles
                        from u in _ctx.Users
                        from r in _ctx.Roles
                        where ur.UserId == u.Id && ur.RoleId == r.Id
                        orderby u.Name
                        select new MixUserViewModel { UserID = u.Id, UserName = u.Name, RoleName = r.Name }).ToList();
            foreach (var item in data)
            {
                if (item.RoleName == "User")
                {
                    item.RoleName = "使用者";
                }
                else if (item.RoleName == "Suspension")
                {
                    item.RoleName = "已停權";
                }
            }
            return View(data);
        }

        public async Task<IActionResult> UserDetail(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                var msgObject = new
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    error = "無效請求，必須提供ID喔!"
                };
                return new BadRequestObjectResult(msgObject);
            }

            var User = await _ctx.Users.FirstOrDefaultAsync(m => m.Id == id);

            if (User == null)
            {
                return NotFound();
            }

            return View(User);
        }

        public async Task<IActionResult> UserEdit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _ctx.Users.Select(s => new UserViewModel { Id = s.Id, Name = s.Name, Nickname = s.Nickname, Email = s.Email, PhoneNo = s.PhoneNo}).FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserEdit(string id, [Bind("Id,Name,Nickname,Email,PhoneNo")] UserViewModel User)
        {
            if (id != User.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _ctx.Users.FindAsync(id);
                    if (user == null)
                    {
                        return NotFound();
                    }
                    user.Nickname = User.Nickname;
                    user.Email = User.Email;
                    user.PhoneNo = User.PhoneNo;

                    await _ctx.SaveChangesAsync();

                    return RedirectToAction("UserData");
                }
                catch (DbUpdateConcurrencyException)
                {
                }
                return RedirectToAction("UserData");
            }

            return View(User);
        }



        public async Task<IActionResult> UserDelete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var User = await _ctx.Users.FirstOrDefaultAsync(m => m.Id == id);
            if (User == null)
            {
                return NotFound();
            }
            return View(User);
        }
        [HttpPost, ActionName("UserDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var User = await _ctx.Users.FindAsync(id);
            _ctx.Users.Remove(User);
            await _ctx.SaveChangesAsync();
            _sellerService.DeleteProductFolder(id);
            return RedirectToAction("UserData");
        }
        public async Task<IActionResult> UserSuspension(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                var msgObject = new
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    error = "無效請求，必須提供ID喔!"
                };
                return new BadRequestObjectResult(msgObject);
            }

            var User = await _ctx.Users.FirstOrDefaultAsync(m => m.Id == id);

            if (User == null)
            {
                return NotFound();
            }

            return View(User);
        }

        public IActionResult Seller(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            try
            {
                var data = _ctx.UserRoles.Where(ur => ur.UserId == id).FirstOrDefault();
                var role = _ctx.Roles.Where(r => r.Name == "User").FirstOrDefault();
                if (data != null && role != null)
                {
                    var newUserRole = new UserRoles
                    {
                        UserId = id,
                        RoleId = role.Id
                    };
                    _ctx.Remove(data);
                    _ctx.UserRoles.Add(newUserRole);
                    _ctx.SaveChanges();
                }
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(ex.Data);
            }

            return RedirectToAction("UserData");
        }

        

        public IActionResult Administrator(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            try
            {
                var data = _ctx.UserRoles.Where(ur => ur.UserId == id).FirstOrDefault();
                var role = _ctx.Roles.Where(r => r.Name == "Suspension").FirstOrDefault();
                if (data != null && role != null)
                {
                    var newUserRole = new UserRoles
                    {
                        UserId = id,
                        RoleId = role.Id
                    };
                    _ctx.Remove(data);
                    _ctx.UserRoles.Add(newUserRole);
                    _ctx.SaveChanges();
                }
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(ex.Data);
            }

            return RedirectToAction("UserData");
        }

        private bool AdministratorUserExists(string id)
        {
            throw new NotImplementedException();
        }
    }
}
