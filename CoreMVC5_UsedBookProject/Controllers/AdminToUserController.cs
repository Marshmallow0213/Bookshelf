﻿using CoreMVC5_UsedBookProject.Data;
using CoreMVC5_UsedBookProject.Interfaces;
using CoreMVC5_UsedBookProject.Models;
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
        private readonly IHashService _hashService;

        public AdminToUserController(ProductContext ctx, IHashService hashService)
        {
            _ctx = ctx;
            _hashService = hashService;
        }
        [Authorize]
        public async Task<IActionResult> UserData()
        {
            var model = await _ctx.Users.ToListAsync();
            return View(model);
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

            var administratorUser = await _ctx.Users.FirstOrDefaultAsync(m => m.Id == id);

            if (administratorUser == null)
            {
                return NotFound();
            }

            return View(administratorUser);
        }


        public async Task<IActionResult> UserEdit(string id)
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
        public async Task<IActionResult> UserEdit(string id,
            [Bind("Id,Name,Nickname,Password,Email,PhoneNo")] User User)
        {
            if (id != User.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var user = _ctx.Users.Where(w => w.Id == id).FirstOrDefault();
                    _ctx.Entry(user).State = EntityState.Modified;
                    user.Name = User.Name;
                    user.Password = User.Password;
                    user.Nickname = User.Nickname;
                    user.Email = User.Email;
                    user.PhoneNo = User.PhoneNo;
                    await _ctx.SaveChangesAsync();
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
                var role = _ctx.Roles.Where(r => r.Name == "Seller").FirstOrDefault();
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

        public IActionResult Buyer(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            try
            {
                var data = _ctx.UserRoles.Where(ur => ur.UserId == id).FirstOrDefault();
                var role = _ctx.Roles.Where(r => r.Name == "Buyer").FirstOrDefault();
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
                var role = _ctx.Roles.Where(r => r.Name == "Administrator").FirstOrDefault();
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
