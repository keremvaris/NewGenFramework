
using System.Collections.Generic;
using BayiPuan.Entities.Concrete;

namespace BayiPuan.Business.Abstract
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