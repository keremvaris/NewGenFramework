
using System.Collections.Generic;
using System.Linq;
using BayiPuan.Business.Abstract;
using NewGenFramework.Core.Aspects.Postsharp.CacheAspects;
using NewGenFramework.Core.CrossCuttingConcerns.Caching.Microsoft;
using BayiPuan.DataAccess.Abstract;
using BayiPuan.Entities.Concrete;
using NewGenFramework.Core.CrossCuttingConcerns.Transaction;

namespace BayiPuan.Business.Concrete.Managers
{
    public class CategoryManager : ManagerBase, ICategoryService
    {
        private readonly ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }
        
         // [LogAspect(typeof(DatabaseLogger))]
        [CacheAspect(typeof(MemoryCacheManager))]
        // [PerformanceCounterAspect(1)]      
        public List<Category> GetAll()
        {
            return _categoryDal.GetList();
        }

        public Category GetById(int categoryId)
        {
            return _categoryDal.Get(u => u.CategoryId == categoryId);
        }      

        //[FluentValidationAspect(typeof(CategoryValidator))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public Category Add(Category category)
        {
            return _categoryDal.Add(category);
        }
        //[FluentValidationAspect(typeof(CategoryValidator))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Update(Category category)
        {
              _categoryDal.Update(category);
        }

        public void Delete(Category category)
        {
            _categoryDal.Delete(category);
        }    

        public List<Category> GetByCategory(int categoryId)
        {
            return _categoryDal.GetList(filter: t => t.CategoryId == categoryId).ToList();
        }
    }
}
