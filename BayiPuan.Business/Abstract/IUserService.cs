
using System.Collections.Generic;
using BayiPuan.Entities.ComplexTypes;
using BayiPuan.Entities.Concrete;


namespace BayiPuan.Business.Abstract
{
  public interface IUserService
  {
    User GetByUserNameAndPassword(string userName, string password);
    List<User> GetAll();
    User GetById(int userId);
    List<User> GetByUser(int userId);
    List<UserRoleItem> GetUserRoles(User user);
    User Add(User user);
    void Update(User user);
    void Delete(User user);
    User UniqueUserName(string userName);
    User UniqueEmail(string email);

  }
}