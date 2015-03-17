using System;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace MegaCityOne.Mvc
{
    /// <summary>
    /// This attribute leverage MegaCityOne's Judge security for MVC 
    /// applications. The rule to be advised is mandatory. 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class JudgeAuthorizeAttribute : FilterAttribute, IAuthorizationFilter
    {
        /// <summary>
        /// The rule to be advised by the Judge upon authorization request.
        /// </summary>
        public string Rule { get; set; }

        /// <summary>
        /// Creates an instance of a JudgeAuthorizeAttribute.
        /// </summary>
        /// <param name="rule">The rule to be advised during the MVC 
        /// authorize process.</param>
        public JudgeAuthorizeAttribute(string rule)
        {
            this.Rule = rule;
        }

        /// <summary>
        /// This method executes authorization based on the Judge returned by 
        /// the MegaCityOne.Mvc.Dispatcher.
        /// </summary>
        /// <param name="filterContext">The authorization context.</param>
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            if (AllowAnonymous(filterContext))
            {
                return;
            }

            if (AuthorizeCore(filterContext.HttpContext))
            {
                HttpCachePolicyBase cachePolicy = filterContext.HttpContext.Response.Cache;
                cachePolicy.SetProxyMaxAge(new TimeSpan(0));
                cachePolicy.AddValidationCallback(CacheValidateHandler, null /* data */);
            }
            else
            {
                filterContext.Result = (ActionResult)new HttpUnauthorizedResult();
            }
        }

        private bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }

            IPrincipal originalPrincipal = Thread.CurrentPrincipal;
            Thread.CurrentPrincipal = httpContext.User;
            bool advisal = JudgeDispatcher.Advise(this.Rule, HttpContext.Current);
            Thread.CurrentPrincipal = originalPrincipal;
            return advisal;
        }

        private void CacheValidateHandler(HttpContext context, object data, ref HttpValidationStatus validationStatus)
        {
            validationStatus = HttpValidationStatus.Valid;
            if (!this.AuthorizeCore(new HttpContextWrapper(context)))
            {
                validationStatus = HttpValidationStatus.IgnoreThisRequest;
            }
        }

        private bool AllowAnonymous(AuthorizationContext filterContext)
        {
            return filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), inherit: true) ||
                filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), inherit: true);
        }
    }
}
