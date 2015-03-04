using MegaCityOne.Example.Mvc.Attributes;
using MegaCityOne.Mvc;
using System.Web.Mvc;

namespace MegaCityOne.Example.Mvc.Controllers
{
    [DummyAuthAttribute]
    public class HomeController : Controller
    {
        [JudgeAuthorize(Rule = "CanDisplayMainPage")]
        public ActionResult Index()
        {
            return View();
        }
    }
}