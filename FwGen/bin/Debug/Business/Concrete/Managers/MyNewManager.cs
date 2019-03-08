
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
    public class MyNewManager : ManagerBase, IMyNewService
    {
        private IMyNewDal _myNewDal;

        public MyNewManager(IMyNewDal myNewDal)
        {
            _myNewDal = myNewDal;
        }
        
         // [LogAspect(typeof(DatabaseLogger))]
        [CacheAspect(typeof(MemoryCacheManager))]
        // [PerformanceCounterAspect(1)]      
        public List<MyNew> GetAll()
        {
            return _myNewDal.GetList();
        }

        public MyNew GetById(int myNewId)
        {
            return _myNewDal.Get(u => u.MyNewId == myNewId);
        }      

        //[FluentValidationAspect(typeof(MyNewValidator))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public MyNew Add(MyNew myNew)
        {
            return _myNewDal.Add(myNew);
        }
        //[FluentValidationAspect(typeof(MyNewValidator))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Update(MyNew myNew)
        {
              _myNewDal.Update(myNew);
        }

        public void Delete(MyNew myNew)
        {
            _myNewDal.Delete(myNew);
        }    

        public List<MyNew> GetByMyNew(int myNewId)
        {
            return _myNewDal.GetList(filter: t => t.MyNewId == myNewId).ToList();
        }
    }
}
