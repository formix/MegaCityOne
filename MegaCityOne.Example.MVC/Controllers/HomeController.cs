using MegaCityOne.Example.Mvc.Attributes;
using MegaCityOne.Example.Mvc.Data;
using MegaCityOne.Example.Mvc.Models;
using MegaCityOne.Mvc;
using System.Web.Mvc;

namespace MegaCityOne.Example.Mvc.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            UserData data = new UserData()
            {
                Name = "formix",
                Roles = "administrator;engineer"
            };
            
            return View(new HomeModel()
            {
                User = data
            });
        }

        [HttpPost]
        public ActionResult Index(HomeModel model)
        {
            Session["User"] = model.User;
            return View(model);
        }
    }
}