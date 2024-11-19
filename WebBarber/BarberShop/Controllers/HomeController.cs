using System.Web.Mvc;

namespace BarberShop.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Blog()
        {
            return View();
        }
        public ActionResult ĐánhGiá()
        {
            return View();
        }
        public ActionResult ĐặtLịch()
        {
            return View();
        }
        public ActionResult HomePage()
        {
            return View();
        }
        public ActionResult Products()
        {
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult SignUp()
        {
            return View();
        }
        

    }
}