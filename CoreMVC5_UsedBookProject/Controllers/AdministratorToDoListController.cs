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
    [Authorize(Roles = "Top Administrator,common Administrator")]
    public class AdministratorToDoListController : Controller
    {
        private readonly AdminAccountContext _ctx;
        private readonly IHashService _hashService;

        public AdministratorToDoListController(AdminAccountContext ctx, IHashService hashService)
        {
            _ctx = ctx;
            _hashService = hashService;
        }
        public IActionResult DoList()
        {
            var data = (from ur in _ctx.AdminlistRoles
                        from u in _ctx.Adminlists
                        from r in _ctx.AdminRoles
                        where ur.ListId == u.Id && ur.RoleId == r.Id
                        select new ToDoListViewModels { Id = u.Id, Maintitle = u.Maintitle, Subtitle = u.subtitle, RoleName = r.Name }).ToList();
            foreach (var item in data)
            {
                if (item.RoleName == "undone")
                {
                    item.RoleName = "未完成";
                }
                else if (item.RoleName == "done")
                {
                    item.RoleName = "已完成";
                }
            }
            return View(data);
        }
        [Authorize(Roles = "Top Administrator")]
        public IActionResult DoListForCheck()
        {
            var data = (from ur in _ctx.AdminlistRoles
                        from u in _ctx.Adminlists
                        from r in _ctx.AdminRoles
                        where ur.ListId == u.Id && ur.RoleId == r.Id
                        select new ToDoListViewModels { Id = u.Id, Maintitle = u.Maintitle, Subtitle = u.subtitle, RoleName = r.Name }).ToList();
            foreach (var item in data)
            {
                if (item.RoleName == "checkF")
                {
                    item.RoleName = "審核失敗";
                }
                else if (item.RoleName == "checkT")
                {
                    item.RoleName = "審核成功";
                }
            }
            return View(data);
        }
        public IActionResult DoListCreate()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DoListCreate([Bind("Id,Maintitle,subtitle")] Adminlist list)
        {
            if (ModelState.IsValid)
            {
                _ctx.Adminlists.Add(list);
                await _ctx.SaveChangesAsync();

                var userRole = new AdminlistRole
                {
                    ListId = list.Id,
                    RoleId = "R001"
                };

                _ctx.AdminlistRoles.Add(userRole);
                await _ctx.SaveChangesAsync();

                return RedirectToAction("DoList");
            }

            return View(list);
        }

        public async Task<IActionResult> DoListEdit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var list = await _ctx.Adminlists.Select(t => new ToDoListViewModels { Id = t.Id, Maintitle = t.Maintitle, Subtitle = t.subtitle }).FirstOrDefaultAsync(m => m.Id == id);
            if (list == null)
            {
                return NotFound();
            }
            return View(list);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DoListEdit(string id, [Bind("Id,Maintitle,Subtitle")] ToDoListViewModels List)
        {
            if (id != List.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var list = await _ctx.Adminlists.FindAsync(id);
                    if (list == null)
                    {
                        return NotFound();
                    }
                    list.Id = List.Id;
                    list.Maintitle = List.Maintitle;
                    list.subtitle = List.Subtitle;

                    await _ctx.SaveChangesAsync();

                    return RedirectToAction("DoList");
                }
                catch (DbUpdateConcurrencyException)
                {
                }
                return RedirectToAction("DoList");
            }
            return View(List);
        }
        public async Task<IActionResult> DoListDetail(string id)
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

            var List = await _ctx.Adminlists.FirstOrDefaultAsync(m => m.Id == id);

            if (List == null)
            {
                return NotFound();
            }

            return View(List);
        }
        public async Task<IActionResult> DoListDelete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var list = await _ctx.Adminlists.FirstOrDefaultAsync(m => m.Id == id);
            if (list == null)
            {
                return NotFound();
            }
            return View(list);
        }
        [HttpPost, ActionName("DoListDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var list = await _ctx.Adminlists.FindAsync(id);
            _ctx.Adminlists.Remove(list);
            await _ctx.SaveChangesAsync();
            return RedirectToAction("DoList");
        }
        public async Task<IActionResult> DoListSuspension(string id)
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

            var List = await _ctx.Adminlists.FirstOrDefaultAsync(m => m.Id == id);

            if (List == null)
            {
                return NotFound();
            }

            return View(List);
        }
        public async Task<IActionResult> DoListSuspensionForCheck(string id)
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

            var List = await _ctx.Adminlists.FirstOrDefaultAsync(m => m.Id == id);

            if (List == null)
            {
                return NotFound();
            }

            return View(List);
        }
        public IActionResult undone(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            try
            {
                var data = _ctx.AdminlistRoles.Where(ur => ur.ListId == id).FirstOrDefault();
                var role = _ctx.AdminRoles.Where(r => r.Name == "udone").FirstOrDefault();
                if (data != null && role != null)
                {
                    var newUserRole = new AdminlistRole
                    {
                        ListId = id,
                        RoleId = role.Id
                    };
                    _ctx.Remove(data);
                    _ctx.AdminlistRoles.Add(newUserRole);
                    _ctx.SaveChanges();
                }
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(ex.Data);
            }

            return RedirectToAction("DoList");
        }
        public IActionResult done(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            try
            {
                var data = _ctx.AdminlistRoles.Where(ur => ur.ListId == id).FirstOrDefault();
                var role = _ctx.AdminRoles.Where(r => r.Name == "done").FirstOrDefault();
                if (data != null && role != null)
                {
                    var newUserRole = new AdminlistRole
                    {
                        ListId = id,
                        RoleId = role.Id
                    };
                    _ctx.Remove(data);
                    _ctx.AdminlistRoles.Add(newUserRole);
                    _ctx.SaveChanges();
                }
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(ex.Data);
            }

            return RedirectToAction("DoList");
        }
        public IActionResult checkF(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            try
            {
                var data = _ctx.AdminlistRoles.Where(ur => ur.ListId == id).FirstOrDefault();
                var role = _ctx.AdminRoles.Where(r => r.Name == "checkF").FirstOrDefault();
                if (data != null && role != null)
                {
                    var newUserRole = new AdminlistRole
                    {
                        ListId = id,
                        RoleId = role.Id
                    };
                    _ctx.Remove(data);
                    _ctx.AdminlistRoles.Add(newUserRole);
                    _ctx.SaveChanges();
                }
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(ex.Data);
            }

            return RedirectToAction("DoList");
        }
        public IActionResult checkT(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            try
            {
                var data = _ctx.AdminlistRoles.Where(ur => ur.ListId == id).FirstOrDefault();
                var role = _ctx.AdminRoles.Where(r => r.Name == "checkT").FirstOrDefault();
                if (data != null && role != null)
                {
                    var newUserRole = new AdminlistRole
                    {
                        ListId = id,
                        RoleId = role.Id
                    };
                    _ctx.Remove(data);
                    _ctx.AdminlistRoles.Add(newUserRole);
                    _ctx.SaveChanges();
                }
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(ex.Data);
            }

            return RedirectToAction("DoList");
        }
    }
}
