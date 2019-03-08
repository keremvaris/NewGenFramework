
using System.Collections.Generic;
using NewGenFramework.Entities.ComplexTypes;
using NewGenFramework.Entities.Concrete;

namespace NewGenFramework.Business.Abstract
{
		public interface ICityService
		{
				List<City> GetAll();
				City GetById(int cityId);
				List<City> GetByCity(int cityId);
				
				City Add(City city);
				void Update(City city);
				void Delete(City city);

		}
}