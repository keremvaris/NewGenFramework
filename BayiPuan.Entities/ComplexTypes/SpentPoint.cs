using NewGenFramework.Core.Entities;

namespace BayiPuan.Entities.ComplexTypes
{
    public class SpentPoint : IEntity
    {
        public int UserId { get; set; }
        public decimal SpendPoint { get; set; }
      public decimal SumPointToMoney { get; set; }
    }
}