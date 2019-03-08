using FluentValidation;
using FluentValidation.Results;
using BayiPuan.Entities.Concrete;
using BayiPuan.Business.DependencyResolvers.Ninject;
using BayiPuan.Business.Abstract;

namespace BayiPuan.Business.ValidationRules.FluentValidation
{
  public class UserValidator : AbstractValidator<User>
  {
    public UserValidator()
    {
      //Eğer CustomRule yazmak istenirse service interfacelerini çözer custom rule için gerekli metodlara ulaşmanızı sağlar
      var userService = DependencyResolver<IUserService>.Resolve();
      //Sadece Boş Olamaz Kontrolü Yapar
      //RuleFor(x => x.UserId).NotEmpty().WithMessage("");
      RuleFor(x => x.UserTypeId).NotEmpty().WithMessage("Boş Bırakılamaz!!!");
      RuleFor(x => x.UserName).NotEmpty().WithMessage("Boş Bırakılamaz!!!");
      RuleFor(x => x.Password).NotEmpty().WithMessage("Boş Bırakılamaz!!!");
      RuleFor(x => x.FirstName).NotEmpty().WithMessage("Boş Bırakılamaz!!!");
      RuleFor(x => x.LastName).NotEmpty().WithMessage("Boş Bırakılamaz!!!");
      RuleFor(x => x.Email).NotEmpty().WithMessage("Boş Bırakılamaz!!!");
      RuleFor(x => x.MobilePhone).NotEmpty().WithMessage("Boş Bırakılamaz!!!");
      RuleFor(x => x.UserImage).NotEmpty().WithMessage("Boş Bırakılamaz!!!");
      RuleFor(x => x.BirthDate).NotEmpty().WithMessage("Boş Bırakılamaz!!!");
      RuleFor(x => x.State).NotEmpty().WithMessage("Boş Bırakılamaz!!!");
      RuleFor(x => x.SellerId).NotEmpty().WithMessage("Boş Bırakılamaz!!!");
      RuleFor(x => x.Agreement).NotEmpty().WithMessage("Kullanıcı Sözleşmesini Kabul Etmeniz Gerekir!").Equals(false);
      //Custom(rm =>
      //    {
      //      var username = userService.UniqueUserName(rm.UserName);
      //      if (username != null /*&& username.UserName != null*/)
      //      {
      //        return new ValidationFailure("UserName", "This user name is already registered");
      //      }
      //      return null;
      //    });
      //Custom(rm =>
      //{
      //  var useremail = userService.UniqueEmail(rm.Email);
      //  if (useremail != null)
      //  {
      //    return new ValidationFailure("Email", "This email is already registered");
      //  }
      //  return null;
      //});

    }
  }
}