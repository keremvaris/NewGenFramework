using System;
using FluentValidation;
using Ninject;

namespace BayiPuan.Business.DependencyResolvers.Ninject
{
   public class NinjectValidatoryFactory: ValidatorFactoryBase
    {
        private readonly IKernel _kernel;

        public NinjectValidatoryFactory()
        {
            _kernel = new StandardKernel(new ValidationModule());
        }

        public override IValidator CreateInstance(Type validatorType)
        {
            //var bindings = (List<IBinding>)_kernel.GetBindings(validatorType);

            //if (bindings.Count > 0)
            //    return (IValidator)_kernel.Get(validatorType);

            //return null;

            return (validatorType == null ) ? null : (IValidator)_kernel.TryGet(validatorType);
        }
    }
}
