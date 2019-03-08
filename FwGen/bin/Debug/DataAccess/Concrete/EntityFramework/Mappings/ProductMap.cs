using System.Data.Entity.ModelConfiguration;
using NewGenFramework.Entities.Concrete;

namespace NewGenFramework.DataAccess.Concrete.EntityFramework.Mappings
{
  public class ProductMap : EntityTypeConfiguration<Product>
  {
    public ProductMap()
    {
      ToTable("Products","dbo");
HasKey(x => x.ProductId);
Property(x => x.ProductId).HasColumnName("ProductId");
Property(x => x.ProductCode).HasColumnName("ProductCode");
Property(x => x.ProductShortCode).HasColumnName("ProductShortCode");
Property(x => x.ProductName).HasColumnName("ProductName");
Property(x => x.UnitType).HasColumnName("UnitType");
Property(x => x.UnitTypeId).HasColumnName("UnitTypeId");
Property(x => x.StockAmount).HasColumnName("StockAmount");
Property(x => x.RemainingStockAmount).HasColumnName("RemainingStockAmount");
Property(x => x.CriticalStockAmount).HasColumnName("CriticalStockAmount");
Property(x => x.Point).HasColumnName("Point");
Property(x => x.PointToMoney).HasColumnName("PointToMoney");

    }
  }
}