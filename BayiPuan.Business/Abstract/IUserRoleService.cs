
using System.Collections.Generic;
using BayiPuan.Entities.Concrete;

namespace BayiPuan.Business.Abstract
{
    public interface IUserRoleService
    {
        List<UserRole> GetAll();
        UserRole GetById(int userRoleId);
        List<UserRole> GetByUserRole(int userRoleId);
        
        UserRole Add(UserRole userRole);
        void Update(UserRole userRole);
        void Delete(UserRole userRole);

    }
}