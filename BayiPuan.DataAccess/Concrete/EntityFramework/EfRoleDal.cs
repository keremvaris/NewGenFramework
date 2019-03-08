using BayiPuan.DataAccess.Abstract;
using BayiPuan.DataAccess.Concrete.Context;
using BayiPuan.Entities.Concrete;
using NewGenFramework.Core.DataAccess.EntityFramework;

namespace BayiPuan.DataAccess.Concrete.EntityFramework
{
     public class EfRoleDal:EfEntityRepositoryBase<Role,BayiPuanContext>,IRoleDal
    {
    }
}
