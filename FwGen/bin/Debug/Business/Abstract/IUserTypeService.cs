
using System.Collections.Generic;
using NewGenFramework.Entities.ComplexTypes;
using NewGenFramework.Entities.Concrete;

namespace NewGenFramework.Business.Abstract
{
		public interface IUserTypeService
		{
				List<UserType> GetAll();
				UserType GetById(int userTypeId);
				List<UserType> GetByUserType(int userTypeId);
				
				UserType Add(UserType userType);
				void Update(UserType userType);
				void Delete(UserType userType);

		}
}