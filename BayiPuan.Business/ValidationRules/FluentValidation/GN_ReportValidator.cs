using FluentValidation;
using BayiPuan.Entities.Concrete;
using BayiPuan.Business.DependencyResolvers.Ninject;
using BayiPuan.Business.Abstract;

namespace BayiPuan.Business.ValidationRules.FluentValidation
{
  public class GN_ReportValidator : AbstractValidator<GN_Report>
  {
    public GN_ReportValidator()
    {
      //Eğer CustomRule yazmak istenirse service interfacelerini çözer custom rule için gerekli metodlara ulaşmanızı sağlar
      var gN_ReportService = DependencyResolver<IGN_ReportService>.Resolve();
      //Sadece Boş Olamaz Kontrolü Yapar
      RuleFor(x => x.ReportId).NotEmpty();
      RuleFor(x => x.ReportTitle).NotEmpty();
      RuleFor(x => x.ReportSql).NotEmpty();
      RuleFor(x => x.ReportFilter).NotEmpty();
      


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