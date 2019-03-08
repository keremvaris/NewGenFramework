using FluentValidation;
using NewGenFramework.Entities.Concrete;
using NewGenFramework.Business.DependencyResolvers.Ninject;
using NewGenFramework.Entities.Concrete;
using NewGenFramework.Business.Abstract;

namespace NewGenFramework.Business.ValidationRules.FluentValidation
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
RuleFor(x => x.ApprovedDate).NotEmpty();
RuleFor(x => x.NotApproved).NotEmpty();
RuleFor(x => x.NotApprovedDate).NotEmpty();
RuleFor(x => x.Reason).NotEmpty();
RuleFor(x => x.EditUserId).NotEmpty();
RuleFor(x => x.BuyState).NotEmpty();
RuleFor(x => x.Brand).NotEmpty();
RuleFor(x => x.BrandId).NotEmpty();


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