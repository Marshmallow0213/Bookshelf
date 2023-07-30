using CoreMVC5_UsedBookProject.Data;
using CoreMVC5_UsedBookProject.Interfaces;
using CoreMVC5_UsedBookProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.Json;

namespace CoreMVC5_UsedBookProject.Controllers
{
    [Authorize(Roles = "Top Administrator,common Administrator")]
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

            var countadmin1 = _ctx.UserRoles.Where(r => r.RoleId == "R001").Count();
            var countadmin2 = _ctx.UserRoles.Where(r => r.RoleId == "R002").Count();
            var countadmin3 = _ctx.UserRoles.Where(r => r.RoleId == "R003").Count();
            var totalAdminRoleCount = adminRoleCounts.Sum();
            ViewBag.AdminData1 = JsonSerializer.Serialize(countadmin1);
            ViewBag.AdminData2 = JsonSerializer.Serialize(countadmin2);
            ViewBag.AdminData3 = JsonSerializer.Serialize(countadmin3);
            ViewBag.AdminDatat = JsonSerializer.Serialize(totalAdminRoleCount);
            ViewBag.AdminData = JsonSerializer.Serialize(adminRoleCounts);



            var userRoleCounts = _ctxc.UserRoles
                .Where(r => r.RoleId == "R001" ||
                            r.RoleId == "R002")
                .GroupBy(r => r.RoleId)
                .Select(g => g.Count())
                .ToList();

            var countuser1 = _ctxc.UserRoles.Where(r => r.RoleId == "R001").Count();
            var countuser2 = _ctxc.UserRoles.Where(r => r.RoleId == "R002").Count();
            var totalUserRoleCount = userRoleCounts.Sum();
            ViewBag.UserData1 = JsonSerializer.Serialize(countuser1);
            ViewBag.UserData2 = JsonSerializer.Serialize(countuser2);
            ViewBag.UserDatat = JsonSerializer.Serialize(totalUserRoleCount);
            ViewBag.UserData = JsonSerializer.Serialize(userRoleCounts);

            return View();
        }
    }
}
