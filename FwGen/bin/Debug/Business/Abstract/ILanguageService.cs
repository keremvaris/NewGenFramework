
using System.Collections.Generic;
using NewGenFramework.Entities.ComplexTypes;
using NewGenFramework.Entities.Concrete;

namespace NewGenFramework.Business.Abstract
{
		public interface ILanguageService
		{
				List<Language> GetAll();
				Language GetById(int languageId);
				List<Language> GetByLanguage(int languageId);
				
				Language Add(Language language);
				void Update(Language language);
				void Delete(Language language);

		}
}