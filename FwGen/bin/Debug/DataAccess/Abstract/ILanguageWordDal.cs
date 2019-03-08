
using NewGenFramework.Core.DataAccess;
using NewGenFramework.Entities.Concrete;
namespace NewGenFramework.DataAccess.Abstract
{
    //Neden IEntityRepository direk kullanmıyoruz da IProductDal diye bir şeyi araya atıyoruz sorusuna 
    //Nesneye özgü metodlar geliştirilebilir.
    public interface ILanguageWordDal:IEntityRepository<LanguageWord>
    {
        //for Ex:
        //List<LanguageWordDetail> GetLanguageWordDetails();
    }
}