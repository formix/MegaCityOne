using MegaCityOne.Mvc;
using System.Web.Mvc;

namespace MegaCityOne.Example.Mvc.Controllers
{
    public class HomeController : Controller
    {
        [JudgeAuthorize(Rule = "IsLocalAdmin")]
        public ActionResult Index()
        {
            return View();
        }
    }
}