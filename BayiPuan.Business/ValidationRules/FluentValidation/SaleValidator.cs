using FluentValidation;
using BayiPuan.Entities.Concrete;
using BayiPuan.Business.DependencyResolvers.Ninject;
using BayiPuan.Business.Abstract;

namespace BayiPuan.Business.ValidationRules.FluentValidation
{
   public class SaleValidator:AbstractValidator<Sale>
    {
        public SaleValidator()
        {
        //Eğer CustomRule yazmak istenirse service interfacelerini çözer custom rule için gerekli metodlara ulaşmanızı sağlar
        var saleService = DependencyResolver<ISaleService>.Resolve();
        //Sadece Boş Olamaz Kontrolü Yapar
            RuleFor(x => x.SaleId).NotEmpty();
RuleFor(x => x.CustomerId).NotEmpty();
RuleFor(x => x.InvoiceNo).NotEmpty();
RuleFor(x => x.InvoiceImage).NotEmpty().WithMessage("Bu alanı Boş Geçemezsiniz!");
RuleFor(x => x.InvoiceImageExt).NotEmpty();
RuleFor(x => x.ProductId).NotEmpty();
RuleFor(x => x.AmountOfSales).NotEmpty();


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