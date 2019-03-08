
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
    public class LanguageWordManager : ManagerBase, ILanguageWordService
    {
        private ILanguageWordDal _languageWordDal;

        public LanguageWordManager(ILanguageWordDal languageWordDal)
        {
            _languageWordDal = languageWordDal;
        }
        
         // [LogAspect(typeof(DatabaseLogger))]
        [CacheAspect(typeof(MemoryCacheManager))]
        // [PerformanceCounterAspect(1)]      
        public List<LanguageWord> GetAll()
        {
            return _languageWordDal.GetList();
        }

        public LanguageWord GetById(int languageWordId)
        {
            return _languageWordDal.Get(u => u.LanguageWordId == languageWordId);
        }      

        //[FluentValidationAspect(typeof(LanguageWordValidator))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public LanguageWord Add(LanguageWord languageWord)
        {
            return _languageWordDal.Add(languageWord);
        }
        //[FluentValidationAspect(typeof(LanguageWordValidator))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Update(LanguageWord languageWord)
        {
              _languageWordDal.Update(languageWord);
        }

        public void Delete(LanguageWord languageWord)
        {
            _languageWordDal.Delete(languageWord);
        }    

        public List<LanguageWord> GetByLanguageWord(int languageWordId)
        {
            return _languageWordDal.GetList(filter: t => t.LanguageWordId == languageWordId).ToList();
        }
    }
}
