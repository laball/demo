using System.Web.Mvc;

namespace AbpSignalrApi.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //ViewBag.Title = "Home Page";
            //return View();

            return Redirect("/swagger/ui/index");
        }
    }
}
