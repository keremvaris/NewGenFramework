using NewGenFramework.Core.Entities;

namespace BayiPuan.Entities.Concrete
{
  public class Campaign : IEntity
  {
    public virtual int CampaignId { get; set; }
    public string CampaignName { get; set; }
    public byte[] CampaignImage { get; set; }
    public string CampaignImageExt { get; set; }
  }
}
