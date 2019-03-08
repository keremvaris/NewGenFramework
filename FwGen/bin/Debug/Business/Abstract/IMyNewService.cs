
using System.Collections.Generic;
using NewGenFramework.Entities.ComplexTypes;
using NewGenFramework.Entities.Concrete;

namespace NewGenFramework.Business.Abstract
{
		public interface IMyNewService
		{
				List<MyNew> GetAll();
				MyNew GetById(int myNewId);
				List<MyNew> GetByMyNew(int myNewId);
				
				MyNew Add(MyNew myNew);
				void Update(MyNew myNew);
				void Delete(MyNew myNew);

		}
}