using Microsoft.AspNetCore.Mvc;

namespace CoreMVC5_UsedBookProject.Controllers
{
    public class AdministratorHomePage : Controller
    {
        public IActionResult Home()
        {
            return View();
        }
    }
}
