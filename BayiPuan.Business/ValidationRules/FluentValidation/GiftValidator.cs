using FluentValidation;
using BayiPuan.Entities.Concrete;
using BayiPuan.Business.DependencyResolvers.Ninject;
using BayiPuan.Business.Abstract;

namespace BayiPuan.Business.ValidationRules.FluentValidation
{
  public class GiftValidator : AbstractValidator<Gift>
  {
    public GiftValidator()
    {
      //Eğer CustomRule yazmak istenirse service interfacelerini çözer custom rule için gerekli metodlara ulaşmanızı sağlar
      var giftService = DependencyResolver<IGiftService>.Resolve();
      //Sadece Boş Olamaz Kontrolü Yapar
      RuleFor(x => x.GiftId).NotEmpty();
      RuleFor(x => x.CategoryId).NotEmpty();
      RuleFor(x => x.BrandId).NotEmpty();
      RuleFor(x => x.Usage).NotEmpty();
      RuleFor(x => x.Coverage).NotEmpty();
      RuleFor(x => x.ValidityPeriod).NotEmpty();
      RuleFor(x => x.Indivisible).NotEmpty();
      RuleFor(x => x.Combining).NotEmpty();
      RuleFor(x => x.TermOfUse).NotEmpty();
      RuleFor(x => x.Cancellation).NotEmpty();
    
      RuleFor(x => x.Description).NotEmpty();
      RuleFor(x => x.GiftPoint).NotEmpty();


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