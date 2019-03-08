
using System.Collections.Generic;
using NewGenFramework.Entities.ComplexTypes;
using NewGenFramework.Entities.Concrete;

namespace NewGenFramework.Business.Abstract
{
		public interface ICategoryService
		{
				List<Category> GetAll();
				Category GetById(int categoryId);
				List<Category> GetByCategory(int categoryId);
				
				Category Add(Category category);
				void Update(Category category);
				void Delete(Category category);

		}
}