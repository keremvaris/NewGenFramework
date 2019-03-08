
using System.Collections.Generic;
using NewGenFramework.Entities.ComplexTypes;
using NewGenFramework.Entities.Concrete;

namespace NewGenFramework.Business.Abstract
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