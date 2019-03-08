using System.Data.Entity.ModelConfiguration;
using BayiPuan.Entities.Concrete;

namespace BayiPuan.DataAccess.Concrete.EntityFramework.Mappings
{
  public class CampaignMap : EntityTypeConfiguration<Campaign>
  {
    public CampaignMap()
    {
      ToTable("Campaigns","dbo");
HasKey(x => x.CampaignId);
Property(x => x.CampaignId).HasColumnName("CampaignId");
Property(x => x.CampaignName).HasColumnName("CampaignName");

    }
  }
}