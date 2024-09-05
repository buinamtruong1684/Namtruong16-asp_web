using Microsoft.AspNetCore.Mvc;

namespace BaiTap3.Controllers
{
    public class NhomController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
