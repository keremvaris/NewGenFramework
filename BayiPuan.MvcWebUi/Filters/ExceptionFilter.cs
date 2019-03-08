using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewGenFramework.Core.CrossCuttingConcerns.ExceptionHandling.Exceptions;
using NewGenFramework.Core.Utilities.MVC.Enums;

namespace BayiPuan.MvcWebUi.Filters
{
  public class ExceptionFilter : ActionFilterAttribute, IExceptionFilter
  {
    public void OnException(ExceptionContext filterContext)
    {
      filterContext.ExceptionHandled = true;

      if (filterContext.Exception is NotificationException)
      {
        filterContext.Result = new ViewResult
        {
          TempData = new TempDataDictionary
          {
            {$"NewGenFramework.notifications.{NotifyType.Error}",filterContext.Exception.Message }
          }
        };
      }
      else
      {
        filterContext.Result = new ViewResult()
        {
          ViewName = "NoAuthorize",
          ViewData = new ViewDataDictionary(filterContext.Exception)
        };
      }

    }
  }
}