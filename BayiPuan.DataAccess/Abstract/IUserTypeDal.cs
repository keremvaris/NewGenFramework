using BayiPuan.Entities.Concrete;
using NewGenFramework.Core.DataAccess;

namespace BayiPuan.DataAccess.Abstract
{
    //Neden IEntityRepository direk kullanmıyoruz da IProductDal diye bir şeyi araya atıyoruz sorusuna 
    //Nesneye özgü metodlar geliştirilebilir.
    public interface IUserTypeDal:IEntityRepository<UserType>
    {
        //for Ex:
        //List<UserTypeDetail> GetUserTypeDetails();
    }
}