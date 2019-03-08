
using System.Collections.Generic;
using NewGenFramework.Entities.ComplexTypes;
using NewGenFramework.Entities.Concrete;

namespace NewGenFramework.Business.Abstract
{
		public interface IScoreService
		{
				List<Score> GetAll();
				Score GetById(int scoreId);
				List<Score> GetByScore(int scoreId);
				
				Score Add(Score score);
				void Update(Score score);
				void Delete(Score score);

		}
}