using System.Data.Entity.ModelConfiguration;
using NewGenFramework.Entities.Concrete;

namespace NewGenFramework.DataAccess.Concrete.EntityFramework.Mappings
{
  public class MyNewMap : EntityTypeConfiguration<MyNew>
  {
    public MyNewMap()
    {
      ToTable("MyNews","dbo");
HasKey(x => x.NewsId);
Property(x => x.NewsId).HasColumnName("NewsId");
Property(x => x.NewsName).HasColumnName("NewsName");
Property(x => x.NewsImage).HasColumnName("NewsImage");
Property(x => x.NewsImageExt).HasColumnName("NewsImageExt");
Property(x => x.Description).HasColumnName("Description");
Property(x => x.NewsType).HasColumnName("NewsType");
Property(x => x.IsActive).HasColumnName("IsActive");

    }
  }
}