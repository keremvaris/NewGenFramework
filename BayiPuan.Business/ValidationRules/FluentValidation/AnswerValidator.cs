using FluentValidation;
using BayiPuan.Entities.Concrete;
using BayiPuan.Business.DependencyResolvers.Ninject;
using BayiPuan.Business.Abstract;

namespace BayiPuan.Business.ValidationRules.FluentValidation
{
   public class AnswerValidator:AbstractValidator<Answer>
    {
        public AnswerValidator()
        {
        //Eğer CustomRule yazmak istenirse service interfacelerini çözer custom rule için gerekli metodlara ulaşmanızı sağlar
        var answerService = DependencyResolver<IAnswerService>.Resolve();
        //Sadece Boş Olamaz Kontrolü Yapar
            RuleFor(x => x.AnswerId).NotEmpty();
RuleFor(x => x.KnowledgeTest).NotEmpty();
RuleFor(x => x.KnowledgeTestId).NotEmpty();
RuleFor(x => x.User).NotEmpty();
RuleFor(x => x.UserId).NotEmpty();
RuleFor(x => x.AnswerDate).NotEmpty();
RuleFor(x => x.ValidAnswer).NotEmpty();


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