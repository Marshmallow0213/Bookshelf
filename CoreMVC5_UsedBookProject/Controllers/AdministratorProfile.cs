using CoreMVC5_UsedBookProject.Data;
using CoreMVC5_UsedBookProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using CoreMVC5_UsedBookProject.ViewModels;
namespace CoreMVC5_UsedBookProject.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdministratorProfile : Controller
    {
        private readonly AdminAccountContext _ctx;

        public AdministratorProfile(AdminAccountContext ctx)
        {
            _ctx = ctx;
        }

        [Authorize]
        public async Task<IActionResult> AdministratorData()
        {
            var model = await _ctx.Users.ToListAsync();
            return View(model);
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

            //var administratorUser = await _ctx.Users.FirstOrDefaultAsync(m => m.Id == id);
            var administratorUser = await _ctx.Users.Where(w=>w.Id == id)
                .Select(g => new AdministratorUserViewModel { Id = id, Name = g.Name, Nickname = g.Nickname,Email = g.Email, PhoneNo = g.PhoneNo })
                .FirstOrDefaultAsync();

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
        public async Task<IActionResult> AdministratorCreate([Bind("Name,Nickname,Email,PhoneNo")] AdministratorUser administratorUser)
        {
            if (ModelState.IsValid)
            {
                _ctx.Users.Add(administratorUser);
                await _ctx.SaveChangesAsync();
                return RedirectToAction(nameof(AdministratorHomePage));
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
        public async Task<IActionResult> AdministratorDeleteConfirmed(string id)
        {
            var administratorUser = await _ctx.Users.FindAsync(id);

            if (administratorUser == null)
            {
                return NotFound();
            }

            _ctx.Users.Remove(administratorUser);
            await _ctx.SaveChangesAsync();
            return RedirectToAction(nameof(AdministratorHomePage));
        }

        public async Task<IActionResult> AdministratorEdit(string id)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdministratorEdit(string id, [Bind("Id,Name,Nickname,Email,PhoneNo")] AdministratorUser administratorUser)
        {
            if (id != administratorUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _ctx.Users.Update(administratorUser);
                    await _ctx.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
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

                return RedirectToAction(nameof(AdministratorHomePage));
            }

            return View(administratorUser);
        }

        private bool AdministratorUserExists(string id)
        {
            return _ctx.Users.Any(e => e.Id == id);
        }
    }
}
