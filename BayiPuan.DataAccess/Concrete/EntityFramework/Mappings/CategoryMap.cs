using System.Data.Entity.ModelConfiguration;
using BayiPuan.Entities.Concrete;

namespace BayiPuan.DataAccess.Concrete.EntityFramework.Mappings
{
  public class CategoryMap : EntityTypeConfiguration<Category>
  {
    public CategoryMap()
    {
      ToTable(@"Categories","dbo");
HasKey(x => x.CategoryId);
Property(x => x.CategoryId).HasColumnName("CategoryId");
Property(x => x.CategoryName).HasColumnName("CategoryName");
Property(x => x.TopCategoryId).HasColumnName("TopCategoryId");

    }
  }
}