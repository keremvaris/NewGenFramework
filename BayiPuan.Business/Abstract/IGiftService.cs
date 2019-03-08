
using System.Collections.Generic;
using BayiPuan.Entities.Concrete;

namespace BayiPuan.Business.Abstract
{
    public interface IGiftService
    {
        List<Gift> GetAll(string includeColumns = null);
        Gift GetById(int giftId);
        List<Gift> GetByGift(int giftId);
        
        Gift Add(Gift gift);
        void Update(Gift gift);
        void Delete(Gift gift);

    }
}