using System.Collections.Generic;
using BayiPuan.Entities.ComplexTypes;
using BayiPuan.Entities.Concrete;


namespace BayiPuan.MvcWebUi.Models.ComplexTypeModels
{
  public class ViewModel
  {
    public IEnumerable<ProductPoint> totalWon { get; set; }
    public decimal remainingScore { get; set; }
    public decimal remainingScoreToMoney { get; set; }
    public IEnumerable<SpentPoint> spentPoint { get; set; }
    public IEnumerable<SalesRanking> saleRankings { get; set; }
    public List<Campaign> CampaignView { get; set; }
  }
}