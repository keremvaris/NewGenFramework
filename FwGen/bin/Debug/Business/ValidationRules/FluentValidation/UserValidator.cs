using FluentValidation;
using NewGenFramework.Entities.Concrete;
using NewGenFramework.Business.DependencyResolvers.Ninject;
using NewGenFramework.Entities.Concrete;
using NewGenFramework.Business.Abstract;

namespace NewGenFramework.Business.ValidationRules.FluentValidation
{
   public class UserValidator:AbstractValidator<User>
    {
        public UserValidator()
        {
        //Eğer CustomRule yazmak istenirse service interfacelerini çözer custom rule için gerekli metodlara ulaşmanızı sağlar
        var userService = DependencyResolver<IUserService>.Resolve();
        //Sadece Boş Olamaz Kontrolü Yapar
            RuleFor(x => x.UserId).NotEmpty();
RuleFor(x => x.UserType).NotEmpty();
RuleFor(x => x.UserTypeId).NotEmpty();
RuleFor(x => x.UserName).NotEmpty();
RuleFor(x => x.Password).NotEmpty();
RuleFor(x => x.FirstName).NotEmpty();
RuleFor(x => x.LastName).NotEmpty();
RuleFor(x => x.Email).NotEmpty();
RuleFor(x => x.MobilePhone).NotEmpty();
RuleFor(x => x.UserImage).NotEmpty();
RuleFor(x => x.BirthDate).NotEmpty();
RuleFor(x => x.State).NotEmpty();
RuleFor(x => x.Agreement).NotEmpty();
RuleFor(x => x.Seller).NotEmpty();
RuleFor(x => x.SellerId).NotEmpty();
RuleFor(x => x.Contact).NotEmpty();


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