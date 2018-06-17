using System.Web.Mvc;

namespace StudentTestingSystem.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Дипломный проект";
            return View();
        }
    }
}