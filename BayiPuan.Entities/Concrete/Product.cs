using NewGenFramework.Core.Entities;

namespace BayiPuan.Entities.Concrete
{
  public class Product : IEntity
  {
    public virtual int ProductId { get; set; }
    public virtual string ProductCode { get; set; }
    public virtual string ProductShortCode { get; set; }
    public virtual string ProductName { get; set; }
    public UnitType UnitType { get; set; }
    public virtual int UnitTypeId { get; set; }
    public virtual int StockAmount { get; set; }
    public virtual int RemainingStockAmount { get; set; }
    public virtual int CriticalStockAmount { get; set; }
    public virtual decimal Point { get; set; }
    public virtual decimal PointToMoney { get; set; }



  }
}
