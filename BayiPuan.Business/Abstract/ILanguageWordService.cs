
using System.Collections.Generic;
using BayiPuan.Entities.Concrete;

namespace BayiPuan.Business.Abstract
{
    public interface ILanguageWordService
    {
    List<LanguageWord> GetAll();
      List<LanguageWord> GetByLanguage(int languageId);
      LanguageWord GetValue(int languageId, string code);
      LanguageWord Add(LanguageWord languageWord);

  }
}