using Ninject;
using Ninject.Modules;

namespace BayiPuan.Business.DependencyResolvers.Ninject
{
    public class ResolveModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Load(new BusinessModule());
            //var soaSetting = ConfigurationManager.AppSettings[/*SOA webconfig Key*/];

            //var soa = !string.IsNullOrEmpty(soaSetting) && soaSetting.ToBoolean();

            //if (soa)
            //{
            //    Kernel.Load(new ServiceModule());
            //}
            //else
            //{
            //    Kernel.Load(new BusinessModule());
            //}
        }
    }
}
