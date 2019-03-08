
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using NewGenFramework.Business.Abstract;
using NewGenFramework.Core.Aspects.Postsharp.AuthorizationAspects;
using NewGenFramework.Core.Aspects.Postsharp.CacheAspects;
using NewGenFramework.Core.Aspects.Postsharp.TransactionAspects;
using NewGenFramework.Core.CrossCuttingConcerns.Caching.Microsoft;
using NewGenFramework.DataAccess.Abstract;
using NewGenFramework.Entities.Concrete;
using NewGenFramework.Core.CrossCuttingConcerns.Transaction;

namespace NewGenFramework.Business.Concrete.Managers
{
    public class UserTypeManager : ManagerBase, IUserTypeService
    {
        private IUserTypeDal _userTypeDal;

        public UserTypeManager(IUserTypeDal userTypeDal)
        {
            _userTypeDal = userTypeDal;
        }
        
         // [LogAspect(typeof(DatabaseLogger))]
        [CacheAspect(typeof(MemoryCacheManager))]
        // [PerformanceCounterAspect(1)]      
        public List<UserType> GetAll()
        {
            return _userTypeDal.GetList();
        }

        public UserType GetById(int userTypeId)
        {
            return _userTypeDal.Get(u => u.UserTypeId == userTypeId);
        }      

        //[FluentValidationAspect(typeof(UserTypeValidator))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public UserType Add(UserType userType)
        {
            return _userTypeDal.Add(userType);
        }
        //[FluentValidationAspect(typeof(UserTypeValidator))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Update(UserType userType)
        {
              _userTypeDal.Update(userType);
        }

        public void Delete(UserType userType)
        {
            _userTypeDal.Delete(userType);
        }    

        public List<UserType> GetByUserType(int userTypeId)
        {
            return _userTypeDal.GetList(filter: t => t.UserTypeId == userTypeId).ToList();
        }
    }
}
