
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
    public class MyNewManager : ManagerBase, IMyNewService
    {
        private readonly IMyNewDal _myNewDal;

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
            return _myNewDal.Get(u => u.NewsId == myNewId);
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
            return _myNewDal.GetList(filter: t => t.NewsId == myNewId).ToList();
        }
    }
}
