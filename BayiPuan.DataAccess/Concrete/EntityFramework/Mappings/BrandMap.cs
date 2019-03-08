using System.Data.Entity.ModelConfiguration;
using BayiPuan.Entities.Concrete;

namespace BayiPuan.DataAccess.Concrete.EntityFramework.Mappings
{
  public class BrandMap : EntityTypeConfiguration<Brand>
  {
    public BrandMap()
    {
      ToTable("Brands","dbo");
HasKey(x => x.BrandId);
Property(x => x.BrandId).HasColumnName("BrandId");
Property(x => x.BrandName).HasColumnName("BrandName");
Property(x => x.BrandImage).HasColumnName("BrandImage");
Property(x => x.BrandImageExt).HasColumnName("BrandImageExt");

    }
  }
}