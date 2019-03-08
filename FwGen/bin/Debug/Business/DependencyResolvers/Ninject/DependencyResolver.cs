using Ninject;

namespace NewGenFramework.Business.DependencyResolvers.Ninject
{
    public class DependencyResolver<T>
    {
        public static T Resolve()
        {
            IKernel kernel = new StandardKernel(new ResolveModule(),new AutoMapperModule());
           
            return kernel.Get<T>();
        }
    }
}
