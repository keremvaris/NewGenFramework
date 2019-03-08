


using BayiPuan.Entities.Concrete;
using NewGenFramework.Core.DataAccess;

namespace BayiPuan.DataAccess.Abstract
{
    //Neden IEntityRepository direk kullanmıyoruz da IProductDal diye bir şeyi araya atıyoruz sorusuna 
    //Nesneye özgü metodlar geliştirilebilir.
    public interface ILanguageWordDal:IEntityRepository<LanguageWord>
    {
        //for Ex:
        //List<LanguageWordDetail> GetLanguageWordDetails();
    }
}