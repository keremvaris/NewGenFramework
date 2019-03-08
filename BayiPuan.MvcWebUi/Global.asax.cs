using System;
using System.Globalization;
using System.IO.Compression;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using FluentValidation.Mvc;
using BayiPuan.Business.DependencyResolvers.Ninject;
using BayiPuan.MvcWebUi.App_Start;
using BayiPuan.MvcWebUi.GenericVM;
using NewGenFramework.Core.CrossCuttingConcerns.Security;
using NewGenFramework.Core.CrossCuttingConcerns.Security.Web;
using NewGenFramework.Core.Utilities.MVC.Infrastructre;




namespace BayiPuan.MvcWebUi
{
  public class Global : System.Web.HttpApplication
  {

    protected void Application_Start()
    {
      ModelMetadataProviders.Current = new CachedDataAnnotationsModelMetadataProvider();
      ViewEngines.Engines.Clear();
      ViewEngines.Engines.Add(new RazorViewEngine());
      AreaRegistration.RegisterAllAreas();

      RouteConfig.RegisterRoutes(RouteTable.Routes);
      FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
      //BundleConfig.RegisterBundles(BundleTable.Bundles);

      ModelBinders.Binders.Remove(typeof(byte[]));
      ModelBinders.Binders.Add(typeof(byte[]), new UploadModelBinder());
      ModelBinders.Binders.DefaultBinder = new CustomModelBinder();

      ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory(new BusinessModule(),
          /*new ResolveModule(),*/new AutoMapperModule()));

      ModelValidatorProviders.Providers.Clear();

      FluentValidationModelValidatorProvider.Configure(provider =>
      {
        provider.ValidatorFactory = new NinjectValidatoryFactory();
        provider.AddImplicitRequiredValidator = false;
      });
      //ModelBinders.Binders.Add(typeof(Cart), new CartModelBinder());
      //ModelBinders.Binders.Add(typeof(AcceptanceCart), new AcceptanceCartModelBinder());
      //ModelBinders.Binders.Add(typeof(OutCart), new OutCartModelBinder());
      //HibernatingRhinos.Profiler.Appender.EntityFramework.EntityFrameworkProfiler.Initialize();
    }

    public override void Init()
    {
      PostAuthenticateRequest += MvcApplication_PostAuthenticateRequest;
      base.Init();
    }


    protected void Application_AcquireRequestState(object sender, EventArgs e)
    {
      try
      {
        var prin = new GenericPrincipal(new Identity
        {
          Id = new Guid(Session.SessionID),
          Url = Request.RawUrl,

        }, new string[] { });
        Thread.CurrentPrincipal = prin;
      }
      catch (Exception)
      {
      }

      string culture = "tr-TR";

      if (!string.IsNullOrEmpty(Request.Cookies["culture"]?.Value))
      {
        culture = Request.Cookies["culture"].Value;
      }
      else
      {
        if (Request.UserLanguages != null) culture = Request.UserLanguages[0];
      }

      var ci = new CultureInfo(culture);

      Thread.CurrentThread.CurrentUICulture = ci;
      Thread.CurrentThread.CurrentCulture = ci;

    }

    protected void Application_BeginRequest(object sender, EventArgs e)
    {

    }

    private void MvcApplication_PostAuthenticateRequest(object sender, EventArgs e)
    {
      try
      {
        var authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
        if (authCookie == null)
        {
          return;
        }

        var encTicket = authCookie.Value;
        if (String.IsNullOrEmpty(encTicket))
        {
          return;
        }

        var ticket = FormsAuthentication.Decrypt(encTicket);

        var securityUtlities = new SecurityUtilities();
        var identity = securityUtlities.FormsAuthTicketToIdentity(ticket);

        var principal = new GenericPrincipal(identity, identity.Roles);

        HttpContext.Current.User = principal;
        Thread.CurrentPrincipal = principal;
      }
      catch (Exception)
      {

      }

    }
  }
}
