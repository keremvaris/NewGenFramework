
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
    public class GiftManager : ManagerBase, IGiftService
    {
        private IGiftDal _giftDal;

        public GiftManager(IGiftDal giftDal)
        {
            _giftDal = giftDal;
        }
        
         // [LogAspect(typeof(DatabaseLogger))]
        [CacheAspect(typeof(MemoryCacheManager))]
        // [PerformanceCounterAspect(1)]      
        public List<Gift> GetAll()
        {
            return _giftDal.GetList();
        }

        public Gift GetById(int giftId)
        {
            return _giftDal.Get(u => u.GiftId == giftId);
        }      

        //[FluentValidationAspect(typeof(GiftValidator))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public Gift Add(Gift gift)
        {
            return _giftDal.Add(gift);
        }
        //[FluentValidationAspect(typeof(GiftValidator))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Update(Gift gift)
        {
              _giftDal.Update(gift);
        }

        public void Delete(Gift gift)
        {
            _giftDal.Delete(gift);
        }    

        public List<Gift> GetByGift(int giftId)
        {
            return _giftDal.GetList(filter: t => t.GiftId == giftId).ToList();
        }
    }
}
