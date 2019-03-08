
using System.Collections.Generic;
using System.Linq;
using BayiPuan.Business.Abstract;
using NewGenFramework.Core.Aspects.Postsharp.CacheAspects;
using NewGenFramework.Core.CrossCuttingConcerns.Caching.Microsoft;
using BayiPuan.DataAccess.Abstract;
using BayiPuan.Entities.Concrete;
using NewGenFramework.Core.CrossCuttingConcerns.Transaction;

namespace BayiPuan.Business.Concrete.Managers
{
    public class UserRoleManager : ManagerBase, IUserRoleService
    {
        private readonly IUserRoleDal _userRoleDal;

        public UserRoleManager(IUserRoleDal userRoleDal)
        {
            _userRoleDal = userRoleDal;
        }
        
         // [LogAspect(typeof(DatabaseLogger))]
        [CacheAspect(typeof(MemoryCacheManager))]
        // [PerformanceCounterAspect(1)]      
        public List<UserRole> GetAll()
        {
            return _userRoleDal.GetList();
        }

        public UserRole GetById(int userRoleId)
        {
            return _userRoleDal.Get(u => u.UserRoleId == userRoleId);
        }      

        //[FluentValidationAspect(typeof(UserRoleValidator))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public UserRole Add(UserRole userRole)
        {
            return _userRoleDal.Add(userRole);
        }
        //[FluentValidationAspect(typeof(UserRoleValidator))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Update(UserRole userRole)
        {
              _userRoleDal.Update(userRole);
        }

        public void Delete(UserRole userRole)
        {
            _userRoleDal.Delete(userRole);
        }    

        public List<UserRole> GetByUserRole(int userRoleId)
        {
            return _userRoleDal.GetList(filter: t => t.UserRoleId == userRoleId).ToList();
        }
    }
}
