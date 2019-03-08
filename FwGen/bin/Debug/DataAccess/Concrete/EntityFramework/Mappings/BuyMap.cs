using System.Data.Entity.ModelConfiguration;
using NewGenFramework.Entities.Concrete;

namespace NewGenFramework.DataAccess.Concrete.EntityFramework.Mappings
{
  public class BuyMap : EntityTypeConfiguration<Buy>
  {
    public BuyMap()
    {
      ToTable(@"Buies","dbo");
HasKey(x => x.BuyId);
Property(x => x.BuyId).HasColumnName("BuyId");
Property(x => x.Gift).HasColumnName("Gift");
Property(x => x.GiftId).HasColumnName("GiftId");
Property(x => x.User).HasColumnName("User");
Property(x => x.UserId).HasColumnName("UserId");
Property(x => x.BuyDate).HasColumnName("BuyDate");
Property(x => x.BuyAmount).HasColumnName("BuyAmount");
Property(x => x.IsApproved).HasColumnName("IsApproved");
Property(x => x.ApprovedDate).HasColumnName("ApprovedDate");
Property(x => x.NotApproved).HasColumnName("NotApproved");
Property(x => x.NotApprovedDate).HasColumnName("NotApprovedDate");
Property(x => x.Reason).HasColumnName("Reason");
Property(x => x.EditUserId).HasColumnName("EditUserId");
Property(x => x.BuyState).HasColumnName("BuyState");
Property(x => x.Brand).HasColumnName("Brand");
Property(x => x.BrandId).HasColumnName("BrandId");

    }
  }
}