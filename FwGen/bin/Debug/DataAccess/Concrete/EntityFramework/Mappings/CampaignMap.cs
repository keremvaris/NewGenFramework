using System.Data.Entity.ModelConfiguration;
using NewGenFramework.Entities.Concrete;

namespace NewGenFramework.DataAccess.Concrete.EntityFramework.Mappings
{
  public class CampaignMap : EntityTypeConfiguration<Campaign>
  {
    public CampaignMap()
    {
      ToTable("Campaigns","dbo");
HasKey(x => x.CampaignId);
Property(x => x.CampaignId).HasColumnName("CampaignId");
Property(x => x.CampaignName).HasColumnName("CampaignName");
Property(x => x.CampaignImage).HasColumnName("CampaignImage");
Property(x => x.CampaignImageExt).HasColumnName("CampaignImageExt");

    }
  }
}