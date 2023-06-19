using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CoreMVC5_UsedBookProject.Controllers
{
    public class AdministratorHomePageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult AdministratorHomePage()
        {
            return View();
        }
        private readonly IWebHostEnvironment _webHostEnvironment;

        // GET: Admin/GetCarouselImages
        public IActionResult GetCarouselImages()
        {
            // 假設這裡是從某個數據源獲取輪播圖片的列表，以下為示例代碼
            List<object> carouselImages = new List<object>
            {
                new { Id = 1, Url = "~/adminimg/柴犬.jpg", Alt = "Slide 1" },
                new { Id = 2, Url = "~/adminimg/黃金獵犬.jpg", Alt = "Slide 2" },
                new { Id = 3, Url = "~/adminimg/邊境牧羊犬.jpg", Alt = "Slide 3" }
            };

            return Json(carouselImages);
        }

        // POST: Admin/UploadImage
        [HttpPost]
        public IActionResult UploadImage(IFormFile file)
        {
            // 執行上傳圖片的相關邏輯
            // ...

            return RedirectToAction("Index");
        }

        // POST: Admin/DeleteImage
        [HttpPost]
        // POST: Home/DeleteImage
        [HttpPost]
        public IActionResult DeleteImage(int id)
        {
            // 執行刪除圖片的相關邏輯
            // 根據圖片的Id執行刪除操作

            // 假設刪除成功，返回一個成功的訊息，可以是任意格式的回應，這裡以JSON格式為例
            return Json(new { success = true });
        }


        // POST: Admin/SaveChanges
        [HttpPost]
        public IActionResult SaveChanges(List<object> carouselImages)
        {
            // 執行保存變更的相關邏輯
            // ...

            return RedirectToAction("Index");
        }
    }
}
