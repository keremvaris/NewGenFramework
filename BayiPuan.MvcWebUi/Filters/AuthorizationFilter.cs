using System;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using NewGenFramework.Core.CrossCuttingConcerns.Security.Web;

namespace BayiPuan.MvcWebUi.Filters
{
  public class AuthorizationFilter : FilterAttribute, IAuthorizationFilter
  {
    public void OnAuthorization(AuthorizationContext filterContext)
    {
      try
      {
        var authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];

        var encTicket = authCookie?.Value;
        if (String.IsNullOrEmpty(encTicket))
        {

          filterContext.Result = new RedirectToRouteResult(
            new RouteValueDictionary { { "controller", "Account" },
              { "action", "SignIn" }

            });
          return;
        }
        var ticket = FormsAuthentication.Decrypt(encTicket);
        var securityUtilities = new SecurityUtilities();
        var identity = securityUtilities.FormsAuthTicketToIdentity(ticket);
        var principal = new GenericPrincipal(identity, identity.Roles);
        HttpContext.Current.User = principal;
        Thread.CurrentPrincipal = principal;
      }
      catch
      {
        // ignored
      }
    }
  }
}