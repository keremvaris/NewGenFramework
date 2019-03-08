using System.Data.Entity.ModelConfiguration;
using NewGenFramework.Entities.Concrete;

namespace NewGenFramework.DataAccess.Concrete.EntityFramework.Mappings
{
  public class MyProductMap : EntityTypeConfiguration<MyProduct>
  {
    public MyProductMap()
    {
      ToTable("MyProducts","dbo");
HasKey(x => x.MyProductId);
Property(x => x.MyProductId).HasColumnName("MyProductId");
Property(x => x.ProductName).HasColumnName("ProductName");
Property(x => x.MyProductImage).HasColumnName("MyProductImage");
Property(x => x.MyProductImageExt).HasColumnName("MyProductImageExt");
Property(x => x.Description).HasColumnName("Description");
Property(x => x.IsActive).HasColumnName("IsActive");

    }
  }
}