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
using System.IO;
using System;
using System.Collections.Generic;

namespace CoreMVC5_UsedBookProject.Controllers
{
    public class AdministratorHomePageController : Controller
    {
        private readonly AdminAccountContext _ctx;

        public AdministratorHomePageController(AdminAccountContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<IActionResult> AdminIndex(string id)
        {

            var model = await _ctx.TextValue.Select(g => new TextboxViewModel { Id = g.Id, TextValue = g.TextValue, Image1 = g.Image1, Image2 = g.Image2, Image3 = g.Image3 }).FirstOrDefaultAsync();
            return View(model);
        }
        public IActionResult UploadImage(TextboxViewModel textboxViewModel)
        {

            //var model = _ctx.TextValue.FirstOrDefault();
            List<IFormFile> filenames = new()
                {
                    textboxViewModel.File1 ?? null,
                    textboxViewModel.File2 ?? null,
                    textboxViewModel.File3 ?? null
                };

            List<string> Images = new()
                {
                    textboxViewModel.Image1,
                    textboxViewModel.Image2,
                    textboxViewModel.Image3
                };
            string[] checkImage = CheckImageName(filenames, Images);
            UploadImages(filenames, Images);
            var user = _ctx.TextValue.Where(w => w.Id == textboxViewModel.Id).FirstOrDefault();
            _ctx.Entry(user).State = EntityState.Modified;
            user.Image1 = checkImage[0];
            user.Image2 = checkImage[1];
            user.Image3 = checkImage[2];
            _ctx.SaveChanges();
            return RedirectToAction("AdminIndex");
        }
        public string[] CheckImageName(List<IFormFile> filenames, List<string> Images)
        {
            string[] checkImage = { "", "", "" };
            for (int i = 0; i <= 2; i++)
            {
                checkImage[i] = filenames[i] == null ? Images[i] : String.Concat($"{i + 1}", Path.GetExtension(Convert.ToString(filenames[i].FileName)));
            }
            return checkImage;
        }
        public void UploadImages(List<IFormFile> filenames, List<string> Images)
        {
            string folderPath = $@"Images\Home";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            int i = 1;
            foreach (var file in Images)
            {
                if (file == "無圖片")
                {
                    string[] files = System.IO.Directory.GetFiles(folderPath, $"*{i++}.*");
                    foreach (string f in files)
                    {
                        System.IO.File.Delete(f);
                    }
                }
                else
                {
                    i++;
                }
            }
            i = 1;
            foreach (var file in filenames)
            {
                if (file != null)
                {
                    string[] files = System.IO.Directory.GetFiles(folderPath, $"{i}.*");
                    foreach (string f in files)
                    {
                        System.IO.File.Delete(f);
                    }
                    var path = $@"{folderPath}\{i++}{Path.GetExtension(Convert.ToString(file.FileName))}";
                    using var stream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite, FileShare.None, 2097152);
                    file.CopyTo(stream);
                }
                else
                {
                    i++;
                }
            }
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
