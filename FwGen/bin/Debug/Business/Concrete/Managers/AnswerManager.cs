
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
    public class AnswerManager : ManagerBase, IAnswerService
    {
        private IAnswerDal _answerDal;

        public AnswerManager(IAnswerDal answerDal)
        {
            _answerDal = answerDal;
        }
        
         // [LogAspect(typeof(DatabaseLogger))]
        [CacheAspect(typeof(MemoryCacheManager))]
        // [PerformanceCounterAspect(1)]      
        public List<Answer> GetAll()
        {
            return _answerDal.GetList();
        }

        public Answer GetById(int answerId)
        {
            return _answerDal.Get(u => u.AnswerId == answerId);
        }      

        //[FluentValidationAspect(typeof(AnswerValidator))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public Answer Add(Answer answer)
        {
            return _answerDal.Add(answer);
        }
        //[FluentValidationAspect(typeof(AnswerValidator))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Update(Answer answer)
        {
              _answerDal.Update(answer);
        }

        public void Delete(Answer answer)
        {
            _answerDal.Delete(answer);
        }    

        public List<Answer> GetByAnswer(int answerId)
        {
            return _answerDal.GetList(filter: t => t.AnswerId == answerId).ToList();
        }
    }
}
