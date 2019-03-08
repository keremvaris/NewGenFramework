using FluentValidation;
using NewGenFramework.Entities.Concrete;
using NewGenFramework.Business.DependencyResolvers.Ninject;
using NewGenFramework.Entities.Concrete;
using NewGenFramework.Business.Abstract;

namespace NewGenFramework.Business.ValidationRules.FluentValidation
{
   public class RoleValidator:AbstractValidator<Role>
    {
        public RoleValidator()
        {
        //Eğer CustomRule yazmak istenirse service interfacelerini çözer custom rule için gerekli metodlara ulaşmanızı sağlar
        var roleService = DependencyResolver<IRoleService>.Resolve();
        //Sadece Boş Olamaz Kontrolü Yapar
            RuleFor(x => x.RoleId).NotEmpty();
RuleFor(x => x.RoleName).NotEmpty();


        //Custom Rule Kullanımı Aşağıdaki gibidir
         //Custom(rm =>
            //{
            //var useremail = userService.UniqueEmail(rm.Email);
            //    if (rm.Agreement != true /*&& useremail.Email != null*/)
            //    {
            //        return new ValidationFailure(/*you must type the property name here, you must type the error message here */);
        //    }
        //    return null;
        //});
        }
}
}