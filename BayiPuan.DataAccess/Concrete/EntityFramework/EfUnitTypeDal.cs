using NewGenFramework.Core.DataAccess.EntityFramework;
using BayiPuan.DataAccess.Abstract;
using BayiPuan.DataAccess.Concrete.Context;
using BayiPuan.Entities.Concrete;

namespace BayiPuan.DataAccess.Concrete.EntityFramework
{
     public class EfUnitTypeDal:EfEntityRepositoryBase<UnitType,BayiPuanContext>,IUnitTypeDal
    {
    }
}
