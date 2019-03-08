
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using NewGenFramework.Business.Abstract;
using NewGenFramework.Core.Aspects.Postsharp.AuthorizationAspects;
using NewGenFramework.Core.Aspects.Postsharp.CacheAspects;
using NewGenFramework.Core.Aspects.Postsharp.TransactionAspects;
using NewGenFramework.Core.CrossCuttingConcerns.Caching.Microsoft;
using NewGenFramework.DataAccess.Abstract;
using NewGenFramework.Entities.Concrete;
using NewGenFramework.Core.CrossCuttingConcerns.Transaction;

namespace NewGenFramework.Business.Concrete.Managers
{
    public class CategoryManager : ManagerBase, ICategoryService
    {
        private ICategoryDal _categoryDal;

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
