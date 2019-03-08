
using System.Collections.Generic;
using BayiPuan.Entities.Concrete;

namespace BayiPuan.Business.Abstract
{
    public interface IUserTypeService
    {
        List<UserType> GetAll();
        UserType GetById(int userTypeId);
        List<UserType> GetByUserType(int userTypeId);
        
        UserType Add(UserType userType);
        void Update(UserType userType);
        void Delete(UserType userType);

    }
}