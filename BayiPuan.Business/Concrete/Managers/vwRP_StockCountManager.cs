using System.Collections.Generic;
using BayiPuan.Business.Abstract;
using BayiPuan.DataAccess.Abstract;
using BayiPuan.Entities.ComplexTypes;
using NewGenFramework.Core.CrossCuttingConcerns.Transaction;




namespace BayiPuan.Business.Concrete.Managers
{
    
    public class vwRP_StockCountManager: ManagerBase, IvwRP_StockCountService
    {
        private readonly IvwRP_StockCount _rpStockCount;

        public vwRP_StockCountManager(IvwRP_StockCount rpStockCount)
        {
            _rpStockCount = rpStockCount;
        }

        public List<vwRP_StockCount> StockCounts()
        {
            return _rpStockCount.GetList();
        }
    }
}
