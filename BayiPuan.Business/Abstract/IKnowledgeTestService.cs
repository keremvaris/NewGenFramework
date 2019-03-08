
using System.Collections.Generic;
using BayiPuan.Entities.Concrete;

namespace BayiPuan.Business.Abstract
{
    public interface IKnowledgeTestService
    {
        List<KnowledgeTest> GetAll();
        KnowledgeTest GetById(int knowledgeTestId);
        List<KnowledgeTest> GetByKnowledgeTest(int knowledgeTestId);
        
        KnowledgeTest Add(KnowledgeTest knowledgeTest);
        void Update(KnowledgeTest knowledgeTest);
        void Delete(KnowledgeTest knowledgeTest);

    }
}