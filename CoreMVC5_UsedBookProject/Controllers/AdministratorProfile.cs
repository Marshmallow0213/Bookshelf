using CoreMVC5_UsedBookProject.Data;
using CoreMVC5_UsedBookProject.Interfaces;
using CoreMVC5_UsedBookProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVC5_UsedBookProject.Controllers
{
    public class AdministratorProfile : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly AdminAccountContext _ctx;
        private readonly IHashService _hashService;

        public AdministratorProfile(AdminAccountContext ctx, IHashService hashService)
        {
            _ctx = ctx;
            _hashService = hashService;
        }
        [Authorize]
        public async Task<IActionResult> AdministratorData()
        {
            var model = await _ctx.Users.ToListAsync();

            return View(model);
        }
        [Authorize(Roles = "Administrator")]
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
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult AdministratorCreate()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> AdministratorCreate([Bind("Id,Name,Nickname,Password,Email,PhoneNo")] AdministratorUser administratorUser)
        {
            if (ModelState.IsValid)
            {
                // 对密码进行加密
                string hashedPassword = _hashService.MD5Hash(administratorUser.Password);
                administratorUser.Password = hashedPassword;

                _ctx.Users.Add(administratorUser);
                await _ctx.SaveChangesAsync();
                return RedirectToAction("AdministratorData");
            }
            return View(administratorUser);
        }



        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> AdministratorEdit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var administratorUser = await _ctx.Users.FirstAsync(m => m.Id == id);
            if (administratorUser == null)
            {
                return NotFound();
            }
            return View(administratorUser);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> AdministratorEdit(string id,
            [Bind("Id,Name,Nickname,Password,Email,PhoneNo")] AdministratorUser administratorUser)
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
                    user.Password = administratorUser.Password;
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


        [HttpGet]
        [Authorize(Roles = "Administrator")]
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
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var administratorUser = await _ctx.Users.FindAsync(id);
            _ctx.Users.Remove(administratorUser);
            await _ctx.SaveChangesAsync();
            return RedirectToAction("AdministratorData");
        }
        private bool AdministratorUserExists(string id)
        {
            throw new NotImplementedException();
        }
    }
}
