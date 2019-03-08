
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
    public class CustomerManager : ManagerBase, ICustomerService
    {
        private ICustomerDal _customerDal;

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
