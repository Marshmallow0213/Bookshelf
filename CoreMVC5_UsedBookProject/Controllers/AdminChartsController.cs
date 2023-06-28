using CoreMVC5_UsedBookProject.Data;
using CoreMVC5_UsedBookProject.Interfaces;
using CoreMVC5_UsedBookProject.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CoreMVC5_UsedBookProject.Controllers
{
    public class AdminChartsController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly AdminAccountContext _ctx;
        private readonly IHashService _hashService;

        public AdminChartsController(AdminAccountContext ctx)
        {
            _ctx = ctx;
        }

        public IActionResult AdminChartsView()
        {
            //var users = _ctx.Users.ToList();
            string[] userName = _ctx.Users.Select(u => u.Name).ToArray();
            string[] userData = _ctx.Users.Select(u => u.Nickname).ToArray();

            ViewBag.UserName = userName;
            ViewBag.UserData = userData;

            return View();
        }
    }
}
