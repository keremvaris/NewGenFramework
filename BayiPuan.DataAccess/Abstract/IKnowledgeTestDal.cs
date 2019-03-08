
using NewGenFramework.Core.DataAccess;
using BayiPuan.Entities.Concrete;
namespace BayiPuan.DataAccess.Abstract
{
    //Neden IEntityRepository direk kullanmıyoruz da IProductDal diye bir şeyi araya atıyoruz sorusuna 
    //Nesneye özgü metodlar geliştirilebilir.
    public interface IKnowledgeTestDal:IEntityRepository<KnowledgeTest>
    {
        //for Ex:
        //List<KnowledgeTestDetail> GetKnowledgeTestDetails();
    }
}