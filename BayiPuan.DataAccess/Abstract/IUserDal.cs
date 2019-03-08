

using System.Collections.Generic;
using BayiPuan.Entities.ComplexTypes;
using BayiPuan.Entities.Concrete;
using NewGenFramework.Core.DataAccess;


namespace BayiPuan.DataAccess.Abstract
{
    //Neden IEntityRepository direk kullanmıyoruz da IProductDal diye bir şeyi araya atıyoruz sorusuna 
    //Nesneye özgü metodlar geliştirilebilir.
    public interface IUserDal:IEntityRepository<User>
    {
    //for Ex:
    //List<UserDetail> GetUserDetails();
    List<UserRoleItem> GetUserRoles(User user);
  }
}