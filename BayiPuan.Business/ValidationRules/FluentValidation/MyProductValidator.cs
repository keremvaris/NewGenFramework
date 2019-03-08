using FluentValidation;
using BayiPuan.Entities.Concrete;
using BayiPuan.Business.DependencyResolvers.Ninject;
using BayiPuan.Business.Abstract;

namespace BayiPuan.Business.ValidationRules.FluentValidation
{
   public class MyProductValidator:AbstractValidator<MyProduct>
    {
        public MyProductValidator()
        {
        //Eğer CustomRule yazmak istenirse service interfacelerini çözer custom rule için gerekli metodlara ulaşmanızı sağlar
        var myProductService = DependencyResolver<IMyProductService>.Resolve();
        //Sadece Boş Olamaz Kontrolü Yapar
            RuleFor(x => x.MyProductId).NotEmpty();
RuleFor(x => x.ProductName).NotEmpty();
RuleFor(x => x.MyProductImage).NotEmpty();
RuleFor(x => x.MyProductImageExt).NotEmpty();
RuleFor(x => x.Description).NotEmpty();
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