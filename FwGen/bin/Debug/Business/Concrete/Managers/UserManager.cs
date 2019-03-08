
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
    public class UserManager : ManagerBase, IUserService
    {
        private IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }
        
         // [LogAspect(typeof(DatabaseLogger))]
        [CacheAspect(typeof(MemoryCacheManager))]
        // [PerformanceCounterAspect(1)]      
        public List<User> GetAll()
        {
            return _userDal.GetList();
        }

        public User GetById(int userId)
        {
            return _userDal.Get(u => u.UserId == userId);
        }      

        //[FluentValidationAspect(typeof(UserValidator))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public User Add(User user)
        {
            return _userDal.Add(user);
        }
        //[FluentValidationAspect(typeof(UserValidator))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Update(User user)
        {
              _userDal.Update(user);
        }

        public void Delete(User user)
        {
            _userDal.Delete(user);
        }    

        public List<User> GetByUser(int userId)
        {
            return _userDal.GetList(filter: t => t.UserId == userId).ToList();
        }
    }
}
