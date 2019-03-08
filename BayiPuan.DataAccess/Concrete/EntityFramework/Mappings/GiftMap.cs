using System.Data.Entity.ModelConfiguration;
using BayiPuan.Entities.Concrete;

namespace BayiPuan.DataAccess.Concrete.EntityFramework.Mappings
{
  public class GiftMap : EntityTypeConfiguration<Gift>
  {
    public GiftMap()
    {
      ToTable("Gifts", "dbo");
      HasKey(x => x.GiftId);
      Property(x => x.GiftId).HasColumnName("GiftId");
      Property(x => x.CategoryId).HasColumnName("CategoryId");
      Property(x => x.BrandId).HasColumnName("BrandId");
      Property(x => x.Usage).HasColumnName("Usage");
      Property(x => x.Coverage).HasColumnName("Coverage");
      Property(x => x.ValidityPeriod).HasColumnName("ValidityPeriod");
      Property(x => x.Indivisible).HasColumnName("Indivisible");
      Property(x => x.Combining).HasColumnName("Combining");
      Property(x => x.TermOfUse).HasColumnName("TermOfUse");
      Property(x => x.Cancellation).HasColumnName("Cancellation");
      Property(x => x.Detail).HasColumnName("Detail");
      Property(x => x.Description).HasColumnName("Description");
      Property(x => x.GiftPoint).HasColumnName("GiftPoint");
      Property(x => x.IsActive).HasColumnName("IsActive");

    }
  }
}