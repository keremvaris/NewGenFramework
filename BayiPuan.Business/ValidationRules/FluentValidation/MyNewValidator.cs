using FluentValidation;
using BayiPuan.Entities.Concrete;
using BayiPuan.Business.DependencyResolvers.Ninject;
using BayiPuan.Business.Abstract;

namespace BayiPuan.Business.ValidationRules.FluentValidation
{
   public class MyNewValidator:AbstractValidator<MyNew>
    {
        public MyNewValidator()
        {
        //Eğer CustomRule yazmak istenirse service interfacelerini çözer custom rule için gerekli metodlara ulaşmanızı sağlar
        var myNewService = DependencyResolver<IMyNewService>.Resolve();
        //Sadece Boş Olamaz Kontrolü Yapar
            RuleFor(x => x.NewsId).NotEmpty();
RuleFor(x => x.NewsName).NotEmpty();
RuleFor(x => x.NewsImage).NotEmpty();
RuleFor(x => x.NewsImageExt).NotEmpty();
RuleFor(x => x.Description).NotEmpty();
RuleFor(x => x.NewsType).NotEmpty();
RuleFor(x => x.IsActive).NotEmpty();


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