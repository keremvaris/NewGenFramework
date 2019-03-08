
using System.Collections.Generic;
using System.Linq;
using System.Web.Helpers;
using BayiPuan.Business.Abstract;
using BayiPuan.Business.ValidationRules.FluentValidation;
using BayiPuan.DataAccess.Abstract;
using BayiPuan.Entities.ComplexTypes;
using BayiPuan.Entities.Concrete;
using NewGenFramework.Core.Aspects.Postsharp.ValidationAspects;
using NewGenFramework.Core.CrossCuttingConcerns.Transaction;

namespace BayiPuan.Business.Concrete.Managers
{
  public class UserManager : ManagerBase, IUserService
  {
    private readonly IUserDal _userDal;

    public UserManager(IUserDal userDal)
    {
      _userDal = userDal;
    }

    [FluentValidationAspect(typeof(UserValidator))]
    public User GetByUserNameAndPassword(string userName, string password)
    {
      string encoded = Crypto.SHA256(password);
      return _userDal.Get(u => u.UserName == userName && u.Password == encoded && u.State == true);
    }

    public List<User> GetAll()
    {
      return _userDal.GetList();
    }

    public User GetById(int userId)
    {
      return _userDal.Get(u => u.UserId == userId);
    }

    public List<User> GetByUser(int userId)
    {
      return _userDal.GetList(filter: t => t.UserId == userId).ToList();
    }

    public List<UserRoleItem> GetUserRoles(User user)
    {
      return _userDal.GetUserRoles(user);
    }

    //[FluentValidationAspect(typeof(UserValidator))]
    public User Add(User user)
    {
      return _userDal.Add(user);
    }

    public void Update(User user)
    {
      _userDal.Update(user);
    }

    public void Delete(User user)
    {
      _userDal.Delete(user);
    }


    public User UniqueUserName(string userName)
    {
      return _userDal.GetList().FirstOrDefault(u => u.UserName == userName);
    }

    public User UniqueEmail(string email)
    {
      return _userDal.GetList().FirstOrDefault(u => u.Email == email);
    }
  }
}
