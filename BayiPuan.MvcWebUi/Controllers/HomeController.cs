using System.Web;
using System.Web.Mvc;
using BayiPuan.MvcWebUi.Filters;
using BayiPuan.MvcWebUi.Infrastructure;

namespace BayiPuan.MvcWebUi.Controllers
{
  [AuthorizationFilter]
  public class HomeController : BaseController
  {
    // GET: Home
    public ActionResult ChangeCulture(string dilId, string lang, string returnUrl)
    {
      Response.Cookies.Add(new HttpCookie("culture", lang));
      return Redirect(returnUrl);
    }
  }
}