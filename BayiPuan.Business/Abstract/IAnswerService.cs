
using System.Collections.Generic;
using BayiPuan.Entities.Concrete;

namespace BayiPuan.Business.Abstract
{
    public interface IAnswerService
    {
        List<Answer> GetAll();
        Answer GetById(int answerId);
        List<Answer> GetByAnswer(int answerId);
        
        Answer Add(Answer answer);
        void Update(Answer answer);
        void Delete(Answer answer);

    }
}