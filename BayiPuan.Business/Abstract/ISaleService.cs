
using System.Collections.Generic;
using BayiPuan.Entities.Concrete;

namespace BayiPuan.Business.Abstract
{
    public interface ISaleService
    {
        List<Sale> GetAll();
        Sale GetById(int saleId);
        List<Sale> GetBySale(int saleId);
        
        Sale Add(Sale sale);
        void Update(Sale sale);
        void Delete(Sale sale);

    }
}