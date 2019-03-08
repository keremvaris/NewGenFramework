
using System.Collections.Generic;
using NewGenFramework.Entities.ComplexTypes;
using NewGenFramework.Entities.Concrete;

namespace NewGenFramework.Business.Abstract
{
		public interface IRoleService
		{
				List<Role> GetAll();
				Role GetById(int roleId);
				List<Role> GetByRole(int roleId);
				
				Role Add(Role role);
				void Update(Role role);
				void Delete(Role role);

		}
}