
using System.Collections.Generic;
using BayiPuan.Entities.Concrete;

namespace BayiPuan.Business.Abstract
{
    public interface ILanguageService
    {
    List<Language> GetAll();
      Language GetById(int languageId);
      List<Language> GetByLanguage(int languageId);

      Language Add(Language language);
      void Update(Language language);
      void Delete(Language language);

      Language Get(string code);

  }
}