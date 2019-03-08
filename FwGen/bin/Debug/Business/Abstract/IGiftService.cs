
using System.Collections.Generic;
using NewGenFramework.Entities.ComplexTypes;
using NewGenFramework.Entities.Concrete;

namespace NewGenFramework.Business.Abstract
{
		public interface IGiftService
		{
				List<Gift> GetAll();
				Gift GetById(int giftId);
				List<Gift> GetByGift(int giftId);
				
				Gift Add(Gift gift);
				void Update(Gift gift);
				void Delete(Gift gift);

		}
}