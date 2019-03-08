using System.Collections.Generic;
using System.Linq;
using BayiPuan.DataAccess.Abstract;
using BayiPuan.DataAccess.Concrete.Context;
using BayiPuan.Entities.ComplexTypes;
using BayiPuan.Entities.Concrete;
using NewGenFramework.Core.DataAccess.EntityFramework;

namespace BayiPuan.DataAccess.Concrete.EntityFramework
{
     public class EfUserDal:EfEntityRepositoryBase<User,BayiPuanContext>,IUserDal
    {
      public List<UserRoleItem> GetUserRoles(User user)
      {
        using (BayiPuanContext context = new BayiPuanContext())
        {
          var result = from ur in context.UserRoles
            join r in context.Roles
              on ur.RoleId equals r.RoleId
            where ur.UserId == user.UserId
            select new UserRoleItem
            {
              RoleName = r.RoleName
            };
          return result.ToList();
        }
      }
  }
}
