using FluentValidation;
using BayiPuan.Entities.Concrete;
using BayiPuan.Business.DependencyResolvers.Ninject;
using BayiPuan.Business.Abstract;

namespace BayiPuan.Business.ValidationRules.FluentValidation
{
  public class CustomerValidator : AbstractValidator<Customer>
  {
    public CustomerValidator()
    {
      //Eğer CustomRule yazmak istenirse service interfacelerini çözer custom rule için gerekli metodlara ulaşmanızı sağlar
      var customerService = DependencyResolver<ICustomerService>.Resolve();
      //Sadece Boş Olamaz Kontrolü Yapar
      RuleFor(x => x.CustomerId).NotEmpty();
      RuleFor(x => x.CustomerName).NotEmpty();
      RuleFor(x => x.TaxNo).NotEmpty();
      RuleFor(x => x.TaxAdministration).NotEmpty();
      RuleFor(x => x.RelationalPersonName).NotEmpty();
      RuleFor(x => x.RelationalPersonSurname).NotEmpty();
      RuleFor(x => x.MobilePhone).NotEmpty();


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