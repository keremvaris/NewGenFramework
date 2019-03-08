
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
    public class BuyManager : ManagerBase, IBuyService
    {
        private readonly IBuyDal _buyDal;

        public BuyManager(IBuyDal buyDal)
        {
            _buyDal = buyDal;
        }
        
         // [LogAspect(typeof(DatabaseLogger))]
        [CacheAspect(typeof(MemoryCacheManager))]
        // [PerformanceCounterAspect(1)]      
        public List<Buy> GetAll()
        {
            return _buyDal.GetList();
        }

        public Buy GetById(int buyId)
        {
            return _buyDal.Get(u => u.BuyId == buyId);
        }      

        //[FluentValidationAspect(typeof(BuyValidator))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public Buy Add(Buy buy)
        {
            return _buyDal.Add(buy);
        }
        //[FluentValidationAspect(typeof(BuyValidator))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Update(Buy buy)
        {
              _buyDal.Update(buy);
        }

        public void Delete(Buy buy)
        {
            _buyDal.Delete(buy);
        }    

        public List<Buy> GetByBuy(int buyId)
        {
            return _buyDal.GetList(filter: t => t.BuyId == buyId).ToList();
        }
    }
}
