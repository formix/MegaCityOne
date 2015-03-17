using MegaCityOne.Example.Mvc.Attributes;
using MegaCityOne.Example.Mvc.Data;
using MegaCityOne.Example.Mvc.Models;
using MegaCityOne.Mvc;
using System.Security.Principal;
using System.Web.Mvc;

namespace MegaCityOne.Example.Mvc.Controllers
{
    [SessionAuthenticate]
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            if (Session["User"] != null)
            {
                UserData userData = (UserData)Session["User"];
                var homeModel = new HomeModel();
                homeModel.User = userData.Name;
                foreach (var role in userData.Roles.Split(';'))
                {
                    homeModel.SetSelected(role, true);
                }

                return View(homeModel);
            }
            else
            {
                return View(new HomeModel()
                {
                    User = "formix",
                });
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Index(HomeModel model)
        {
            string roles = "";
            foreach (var role in model.Roles)
            {
                if (role.Selected)
                {
                    if (roles.Length > 0)
                    {
                        roles += ";";
                    }
                    roles += role.Name;
                }
            }

            var user = new UserData()
            {
                Name = model.User,
                Roles = roles
            };

            Session["User"] = user;

            HttpContext.User = new GenericPrincipal(
                new GenericIdentity(user.Name), user.Roles.Split(';'));

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Logoff()
        {
            Session["User"] = null;
            HttpContext.User = new GenericPrincipal(new GenericIdentity(""), new string[0]);
            return Redirect("~/Home/Index");
        }

        [JudgeAuthorize("CanCreateProject")]
        public ActionResult CreateProject()
        {
            return Content("Project created");
        }

        [JudgeAuthorize("CanManageUsers")]
        public ActionResult ManageUsers()
        {
            return Content("You can manage users");
        }
    }
}