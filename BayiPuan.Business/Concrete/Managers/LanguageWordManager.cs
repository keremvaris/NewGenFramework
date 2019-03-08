
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BayiPuan.Business.Abstract;
using NewGenFramework.Core.Aspects.Postsharp.CacheAspects;
using NewGenFramework.Core.CrossCuttingConcerns.Caching.Microsoft;
using BayiPuan.DataAccess.Abstract;
using BayiPuan.Entities.Concrete;
using NewGenFramework.Core.CrossCuttingConcerns.Transaction;

namespace BayiPuan.Business.Concrete.Managers
{
  public class LanguageWordManager : ManagerBase, ILanguageWordService
  {


    private readonly ILanguageWordDal _languageWordDal;
    private readonly IMapper _mapper;

    public LanguageWordManager(ILanguageWordDal languageWordDal, IMapper mapper)
    {
      _languageWordDal = languageWordDal;
      _mapper = mapper;
    }

    [CacheAspect(typeof(MemoryCacheManager))]
    public List<LanguageWord> GetAll()
    {
      var languageWords = _mapper.Map<List<LanguageWord>>(_languageWordDal.GetList());
      return languageWords;
    }

    [CacheAspect(typeof(MemoryCacheManager))]
    public List<LanguageWord> GetByLanguage(int dilId)
    {
      var getByLanguageWords = _mapper.Map<List<LanguageWord>>(_languageWordDal.GetList(filter: t => t.LanguageId == dilId));
      return getByLanguageWords;
    }

    [CacheAspect(typeof(MemoryCacheManager))]
    public LanguageWord GetValue(int dilId, string kod)
    {
      return GetByLanguage(dilId).FirstOrDefault(t => t.Code == kod);
    }

    [CacheRemoveAspect(typeof(MemoryCacheManager))]
    public LanguageWord Add(LanguageWord languageWord)
    {
      return _languageWordDal.Add(languageWord);
    }
  }
}
