
using System.Collections.Generic;
using NewGenFramework.Entities.ComplexTypes;
using NewGenFramework.Entities.Concrete;

namespace NewGenFramework.Business.Abstract
{
		public interface IUserService
		{
				List<User> GetAll();
				User GetById(int userId);
				List<User> GetByUser(int userId);
				
				User Add(User user);
				void Update(User user);
				void Delete(User user);

		}
}