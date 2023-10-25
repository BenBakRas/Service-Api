using Microsoft.AspNetCore.Mvc;

namespace Service_Api.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
