
using System.Collections.Generic;
using System.Linq;
using BayiPuan.Business.Abstract;
using NewGenFramework.Core.Aspects.Postsharp.CacheAspects;
using NewGenFramework.Core.CrossCuttingConcerns.Caching.Microsoft;
using BayiPuan.DataAccess.Abstract;
using BayiPuan.Entities.Concrete;
using NewGenFramework.Core.CrossCuttingConcerns.Transaction;

namespace BayiPuan.Business.Concrete.Managers
{
    public class CustomerManager : ManagerBase, ICustomerService
    {
        private readonly ICustomerDal _customerDal;

        public CustomerManager(ICustomerDal customerDal)
        {
            _customerDal = customerDal;
        }
        
         // [LogAspect(typeof(DatabaseLogger))]
        [CacheAspect(typeof(MemoryCacheManager))]
        // [PerformanceCounterAspect(1)]      
        public List<Customer> GetAll()
        {
            return _customerDal.GetList();
        }

        public Customer GetById(int customerId)
        {
            return _customerDal.Get(u => u.CustomerId == customerId);
        }      

        //[FluentValidationAspect(typeof(CustomerValidator))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public Customer Add(Customer customer)
        {
            return _customerDal.Add(customer);
        }
        //[FluentValidationAspect(typeof(CustomerValidator))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Update(Customer customer)
        {
              _customerDal.Update(customer);
        }

        public void Delete(Customer customer)
        {
            _customerDal.Delete(customer);
        }    

        public List<Customer> GetByCustomer(int customerId)
        {
            return _customerDal.GetList(filter: t => t.CustomerId == customerId).ToList();
        }
    }
}
