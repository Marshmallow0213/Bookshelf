using CoreMVC5_UsedBookProject.Data;
using CoreMVC5_UsedBookProject.Interfaces;
using CoreMVC5_UsedBookProject.Models;
using CoreMVC5_UsedBookProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CoreMVC5_UsedBookProject.Controllers
{
    public class MessengerController : Controller
    {
        private readonly AdminAccountContext _ctx;
        private readonly IHashService _hashService;
        private readonly IHubContext<ChatHub> _hubContext;

        public MessengerController(AdminAccountContext ctx, IHashService hashService, IHubContext<ChatHub> hubContext)
        {
            _ctx = ctx;
            _hashService = hashService;
            _hubContext = hubContext;
        }
        public IActionResult enter()
        {
            var data = _ctx.announcements.ToList();
            return View(data);

        }
        public IActionResult enterCreate()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> enterCreate([Bind("Message,CreatedAt")] announcement list)
        {
            if (ModelState.IsValid)
            {
                list.CreatedAt = DateTime.Now;
                _ctx.announcements.Add(list);
                await _ctx.SaveChangesAsync();

                await _hubContext.Clients.All.SendAsync("ReceiveMessage", "公告", list.Message);

                return RedirectToAction("enter");
            }

            return View(list);
        }

        public async Task<IActionResult> enterEdit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var list = await _ctx.announcements.Select(t => new announcement { Id = t.Id, Message = t.Message, CreatedAt = t.CreatedAt}).FirstOrDefaultAsync(m => m.Id == id);
            if (list == null)
            {
                return NotFound();
            }
            return View(list);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult enterEdit(int id, announcement List)
        {
            if (id != List.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var list = _ctx.announcements.Where(w=>w.Id == id).FirstOrDefault();
                    if (list == null)
                    {
                        return NotFound();
                    }
                    _ctx.Entry(list).State = EntityState.Modified;
                    list.Message = List.Message;
                    list.CreatedAt = DateTime.Now;

                    _ctx.SaveChanges();

                    return RedirectToAction("enter");
                }
                catch (DbUpdateConcurrencyException)
                {
                }
                return RedirectToAction("enter");
            }
            return View(List);
        }
        public async Task<IActionResult> enterDelete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var list = await _ctx.announcements.FirstOrDefaultAsync(m => m.Id == id);
            if (list == null)
            {
                return NotFound();
            }
            return View(list);
        }
        [HttpPost, ActionName("enterDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var list = await _ctx.announcements.FindAsync(id);
            _ctx.announcements.Remove(list);
            await _ctx.SaveChangesAsync();
            return RedirectToAction("enter");
        }




        public IActionResult output()
        {
            var data = _ctx.announcements.ToList();
            return View(data);
        }
    }
}
