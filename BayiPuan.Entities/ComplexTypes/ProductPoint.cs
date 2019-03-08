using NewGenFramework.Core.Entities;

namespace BayiPuan.Entities.ComplexTypes
{
    public class ProductPoint : IEntity
    {
        public int UserId { get; set; }
        public decimal SumPoint { get; set; }
      public decimal SumPointToMoney { get; set; }
    }
}