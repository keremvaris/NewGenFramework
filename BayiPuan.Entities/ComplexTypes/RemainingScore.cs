using NewGenFramework.Core.Entities;

namespace BayiPuan.Entities.ComplexTypes
{
  public class RemainingScore : IEntity
  {
    public int UserId { get; set; }
    public decimal RemainingScorePoint { get; set; }
    public decimal SumPointToMoney { get; set; }
  }
}