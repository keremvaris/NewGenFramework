
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
    public class ProductManager : ManagerBase, IProductService
    {
        private IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }
        
         // [LogAspect(typeof(DatabaseLogger))]
        [CacheAspect(typeof(MemoryCacheManager))]
        // [PerformanceCounterAspect(1)]      
        public List<Product> GetAll()
        {
            return _productDal.GetList();
        }

        public Product GetById(int productId)
        {
            return _productDal.Get(u => u.ProductId == productId);
        }      

        //[FluentValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public Product Add(Product product)
        {
            return _productDal.Add(product);
        }
        //[FluentValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Update(Product product)
        {
              _productDal.Update(product);
        }

        public void Delete(Product product)
        {
            _productDal.Delete(product);
        }    

        public List<Product> GetByProduct(int productId)
        {
            return _productDal.GetList(filter: t => t.ProductId == productId).ToList();
        }
    }
}
