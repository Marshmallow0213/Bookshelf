using CoreMVC5_UsedBookProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace CoreMVC5_UsedBookProject.Controllers
{
    public class BuyerController : Controller
    {
        private readonly BuyerService _buyerService;

        public BuyerController(BuyerService buyerService)
        {
            _buyerService = buyerService;
        }
        public IActionResult Index()
        {
            
            return View();
        }
        public IActionResult Details(string ProductId)
        {
            var name = User.Identity.Name;
            if (ProductId == null)
            {
                return NotFound();
            }
            else
            {
                var product = _buyerService.GetProduct(ProductId);
                if (product == null)
                {
                    return NotFound();
                }
                else
                {
                    return View(product);
                }
            }
        }
    }
}
