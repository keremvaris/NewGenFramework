using FluentValidation;
using NewGenFramework.Entities.Concrete;
using NewGenFramework.Business.DependencyResolvers.Ninject;
using NewGenFramework.Entities.Concrete;
using NewGenFramework.Business.Abstract;

namespace NewGenFramework.Business.ValidationRules.FluentValidation
{
   public class LanguageWordValidator:AbstractValidator<LanguageWord>
    {
        public LanguageWordValidator()
        {
        //Eğer CustomRule yazmak istenirse service interfacelerini çözer custom rule için gerekli metodlara ulaşmanızı sağlar
        var languageWordService = DependencyResolver<ILanguageWordService>.Resolve();
        //Sadece Boş Olamaz Kontrolü Yapar
            RuleFor(x => x.Id).NotEmpty();
RuleFor(x => x.LanguageId).NotEmpty();
RuleFor(x => x.Code).NotEmpty();
RuleFor(x => x.Value).NotEmpty();


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