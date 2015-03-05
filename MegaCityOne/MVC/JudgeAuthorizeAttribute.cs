using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
        public JudgeAuthorizeAttribute(string rule)
        {
            this.Rule = null;
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
            Judge judge = Dispatcher.Current.Dispatch();
            bool advisal = judge.Advise(this.Rule, httpContext);
            Dispatcher.Current.Returns(judge);
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

    }
}
