using CoreMVC5_UsedBookProject.Data;
using CoreMVC5_UsedBookProject.Interfaces;
using CoreMVC5_UsedBookProject.Models;
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
    public class AdministratorProfileController : Controller
    {
        private readonly AdminAccountContext _ctx;
        private readonly IHashService _hashService;

        public AdministratorProfileController(AdminAccountContext ctx, IHashService hashService)
        {
            _ctx = ctx;
            _hashService = hashService;
        }
        [Authorize]
        public async Task<IActionResult> AdministratorData()
        {
            var data = (from ur in _ctx.UserRoles
                        from u in _ctx.Users
                        from r in _ctx.Roles
                        where ur.UserId == u.Id && ur.RoleId == r.Id
                        select new MixViewModel { UserID = u.Id, UserName = u.Name,RoleName = r.Name }).ToList();
            foreach (var item in data)
            {
                if (item.RoleName == "Top Administrator")
                {
                    item.RoleName = "高級管理員";
                }
                else if (item.RoleName == "common Administrator")
                {
                    item.RoleName = "普通管理員";
                }
                else if (item.RoleName == "Suspended Administrator")
                {
                    item.RoleName = "已停權管理員";
                }
            }
            return View(data);
        }


        public async Task<IActionResult> AdministratorDetail(string id)
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

            var administratorUser = await _ctx.Users.FirstOrDefaultAsync(m => m.Id == id);

            if (administratorUser == null)
            {
                return NotFound();
            }

            return View(administratorUser);
        }

        public IActionResult AdministratorCreate()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdministratorCreate([Bind("Id,Name,Nickname,Password,Email,PhoneNo")] AdministratorUser administratorUser)
        {
            if (ModelState.IsValid)
            {
                string hashedPassword = _hashService.MD5Hash(administratorUser.Password);
                administratorUser.Password = hashedPassword;

                _ctx.Users.Add(administratorUser);
                await _ctx.SaveChangesAsync();

                var userRole = new AdministratorUserRoles
                {
                    UserId = administratorUser.Id,
                    RoleId = "R001"
                };

                _ctx.UserRoles.Add(userRole);
                await _ctx.SaveChangesAsync();

                return RedirectToAction("AdministratorData");
            }

            return View(administratorUser);
        }


        public async Task<IActionResult> AdministratorEdit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var administratorUser = await _ctx.Users.Select(s => new AdministratorUserHomePage { Id = s.Id, Name = s.Name, Nickname = s.Nickname, Email = s.Email, PhoneNo = s.PhoneNo }).FirstOrDefaultAsync(m => m.Id == id);
            if (administratorUser == null)
            {
                return NotFound();
            }
            return View(administratorUser);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdministratorEdit(string id,
            [Bind("Id,Name,Nickname,Password,Email,PhoneNo")] AdministratorUserHomePage administratorUser)
        {
            if (id != administratorUser.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var user = _ctx.Users.Where(w => w.Id == id).FirstOrDefault();
                    _ctx.Entry(user).State = EntityState.Modified;
                    user.Name = administratorUser.Name;
                    user.Nickname = administratorUser.Nickname;
                    user.Email = administratorUser.Email;
                    user.PhoneNo = administratorUser.PhoneNo;
                    await _ctx.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                }
                return RedirectToAction("AdministratorData");
            }
            return View(administratorUser);
        }

        public async Task<IActionResult> AdministratorDelete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var administratorUser = await _ctx.Users.FirstOrDefaultAsync(m => m.Id == id);
            if (administratorUser == null)
            {
                return NotFound();
            }
            return View(administratorUser);
        }
        [HttpPost, ActionName("AdministratorDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var administratorUser = await _ctx.Users.FindAsync(id);
            _ctx.Users.Remove(administratorUser);
            await _ctx.SaveChangesAsync();
            return RedirectToAction("AdministratorData");
        }

        public async Task<IActionResult> AdministratorSuspension(string id)
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

            var administratorUser = await _ctx.Users.FirstOrDefaultAsync(m => m.Id == id);

            if (administratorUser == null)
            {
                return NotFound();
            }

            return View(administratorUser);
        }

        public IActionResult TopAdministrator(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            try
            {
                var data = _ctx.UserRoles.Where(ur => ur.UserId == id).FirstOrDefault();
                var role = _ctx.Roles.Where(r => r.Name == "Top Administrator").FirstOrDefault();
                if (data != null && role != null)
                {
                    var newUserRole = new AdministratorUserRoles
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

            return RedirectToAction("AdministratorData");
        }

        public IActionResult commonAdministrator(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            try
            {
                var data = _ctx.UserRoles.Where(ur => ur.UserId == id).FirstOrDefault();
                var role = _ctx.Roles.Where(r => r.Name == "common Administrator").FirstOrDefault();
                if (data != null && role != null)
                {
                    var newUserRole = new AdministratorUserRoles
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

            return RedirectToAction("AdministratorData");
        }

        public IActionResult SuspendedAdministrator(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            try
            {
                var data = _ctx.UserRoles.Where(ur => ur.UserId == id).FirstOrDefault();
                var role = _ctx.Roles.Where(r => r.Name == "Suspended Administrator").FirstOrDefault();
                if (data != null && role != null)
                {
                    var newUserRole = new AdministratorUserRoles
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

            return RedirectToAction("AdministratorData");
        }

        private bool AdministratorUserExists(string id)
        {
            throw new NotImplementedException();
        }
    }
}
