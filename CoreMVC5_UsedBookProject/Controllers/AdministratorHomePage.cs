using CoreMVC5_UsedBookProject.Data;
using CoreMVC5_UsedBookProject.Interfaces;
using CoreMVC5_UsedBookProject.Models;
using CoreMVC5_UsedBookProject.Repositories;

using CoreMVC5_UsedBookProject.Services;
using CoreMVC5_UsedBookProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.WebRequestMethods;


namespace CoreMVC5_UsedBookProject.Controllers
{
    public class AdministratorHomePage : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly AdminAccountContext _ctx;

        public AdministratorHomePage(AdminAccountContext ctx)
        {
            _ctx = ctx;
        }
        public IActionResult AdministratorHome()
        {
            return View();
        }
        [Authorize(Roles = "Administrator,common user")]
        public async Task<IActionResult> DetailCard()
        {
            var Users = await _ctx.Users.ToListAsync();
            return View(Users);
        }


    }
}
