using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    }
}
