using CoreMVC5_UsedBookProject.Data;
using CoreMVC5_UsedBookProject.Models;
using CoreMVC5_UsedBookProject.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Linq;


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

        public async Task<IActionResult> AdminIndex(string id)
        {

            var model = await _ctx.TextValue.FirstOrDefaultAsync();
            return View(model);
        }
    


        public async Task<IActionResult> EditHomeText(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var textbox = await _ctx.TextValue.FirstOrDefaultAsync(t => t.Id == id);
            if (textbox == null)
            {
                return NotFound();
            }
            return View(textbox);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditHomeText(string id,
            [Bind("Id,TextValue")] Textbox textbox)
        {
            if (id != textbox.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var user = _ctx.TextValue.Where(w => w.Id == id).FirstOrDefault();
                    _ctx.Entry(user).State = EntityState.Modified;
                    user.Id = textbox.Id;
                    user.TextValue = textbox.TextValue;
                    await _ctx.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                }
                return RedirectToAction("AdminIndex");
            }
            return View(textbox);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
