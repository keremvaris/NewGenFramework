using System.Data.Entity.ModelConfiguration;
using BayiPuan.Entities.Concrete;

namespace BayiPuan.DataAccess.Concrete.EntityFramework.Mappings
{
  public class BuyMap : EntityTypeConfiguration<Buy>
  {
    public BuyMap()
    {
      ToTable(@"Buys", "dbo");
      HasKey(x => x.BuyId);
      Property(x => x.BuyId).HasColumnName("BuyId");
      Property(x => x.GiftId).HasColumnName("GiftId");
      Property(x => x.UserId).HasColumnName("UserId");
      Property(x => x.BuyDate).HasColumnName("BuyDate");
      Property(x => x.BuyAmount).HasColumnName("BuyAmount");
      Property(x => x.IsApproved).HasColumnName("IsApproved");
      Property(x => x.ApprovedDate).HasColumnName("ApprovedDate");
      Property(x => x.NotApproved).HasColumnName("NotApproved");
      Property(x => x.NotApprovedDate).HasColumnName("NotApprovedDate");
      Property(x => x.Reason).HasColumnName("Reason");
      Property(x => x.EditUserId).HasColumnName("EditUserId");

    }
  }
}