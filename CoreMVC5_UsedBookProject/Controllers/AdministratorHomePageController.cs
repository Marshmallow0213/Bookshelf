using CoreMVC5_UsedBookProject.Data;
using CoreMVC5_UsedBookProject.Interfaces;
using CoreMVC5_UsedBookProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [Authorize]
        public async Task<IActionResult> AdministratorHomePage()
        {
            var model = await _ctx.Users.ToListAsync();

            return View(model);
        }
    }
}
