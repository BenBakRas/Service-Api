using Microsoft.AspNetCore.Mvc;

namespace Service_Api.Controllers
{
    public class DiscountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
