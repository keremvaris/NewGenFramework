using FluentValidation;
using BayiPuan.Entities.Concrete;
using BayiPuan.Business.DependencyResolvers.Ninject;
using BayiPuan.Business.Abstract;

namespace BayiPuan.Business.ValidationRules.FluentValidation
{
   public class LanguageValidator:AbstractValidator<Language>
    {
        public LanguageValidator()
        {
        //Eğer CustomRule yazmak istenirse service interfacelerini çözer custom rule için gerekli metodlara ulaşmanızı sağlar
        var languageService = DependencyResolver<ILanguageService>.Resolve();
        //Sadece Boş Olamaz Kontrolü Yapar
            RuleFor(x => x.LanguageId).NotEmpty();
RuleFor(x => x.Name).NotEmpty();
RuleFor(x => x.Code).NotEmpty();


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