
using NewGenFramework.Core.DataAccess;
using NewGenFramework.Entities.Concrete;
namespace NewGenFramework.DataAccess.Abstract
{
    //Neden IEntityRepository direk kullanmıyoruz da IProductDal diye bir şeyi araya atıyoruz sorusuna 
    //Nesneye özgü metodlar geliştirilebilir.
    public interface IKnowledgeTestDal:IEntityRepository<KnowledgeTest>
    {
        //for Ex:
        //List<KnowledgeTestDetail> GetKnowledgeTestDetails();
    }
}