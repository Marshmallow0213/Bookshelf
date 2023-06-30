using CoreMVC5_UsedBookProject.Data;
using CoreMVC5_UsedBookProject.Interfaces;
using CoreMVC5_UsedBookProject.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace CoreMVC5_UsedBookProject.Controllers
{
    public class AdminChartsController : Controller
    {
        private readonly AdminAccountContext _ctx;
        private readonly ProductContext _ctxc;

        public AdminChartsController(AdminAccountContext ctx, ProductContext ctxc)
        {
            _ctx = ctx;
            _ctxc = ctxc;
        }
        public IActionResult ChartsView()
        {
            var adminRoleCounts = _ctx.UserRoles
                .Where(r => r.RoleId == "R001" ||
                            r.RoleId == "R002" ||
                            r.RoleId == "R003")
                .GroupBy(r => r.RoleId)
                .Select(g => g.Count())
                .ToList();

            var userRoleCounts = _ctxc.UserRoles
                .Where(r => r.RoleId == "R001" ||
                            r.RoleId == "R002" ||
                            r.RoleId == "R003")
                .GroupBy(r => r.RoleId)
                .Select(g => g.Count())
                .ToList();

            ViewBag.AdminData = JsonSerializer.Serialize(adminRoleCounts);
            ViewBag.UserData = JsonSerializer.Serialize(userRoleCounts);

            return View();
        }
    }
}
