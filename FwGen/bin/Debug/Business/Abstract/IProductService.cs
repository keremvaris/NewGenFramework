
using System.Collections.Generic;
using NewGenFramework.Entities.ComplexTypes;
using NewGenFramework.Entities.Concrete;

namespace NewGenFramework.Business.Abstract
{
		public interface IProductService
		{
				List<Product> GetAll();
				Product GetById(int productId);
				List<Product> GetByProduct(int productId);
				
				Product Add(Product product);
				void Update(Product product);
				void Delete(Product product);

		}
}