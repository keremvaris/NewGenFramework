

using BayiPuan.Entities.Concrete;
using NewGenFramework.Core.DataAccess;

namespace BayiPuan.DataAccess.Abstract
{
    //Neden IEntityRepository direk kullanmıyoruz da IProductDal diye bir şeyi araya atıyoruz sorusuna 
    //Nesneye özgü metodlar geliştirilebilir.
    public interface IRoleDal:IEntityRepository<Role>
    {
        //for Ex:
        //List<RoleDetail> GetRoleDetails();
    }
}