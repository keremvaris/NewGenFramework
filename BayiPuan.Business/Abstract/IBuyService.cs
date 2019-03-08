
using System.Collections.Generic;
using BayiPuan.Entities.Concrete;

namespace BayiPuan.Business.Abstract
{
    public interface IBuyService
    {
        List<Buy> GetAll();
        Buy GetById(int buyId);
        List<Buy> GetByBuy(int buyId);
        
        Buy Add(Buy buy);
        void Update(Buy buy);
        void Delete(Buy buy);

    }
}