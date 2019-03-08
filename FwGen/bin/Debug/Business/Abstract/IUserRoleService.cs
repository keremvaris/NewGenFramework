
using System.Collections.Generic;
using NewGenFramework.Entities.ComplexTypes;
using NewGenFramework.Entities.Concrete;

namespace NewGenFramework.Business.Abstract
{
		public interface IUserRoleService
		{
				List<UserRole> GetAll();
				UserRole GetById(int userRoleId);
				List<UserRole> GetByUserRole(int userRoleId);
				
				UserRole Add(UserRole userRole);
				void Update(UserRole userRole);
				void Delete(UserRole userRole);

		}
}