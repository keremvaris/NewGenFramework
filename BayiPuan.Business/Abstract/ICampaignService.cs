
using System.Collections.Generic;
using BayiPuan.Entities.Concrete;

namespace BayiPuan.Business.Abstract
{
    public interface ICampaignService
    {
        List<Campaign> GetAll();
        Campaign GetById(int campaignId);
        List<Campaign> GetByCampaign(int campaignId);
        
        Campaign Add(Campaign campaign);
        void Update(Campaign campaign);
        void Delete(Campaign campaign);

    }
}