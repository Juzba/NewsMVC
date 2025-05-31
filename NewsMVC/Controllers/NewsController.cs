using Microsoft.AspNetCore.Mvc;

namespace NewsMVC.Controllers
{
    public class NewsController : Controller
    {
        public IActionResult News()
        {
            return View();
        }


        public IActionResult Edit()
        {
            return View();
        }
    }
}
