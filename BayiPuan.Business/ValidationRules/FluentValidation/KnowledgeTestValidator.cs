using FluentValidation;
using BayiPuan.Entities.Concrete;
using BayiPuan.Business.DependencyResolvers.Ninject;
using BayiPuan.Business.Abstract;

namespace BayiPuan.Business.ValidationRules.FluentValidation
{
   public class KnowledgeTestValidator:AbstractValidator<KnowledgeTest>
    {
        public KnowledgeTestValidator()
        {
        //Eğer CustomRule yazmak istenirse service interfacelerini çözer custom rule için gerekli metodlara ulaşmanızı sağlar
        var knowledgeTestService = DependencyResolver<IKnowledgeTestService>.Resolve();
        //Sadece Boş Olamaz Kontrolü Yapar
            RuleFor(x => x.KnowledgeTestId).NotEmpty();
RuleFor(x => x.Question).NotEmpty();
RuleFor(x => x.Answer1).NotEmpty();
RuleFor(x => x.Answer2).NotEmpty();
RuleFor(x => x.Answer3).NotEmpty();
RuleFor(x => x.Answer4).NotEmpty();
RuleFor(x => x.ValidAnswerType).NotEmpty();
RuleFor(x => x.Point).NotEmpty();
RuleFor(x => x.KnowledgeDate).NotEmpty();
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