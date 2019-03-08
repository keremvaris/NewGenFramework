using System;
using System.Web.Mvc;
using System.Web.Routing;
using BayiPuan.Business.DependencyResolvers.Ninject;
using Ninject;

namespace BayiPuan.MvcWebUi.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private readonly IKernel _kernel;

        public NinjectControllerFactory()
        {
            _kernel = new StandardKernel(new ResolveModule());
        }

        /// <summary>
        /// Web api DI çözümü Kernel'a ihtiyaç duyuyor. Daha iyi bir çözüm implemente edilebilir. Şimdilik uygun.
        /// </summary>
        public IKernel Kernel
        {
            get { return _kernel; }
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : (IController)_kernel.Get(controllerType);
        }
    }
}