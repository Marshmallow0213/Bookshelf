﻿using CoreMVC5_UsedBookProject.Data;
using CoreMVC5_UsedBookProject.Interfaces;
using CoreMVC5_UsedBookProject.Models;
using CoreMVC5_UsedBookProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace CoreMVC5_UsedBookProject.Controllers
{
    public class AdministratorHomePageController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly AdminAccountContext _ctx;

        public AdministratorHomePageController(AdminAccountContext ctx)
        {
            _ctx = ctx;
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
            var NickName = HttpContext.Request.Cookies["NickName"];
            ViewBag.NickName = NickName;
            var UserIcon = HttpContext.Request.Cookies["UserIcon"];
            ViewBag.UserIcon = UserIcon;
        }
        [Authorize]
        public async Task<IActionResult> AdministratorHomePage()
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
        public async Task<IActionResult>
            Create([Bind("Name,Nickname,Email,PhoneNo")] AdministratorUser administratorUser)
        {
            if (ModelState.IsValid)
            {
                _ctx.Users.Add(administratorUser);
                await _ctx.SaveChangesAsync();
                return RedirectToAction("Index");
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
            return RedirectToAction("Index");
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
        public async Task<IActionResult> Edit(string id,
            [Bind("Name,Nickname,Email,PhoneNo")] AdministratorUser administratorUser)
        {
            if (id != administratorUser.Id)
            {
                return NotFound();
            }
            if(ModelState.IsValid)
            {
                try
                {
                    _ctx.Users.Update(administratorUser);
                    await _ctx.SaveChangesAsync();
                }
                catch(DbUpdateConcurrencyException)
                {
                    if (!AdministratorUserExists(administratorUser.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(administratorUser);
        }

        private bool AdministratorUserExists(string id)
        {
            throw new NotImplementedException();
        }
    }
}
