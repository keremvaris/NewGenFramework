
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
    public class CampaignManager : ManagerBase, ICampaignService
    {
        private ICampaignDal _campaignDal;

        public CampaignManager(ICampaignDal campaignDal)
        {
            _campaignDal = campaignDal;
        }
        
         // [LogAspect(typeof(DatabaseLogger))]
        [CacheAspect(typeof(MemoryCacheManager))]
        // [PerformanceCounterAspect(1)]      
        public List<Campaign> GetAll()
        {
            return _campaignDal.GetList();
        }

        public Campaign GetById(int campaignId)
        {
            return _campaignDal.Get(u => u.CampaignId == campaignId);
        }      

        //[FluentValidationAspect(typeof(CampaignValidator))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public Campaign Add(Campaign campaign)
        {
            return _campaignDal.Add(campaign);
        }
        //[FluentValidationAspect(typeof(CampaignValidator))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Update(Campaign campaign)
        {
              _campaignDal.Update(campaign);
        }

        public void Delete(Campaign campaign)
        {
            _campaignDal.Delete(campaign);
        }    

        public List<Campaign> GetByCampaign(int campaignId)
        {
            return _campaignDal.GetList(filter: t => t.CampaignId == campaignId).ToList();
        }
    }
}
