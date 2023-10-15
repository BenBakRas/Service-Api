using Microsoft.AspNetCore.Mvc;

namespace Service_Api.Controllers
{
    public class Test : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
