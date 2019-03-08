
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
    public class SaleManager : ManagerBase, ISaleService
    {
        private ISaleDal _saleDal;

        public SaleManager(ISaleDal saleDal)
        {
            _saleDal = saleDal;
        }
        
         // [LogAspect(typeof(DatabaseLogger))]
        [CacheAspect(typeof(MemoryCacheManager))]
        // [PerformanceCounterAspect(1)]      
        public List<Sale> GetAll()
        {
            return _saleDal.GetList();
        }

        public Sale GetById(int saleId)
        {
            return _saleDal.Get(u => u.SaleId == saleId);
        }      

        //[FluentValidationAspect(typeof(SaleValidator))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public Sale Add(Sale sale)
        {
            return _saleDal.Add(sale);
        }
        //[FluentValidationAspect(typeof(SaleValidator))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Update(Sale sale)
        {
              _saleDal.Update(sale);
        }

        public void Delete(Sale sale)
        {
            _saleDal.Delete(sale);
        }    

        public List<Sale> GetBySale(int saleId)
        {
            return _saleDal.GetList(filter: t => t.SaleId == saleId).ToList();
        }
    }
}
