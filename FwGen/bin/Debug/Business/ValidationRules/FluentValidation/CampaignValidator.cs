using FluentValidation;
using NewGenFramework.Entities.Concrete;
using NewGenFramework.Business.DependencyResolvers.Ninject;
using NewGenFramework.Entities.Concrete;
using NewGenFramework.Business.Abstract;

namespace NewGenFramework.Business.ValidationRules.FluentValidation
{
   public class CampaignValidator:AbstractValidator<Campaign>
    {
        public CampaignValidator()
        {
        //Eğer CustomRule yazmak istenirse service interfacelerini çözer custom rule için gerekli metodlara ulaşmanızı sağlar
        var campaignService = DependencyResolver<ICampaignService>.Resolve();
        //Sadece Boş Olamaz Kontrolü Yapar
            RuleFor(x => x.CampaignId).NotEmpty();
RuleFor(x => x.CampaignName).NotEmpty();
RuleFor(x => x.CampaignImage).NotEmpty();
RuleFor(x => x.CampaignImageExt).NotEmpty();


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