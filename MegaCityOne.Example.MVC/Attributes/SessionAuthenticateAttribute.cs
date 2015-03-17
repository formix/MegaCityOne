using MegaCityOne.Example.Mvc.Data;
using System;
using System.Security.Principal;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace MegaCityOne.Example.Mvc.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class SessionAuthenticateAttribute : ActionFilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            var user = (UserData)filterContext.HttpContext.Session["User"];
            if (user != null)
            {
                filterContext.HttpContext.User = new GenericPrincipal(
                    new GenericIdentity(user.Name), user.Roles.Split(';'));
            }

        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            if (AllowAnonymous(filterContext))
            {
                return;
            }

            var user = filterContext.HttpContext.User;
            if (user == null || !user.Identity.IsAuthenticated)
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }



        private bool AllowAnonymous(AuthenticationChallengeContext filterContext)
        {
            return filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), inherit: true) ||
                filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), inherit: true);
        }
    }
}