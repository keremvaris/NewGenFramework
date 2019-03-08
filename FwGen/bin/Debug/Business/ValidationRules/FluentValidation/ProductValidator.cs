using FluentValidation;
using NewGenFramework.Entities.Concrete;
using NewGenFramework.Business.DependencyResolvers.Ninject;
using NewGenFramework.Entities.Concrete;
using NewGenFramework.Business.Abstract;

namespace NewGenFramework.Business.ValidationRules.FluentValidation
{
   public class ProductValidator:AbstractValidator<Product>
    {
        public ProductValidator()
        {
        //Eğer CustomRule yazmak istenirse service interfacelerini çözer custom rule için gerekli metodlara ulaşmanızı sağlar
        var productService = DependencyResolver<IProductService>.Resolve();
        //Sadece Boş Olamaz Kontrolü Yapar
            RuleFor(x => x.ProductId).NotEmpty();
RuleFor(x => x.ProductCode).NotEmpty();
RuleFor(x => x.ProductShortCode).NotEmpty();
RuleFor(x => x.ProductName).NotEmpty();
RuleFor(x => x.UnitType).NotEmpty();
RuleFor(x => x.UnitTypeId).NotEmpty();
RuleFor(x => x.StockAmount).NotEmpty();
RuleFor(x => x.RemainingStockAmount).NotEmpty();
RuleFor(x => x.CriticalStockAmount).NotEmpty();
RuleFor(x => x.Point).NotEmpty();
RuleFor(x => x.PointToMoney).NotEmpty();


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