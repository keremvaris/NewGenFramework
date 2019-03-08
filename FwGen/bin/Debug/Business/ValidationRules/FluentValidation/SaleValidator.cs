using FluentValidation;
using NewGenFramework.Entities.Concrete;
using NewGenFramework.Business.DependencyResolvers.Ninject;
using NewGenFramework.Entities.Concrete;
using NewGenFramework.Business.Abstract;

namespace NewGenFramework.Business.ValidationRules.FluentValidation
{
   public class SaleValidator:AbstractValidator<Sale>
    {
        public SaleValidator()
        {
        //Eğer CustomRule yazmak istenirse service interfacelerini çözer custom rule için gerekli metodlara ulaşmanızı sağlar
        var saleService = DependencyResolver<ISaleService>.Resolve();
        //Sadece Boş Olamaz Kontrolü Yapar
            RuleFor(x => x.SaleId).NotEmpty();
RuleFor(x => x.Customer).NotEmpty();
RuleFor(x => x.CustomerId).NotEmpty();
RuleFor(x => x.InvoiceNo).NotEmpty();
RuleFor(x => x.InvoiceImage).NotEmpty();
RuleFor(x => x.InvoiceImageExt).NotEmpty();
RuleFor(x => x.Product).NotEmpty();
RuleFor(x => x.ProductId).NotEmpty();
RuleFor(x => x.AmountOfSales).NotEmpty();
RuleFor(x => x.User).NotEmpty();
RuleFor(x => x.UserId).NotEmpty();
RuleFor(x => x.InvoiceDate).NotEmpty();
RuleFor(x => x.AddDate).NotEmpty();
RuleFor(x => x.IsApproved).NotEmpty();
RuleFor(x => x.ApprovedDate).NotEmpty();
RuleFor(x => x.NotApproved).NotEmpty();
RuleFor(x => x.NotApprovedDate).NotEmpty();
RuleFor(x => x.Reason).NotEmpty();
RuleFor(x => x.InvoiceTotal).NotEmpty();


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