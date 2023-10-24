using Microsoft.AspNetCore.Mvc;

namespace Service_Api.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
