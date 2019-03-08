
using System.Collections.Generic;
using NewGenFramework.Entities.ComplexTypes;
using NewGenFramework.Entities.Concrete;

namespace NewGenFramework.Business.Abstract
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