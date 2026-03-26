using Microsoft.AspNetCore.Mvc;

namespace DiaCare.WebAPI.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
