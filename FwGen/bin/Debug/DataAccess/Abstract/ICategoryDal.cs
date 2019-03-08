
using NewGenFramework.Core.DataAccess;
using NewGenFramework.Entities.Concrete;
namespace NewGenFramework.DataAccess.Abstract
{
    //Neden IEntityRepository direk kullanmıyoruz da IProductDal diye bir şeyi araya atıyoruz sorusuna 
    //Nesneye özgü metodlar geliştirilebilir.
    public interface ICategoryDal:IEntityRepository<Category>
    {
        //for Ex:
        //List<CategoryDetail> GetCategoryDetails();
    }
}