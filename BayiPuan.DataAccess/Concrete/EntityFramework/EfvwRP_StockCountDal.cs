using BayiPuan.DataAccess.Concrete.Context;
using BayiPuan.Entities.ComplexTypes;
using NewGenFramework.Core.DataAccess.EntityFramework;
using BayiPuan.DataAccess.Abstract;


namespace BayiPuan.DataAccess.Concrete.EntityFramework
{
    
    public class EfvwRP_StockCountDal : EfEntityRepositoryBase<vwRP_StockCount, BayiPuanContext>, IvwRP_StockCount
    {
    }
}
