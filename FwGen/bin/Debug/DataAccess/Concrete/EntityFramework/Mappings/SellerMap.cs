using System.Data.Entity.ModelConfiguration;
using NewGenFramework.Entities.Concrete;

namespace NewGenFramework.DataAccess.Concrete.EntityFramework.Mappings
{
  public class SellerMap : EntityTypeConfiguration<Seller>
  {
    public SellerMap()
    {
      ToTable("Sellers","dbo");
HasKey(x => x.SellerId);
Property(x => x.SellerId).HasColumnName("SellerId");
Property(x => x.UserType).HasColumnName("UserType");
Property(x => x.UserTypeId).HasColumnName("UserTypeId");
Property(x => x.SellerName).HasColumnName("SellerName");
Property(x => x.City).HasColumnName("City");
Property(x => x.CityId).HasColumnName("CityId");
Property(x => x.SellerCode).HasColumnName("SellerCode");

    }
  }
}