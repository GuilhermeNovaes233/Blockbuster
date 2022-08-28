using Microsoft.AspNetCore.Mvc;

namespace Blockbuster.UI.Controllers
{
    public class MoviesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
