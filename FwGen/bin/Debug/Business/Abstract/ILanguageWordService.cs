
using System.Collections.Generic;
using NewGenFramework.Entities.ComplexTypes;
using NewGenFramework.Entities.Concrete;

namespace NewGenFramework.Business.Abstract
{
		public interface ILanguageWordService
		{
				List<LanguageWord> GetAll();
				LanguageWord GetById(int languageWordId);
				List<LanguageWord> GetByLanguageWord(int languageWordId);
				
				LanguageWord Add(LanguageWord languageWord);
				void Update(LanguageWord languageWord);
				void Delete(LanguageWord languageWord);

		}
}