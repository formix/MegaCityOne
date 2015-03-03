using System.Web.Mvc;

namespace MegaCityOne.Example.MVC.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Http404()
        {
            Response.StatusCode = 404;

            return View();
        }
    }
}