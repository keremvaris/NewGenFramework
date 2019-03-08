
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
    public class RoleManager : ManagerBase, IRoleService
    {
        private readonly IRoleDal _roleDal;

        public RoleManager(IRoleDal roleDal)
        {
            _roleDal = roleDal;
        }
        
         // [LogAspect(typeof(DatabaseLogger))]
        [CacheAspect(typeof(MemoryCacheManager))]
        // [PerformanceCounterAspect(1)]      
        public List<Role> GetAll()
        {
            return _roleDal.GetList();
        }

        public Role GetById(int roleId)
        {
            return _roleDal.Get(u => u.RoleId == roleId);
        }      

        //[FluentValidationAspect(typeof(RoleValidator))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public Role Add(Role role)
        {
            return _roleDal.Add(role);
        }
        //[FluentValidationAspect(typeof(RoleValidator))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Update(Role role)
        {
              _roleDal.Update(role);
        }

        public void Delete(Role role)
        {
            _roleDal.Delete(role);
        }    

        public List<Role> GetByRole(int roleId)
        {
            return _roleDal.GetList(filter: t => t.RoleId == roleId).ToList();
        }
    }
}
