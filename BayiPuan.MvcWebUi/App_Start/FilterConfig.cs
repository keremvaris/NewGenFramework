using System.Web.Mvc;

namespace BayiPuan.MvcWebUi.App_Start
{
  public class FilterConfig
  {
    protected FilterConfig()
    {
      
    }
    public static void RegisterGlobalFilters(GlobalFilterCollection filters)
    {
      filters.Add(new HandleErrorAttribute());

    }
  }
}