using AutoMapper;
using Ninject.Modules;

namespace BayiPuan.Business.DependencyResolvers.Ninject
{
   public  class AutoMapperModule:NinjectModule
    {
        public override void Load()
        {
            Bind<IMapper>().ToConstant(CreateConfiguration().CreateMapper()).InSingletonScope();
        }

        private MapperConfiguration CreateConfiguration()
        {
            var config=new MapperConfiguration(cfg =>
            {
                cfg.AddProfiles(GetType().Assembly);
            });
            return config;
        }
    }
}
