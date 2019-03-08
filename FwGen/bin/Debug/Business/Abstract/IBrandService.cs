
using System.Collections.Generic;
using NewGenFramework.Entities.ComplexTypes;
using NewGenFramework.Entities.Concrete;

namespace NewGenFramework.Business.Abstract
{
		public interface IBrandService
		{
				List<Brand> GetAll();
				Brand GetById(int brandId);
				List<Brand> GetByBrand(int brandId);
				
				Brand Add(Brand brand);
				void Update(Brand brand);
				void Delete(Brand brand);

		}
}