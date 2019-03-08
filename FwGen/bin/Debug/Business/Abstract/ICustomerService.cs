
using System.Collections.Generic;
using NewGenFramework.Entities.ComplexTypes;
using NewGenFramework.Entities.Concrete;

namespace NewGenFramework.Business.Abstract
{
		public interface ICustomerService
		{
				List<Customer> GetAll();
				Customer GetById(int customerId);
				List<Customer> GetByCustomer(int customerId);
				
				Customer Add(Customer customer);
				void Update(Customer customer);
				void Delete(Customer customer);

		}
}