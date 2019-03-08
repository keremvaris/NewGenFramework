using System.Data.Entity.ModelConfiguration;
using BayiPuan.Entities.Concrete;

namespace BayiPuan.DataAccess.Concrete.EntityFramework.Mappings
{
  public class SellerMap : EntityTypeConfiguration<Seller>
  {
    public SellerMap()
    {
      ToTable("Sellers","dbo");
HasKey(x => x.SellerId);
Property(x => x.SellerId).HasColumnName("SellerId");
Property(x => x.UserTypeId).HasColumnName("UserTypeId");
Property(x => x.SellerName).HasColumnName("SellerName");
Property(x => x.CityId).HasColumnName("CityId");
Property(x => x.SellerCode).HasColumnName("SellerCode");

    }
  }
}