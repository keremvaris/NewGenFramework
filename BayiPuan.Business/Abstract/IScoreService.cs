
using System.Collections.Generic;
using BayiPuan.Entities.Concrete;

namespace BayiPuan.Business.Abstract
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