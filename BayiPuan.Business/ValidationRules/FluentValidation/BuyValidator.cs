using FluentValidation;
using BayiPuan.Entities.Concrete;
using BayiPuan.Business.DependencyResolvers.Ninject;
using BayiPuan.Business.Abstract;

namespace BayiPuan.Business.ValidationRules.FluentValidation
{
   public class BuyValidator:AbstractValidator<Buy>
    {
        public BuyValidator()
        {
        //Eğer CustomRule yazmak istenirse service interfacelerini çözer custom rule için gerekli metodlara ulaşmanızı sağlar
        var buyService = DependencyResolver<IBuyService>.Resolve();
        //Sadece Boş Olamaz Kontrolü Yapar
            RuleFor(x => x.BuyId).NotEmpty();
RuleFor(x => x.Gift).NotEmpty();
RuleFor(x => x.GiftId).NotEmpty();
RuleFor(x => x.User).NotEmpty();
RuleFor(x => x.UserId).NotEmpty();
RuleFor(x => x.BuyDate).NotEmpty();
RuleFor(x => x.BuyAmount).NotEmpty();
RuleFor(x => x.IsApproved).NotEmpty();



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